//=============================================================================
//
// ぼかし回復ゲージ処理
//
// 作成日:2022/10/13
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/13 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBlurHeal : MonoBehaviour
{
    public BlurIn blurIn;
    public Image gaugeBlurHeal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ぼかし解除はゲージ表示
        if (blurIn.isBlur == false)
        {
            gaugeBlurHeal.enabled = true;
            this.transform.localScale = new Vector3(1 - blurIn.valueLerpNowBlur, this.transform.localScale.y, this.transform.localScale.z);
            //ゲージ回復終了時はゲージ非表示
            if (blurIn.valueLerpNowBlur <= 0.0f)
            {
                gaugeBlurHeal.enabled = false;
            }
        }
        // ばかし状態はゲージ非表示
        else
        {
            gaugeBlurHeal.enabled = false;
        }
    }
}
