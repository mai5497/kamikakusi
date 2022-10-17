//=============================================================================
//
// 注視残り回数UI
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/17 作成
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
