//=============================================================================
//
// �ڂ����񕜃Q�[�W����
//
// �쐬��:2022/10/13
// �쐬��:��D��
//
// <�J������>
// 2022/10/13 �쐬
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
        // �ڂ��������̓Q�[�W�\��
        if (blurIn.isBlur == false)
        {
            gaugeBlurHeal.enabled = true;
            this.transform.localScale = new Vector3(1 - blurIn.valueLerpNowBlur, this.transform.localScale.y, this.transform.localScale.z);
            //�Q�[�W�񕜏I�����̓Q�[�W��\��
            if (blurIn.valueLerpNowBlur <= 0.0f)
            {
                gaugeBlurHeal.enabled = false;
            }
        }
        // �΂�����Ԃ̓Q�[�W��\��
        else
        {
            gaugeBlurHeal.enabled = false;
        }
    }
}
