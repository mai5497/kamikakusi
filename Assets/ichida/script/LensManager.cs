//=============================================================================
//
// 狐の窓の制御
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
// 編集者:泉優樹
//
// <開発履歴>
// 2022/10/17 作成
// 2022/10/24 編集(加速減速)
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{
    [Header("ぼかし")]
    [SerializeField]
    private BlurIn blurIn;
    [Header("レンズオブジェクト")]
    [SerializeField]
    private GameObject lensObj;
    [Header("ズーム")]
    [SerializeField]
    private ZoomLens zoomLens;
    [Header("レンズ速度倍率")]
    [SerializeField]
    private float lensSpeed = 0.05f;


    private CP_move01 _Player;

    // レンズ可視
    private EnableLens enableLens;

    private bool oldIsLens;         // islensがtrueになった瞬間のみする処理用
    private Vector2 oldPlayerPos;   // 移動していなければ先ほどまで表示していた場所に表示する為の変数

    private bool isLensInit;    // レンズの位置を初期化するか

    private RectTransform lensRT;

    private RectTransform lensCanvas;

    // 加速度
    public float moveAccel = 0.0f;
    [Header("移動加速_速度")]
    public float moveAccelSpeed;
    [Header("移動減速_速度")]
    public float moveDecelSpeed;
    // 現在のフレーム
    private int nowFrame = 0;
    // 保存するフレーム数
    private const int moveFrameMax = 10;
    // 何フレーム前を使用するか
    private const int useFrame = 5;
    // プレイヤー速度を毎フレーム保存しているリスト
    private Vector2[] moveBeforeList = new Vector2[moveFrameMax];

    // カメラ座標
    private Transform cameraTrans;
    [Header("カメラ補正値")]
    public Vector2 cameraHosei = new Vector2(0.5f, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<CP_move01>();
        // レンズ可視の取得
        enableLens = lensObj.GetComponent<EnableLens>();
        lensRT = lensObj.GetComponent<RectTransform>();

        lensCanvas = GameObject.Find("CanvasLens").GetComponent<RectTransform>();

        oldIsLens = CPData.isLens;
        oldPlayerPos = CPData.playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldPlayerPos != CPData.playerPos)
        {
            isLensInit = true;

            oldPlayerPos = CPData.playerPos;
        }

        if (oldIsLens != CPData.isLens)
        {
            if (blurIn.isBlur == false)
            {
                // ぼかし,レンズの有効
                blurIn.isBlur = true;
                enableLens.EnableImage(true);
            }
            else
            {
                // レンズの無効
                blurIn.isBlur = false;
                enableLens.EnableImage(false);
            }

            oldIsLens = CPData.isLens;
        }

        // レンズ移動(通常時のみ)
        if (!CPData.isKokkurisan && !CPData.isObjNameUI && CPData.isLens && blurIn.blurMode == BlurIn.BlurMode.Normal)
        {
            if (isLensInit)
            {
                lensRT.position = CPData.playerPos;
                isLensInit = false;
            }

            if (Mathf.Abs(_Player.GetMoveValue().x) > 0.1f || Mathf.Abs(_Player.GetMoveValue().y) > 0.1f)
            {
                UpdateMove();
            }
            else
            {
                UpdateIdle();
            }
        }
        else
        {
            UpdateIdle();
        }


        // レンズの注視(ぼかしモード変更)
        if (CPData.isLook && CPData.isLens)
        {
            if (blurIn.blurMode == BlurIn.BlurMode.Normal)
            {
                blurIn.blurMode = BlurIn.BlurMode.PressInit;
                // ズーム処理
                zoomLens.isZoom = true;
            }
        }
        else
        {
            blurIn.blurMode = BlurIn.BlurMode.Normal;
            // ズーム解除処理
            zoomLens.isZoom = false;
        }
    }

    // 動き(加速)
    private void UpdateMove()
    {
        Vector2 moveVal;
        Vector3 newLensPos = lensObj.transform.position;
        moveVal.x = _Player.GetMoveValue().x * lensSpeed * moveAccel * Time.deltaTime;
        moveVal.y = _Player.GetMoveValue().y * lensSpeed * moveAccel * Time.deltaTime;

        cameraTrans = Camera.main.transform;

        newLensPos.x += moveVal.x;
        newLensPos.y += moveVal.y;

        lensObj.transform.position = new Vector2(
                     //エリア指定して移動する
                     Mathf.Clamp(newLensPos.x, -(9.0f - cameraHosei.x) + cameraTrans.position.x, (9.0f - cameraHosei.x) + cameraTrans.position.x),
                     Mathf.Clamp(newLensPos.y, -(5.0f - cameraHosei.y) + cameraTrans.position.y, (5.0f - cameraHosei.y) + cameraTrans.position.y)
                     );

        // 加速処理
        moveAccel += moveAccelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);

        // 現在フレームにプレイヤーの速度を保存
        moveBeforeList[nowFrame] = _Player.GetMoveValue();
        nowFrame++;
        if (nowFrame >= moveFrameMax)
        {
            nowFrame = 0;
        }
    }
    // 待機(減速)
    private void UpdateIdle()
    {
        Vector2 moveVal;
        Vector3 newLensPos = lensObj.transform.position;
        // 指定したフレームのプレイヤーの移動速度を取り出す
        int frame = nowFrame - useFrame;
        if (frame < 0)
        {
            frame = moveFrameMax + frame;
        }
        moveVal.x = moveBeforeList[frame].x * lensSpeed * moveAccel * Time.deltaTime;
        moveVal.y = moveBeforeList[frame].y * lensSpeed * moveAccel * Time.deltaTime;

        newLensPos.x += moveVal.x;
        newLensPos.y += moveVal.y;

        lensObj.transform.position = new Vector2(
                     //エリア指定して移動する
                     Mathf.Clamp(newLensPos.x, -8.5f, 8.5f),
                     Mathf.Clamp(newLensPos.y, -4.5f, 4.5f)
                     );

        // 減速処理
        moveAccel -= moveDecelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);
    }
}
