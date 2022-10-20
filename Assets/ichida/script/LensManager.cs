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

    private bool oldIsLens;
    private Vector2 oldPlayerPos;

    private bool isLensInit;    // レンズの位置を初期化するか


    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<CP_move01>();
        // レンズ可視の取得
        enableLens = lensObj.GetComponent<EnableLens>();


        oldIsLens = CPData.isLens;
        oldPlayerPos = CPData.playerPos;
    }

    // Update is called once per frame
    void Update() {
        if(oldPlayerPos != CPData.playerPos) {
            isLensInit = true;

            oldPlayerPos = CPData.playerPos;
        }

        if (oldIsLens != CPData.isLens) {
            if (blurIn.isBlur == false) {
                // ぼかし,レンズの有効
                blurIn.isBlur = true;
                enableLens.EnableImage(true);
            } else {
                // レンズの無効
                blurIn.isBlur = false;
                enableLens.EnableImage(false);
            }

            oldIsLens = CPData.isLens;
        }

        // レンズ移動(通常時のみ)
        if (!CPData.isKokkurisan && !CPData.isObjNameUI && CPData.isLens && blurIn.blurMode == BlurIn.BlurMode.Normal) {
            if (isLensInit) {
                lensObj.transform.position = CPData.playerPos;
                isLensInit = false;
            }
            Vector2 moveVal;
            moveVal.x = _Player.GetMoveValue().x * lensSpeed;
            moveVal.y = _Player.GetMoveValue().y * lensSpeed;
            lensObj.transform.position = new Vector3(lensObj.transform.position.x + moveVal.x, lensObj.transform.position.y + moveVal.y, lensObj.transform.position.z);
        }
        // レンズの注視(ぼかしモード変更)
        if (CPData.isLook && CPData.isLens) {
            if (blurIn.blurMode == BlurIn.BlurMode.Normal) {
                blurIn.blurMode = BlurIn.BlurMode.PressInit;
                // ズーム処理
                zoomLens.isZoom = true;
            }
        } else {
            blurIn.blurMode = BlurIn.BlurMode.Normal;
            // ズーム解除処理
            zoomLens.isZoom = false;
        }
    }
}
