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

    // Start is called before the first frame update
    void Start()
    {
        lookCntText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lookCntText.text = CPData.lookCnt.ToString();
    }
}
