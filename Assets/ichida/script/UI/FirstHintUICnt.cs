//=============================================================================
//
// 最初にもらえる文字数のヒント
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

public class FirstHintUICnt : MonoBehaviour
{
    [Header("狐か人か(trueが狐)")]
    [SerializeField]
    private bool kituneORnormal = true; 

    private Text hintCntText;

    private int hintCnt;

    // Start is called before the first frame update
    void Start() { 
        hintCntText = GetComponent<Text>();

        if (kituneORnormal) {
            hintCnt = CPData.kituneClearStr.Length - 1;
        } else {
            hintCnt = CPData.normalClearStr.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hintCntText.text = hintCnt.ToString();
    }
}
