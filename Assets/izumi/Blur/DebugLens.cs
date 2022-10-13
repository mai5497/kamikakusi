//=============================================================================
//
// レンズデバッグ用処理
//
// 作成日:2022/10/12
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DebugLens : MonoBehaviour
{
    // FPS用変数
    private int frameCount;
    private float prevTime;
    [Header("FPS表示用テキスト")]
    public Text textFps;

    // キーボード
    private Keyboard keyboard;

    [Header("レンズオブジェクト")]
    public GameObject lensObj;    
    // レンズ可視
    private EnableLens enableLens;

    [Header("ぼかし")]
    public BlurIn blurIn;
    [Header("ぼかしの現在の補間値確認用テキスト")]
    public Text valueLerpText;
    [Header("モード確認用テキスト")]
    public Text modeText;

    [Header("ズーム")]
    public ZoomLens zoomLens;

    // Start is called before the first frame update
    void Start()
    {
        // FPSに必要な変数の初期化
        frameCount = 0;
        prevTime = 0.0f;

        // キーボード
        keyboard = Keyboard.current;

        // レンズ可視の取得
        enableLens = lensObj.GetComponent<EnableLens>();
    }

    // Update is called once per frame
    void Update()
    {
        //FPS計算
        ++frameCount;
        float time = Time.realtimeSinceStartup - prevTime;
        if (time >= 0.5f)
        {
            float fps = frameCount / time;
            textFps.text = ((int)fps).ToString();
            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }

        // ぼかし補間値の表示
        valueLerpText.text = blurIn.valueLerpNowBlur.ToString();

        // モードの表示
        if (blurIn.isBlur)
        {
            switch (blurIn.blurMode)
            {
                case BlurIn.BlurMode.Normal:
                    modeText.text = "狐の窓";
                    break;
                case BlurIn.BlurMode.PressInit:
                    modeText.text = "注視始め";
                    break;
                case BlurIn.BlurMode.Press:
                    modeText.text = "注視";
                    break;
            }
        }
        else
        {
            modeText.text = "通常";
        }

        // 入力
        // レンズの使用切替
        if (keyboard.spaceKey.wasPressedThisFrame)
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
        }
        float moveSpeed = 1 * Time.deltaTime;
        // レンズ移動(通常時のみ)
        if (blurIn.blurMode == BlurIn.BlurMode.Normal)
        {
            if (keyboard.aKey.isPressed)
            {
                lensObj.transform.position = new Vector3(lensObj.transform.position.x - moveSpeed, lensObj.transform.position.y, lensObj.transform.position.z);
            }
            if (keyboard.dKey.isPressed)
            {
                lensObj.transform.position = new Vector3(lensObj.transform.position.x + moveSpeed, lensObj.transform.position.y, lensObj.transform.position.z);
            }
            if (keyboard.sKey.isPressed)
            {
                lensObj.transform.position = new Vector3(lensObj.transform.position.x, lensObj.transform.position.y - moveSpeed, lensObj.transform.position.z);
            }
            if (keyboard.wKey.isPressed)
            {
                lensObj.transform.position = new Vector3(lensObj.transform.position.x, lensObj.transform.position.y + moveSpeed, lensObj.transform.position.z);
            }
        }
        // レンズの注視(ぼかしモード変更)
        if (keyboard.shiftKey.isPressed)
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


}
