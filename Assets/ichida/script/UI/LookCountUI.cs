//=============================================================================
//
// �����c���UI
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/17 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookCountUI : MonoBehaviour
{
    private Text lookCntText;

    private Image lookUI;

    // Start is called before the first frame update
    void Start()
    {
        lookUI = transform.Find("Look").GetComponent<Image>();
        lookCntText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lookCntText.text = CPData.lookCnt.ToString();
        if(CPData.lookCnt <= 0) {
            lookUI.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        }
    }
}
