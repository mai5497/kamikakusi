//=============================================================================
//
// ぼかし処理
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

public class BlurIn : MonoBehaviour
{
    [Header("ぼかしをかけるか")]
    public bool isBlur;
    public enum BlurMode{ 
        Normal,
        PressInit,
        Press
    }
    [Header("ぼかしの種類")]
    public BlurMode blurMode = BlurMode.Normal;
    [Header("ぼかしの最大値")]
    public float valueBlurMax;
    [Header("ぼかし速度(通常)")]
    public float speedNormal = 0.01f;
    [Header("ぼかし速度(長押し)")]
    public float speedPress = 0.025f;
    [Header("ぼかし回復速度")]
    public float speedHeal = 0.1f;
    [Header("長押しし初期秒(長押し)")]
    public float pressInitSecond = 1.0f;
    // 長押し時間
    private float pressSecond;

    // ぼかしをかけるマテリアル
    private Material mBlur;
    // ぼかしの値
    private float valueBlur = 0;

    [Header("ぼかしの現在の補間値　※確認用で表示してます")]
    public float valueLerpNowBlur;

    // Start is called before the first frame update
    void Start()
    {
        // マテリアルの取得
        mBlur = this.GetComponent<Image>().material;

        // ぼかしを非表示
        isBlur = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ぼかしをかけるか
        if (isBlur)
        {
            // ぼかし速度
            float speedBlur = 0;
            // ぼかしの種類
            switch (blurMode)
            {
                // 通常
                case BlurMode.Normal:
                    speedBlur = speedNormal;
                    pressSecond = pressInitSecond;
                    break;
                // 長押し初期
                case BlurMode.PressInit:
                    speedBlur = speedNormal;
                    pressSecond -= Time.deltaTime;
                    if (pressSecond < 0)
                    {
                        blurMode = BlurMode.Press;
                    }
                    break;
                // 長押し
                case BlurMode.Press:
                    speedBlur = speedPress;
                    break;
            }
            // ぼかしの補間値の決定
            valueLerpNowBlur += speedBlur * Time.deltaTime;
            if (valueLerpNowBlur > 1.0f)
            {
                valueLerpNowBlur = 1.0f;
            }
            // ぼかしの値を決定
            valueBlur = Mathf.Lerp(0, valueBlurMax, valueLerpNowBlur);
            // ぼかしの値をセット
            mBlur.SetFloat("_Blur", valueBlur);
        }
        else
        {
            // ぼかしの値をリセット
            mBlur.SetFloat("_Blur", 0);

            // ぼかしの補間値の回復
            valueLerpNowBlur -= speedHeal * Time.deltaTime;
            if (valueLerpNowBlur < 0)
            {
                valueLerpNowBlur = 0;
            }
        }
    }
}
