//=============================================================================
//
// 始めのヒント１文字を表示するスクリプト
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/17 作成(ランダムで一文字表示)
// 2022/10/20 ランダムで表示ではなく、制作者側で設定できるように
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHintUIChar : MonoBehaviour
{
    [Header("狐か人か(trueが狐)")]
    [SerializeField]
    private bool kituneORnormal = true;

    private Text hintCharText;      // 表示用テキスト型変数
    private string firstHintKitune; // 狐と人の一文字ヒントの両方を兼ねているのでそれぞれ用意している
    private string firstHintNormal;


    // Start is called before the first frame update
    void Start() {
        hintCharText = GetComponent<Text>();

        firstHintKitune = CPData.kituneHint;
        firstHintNormal = CPData.normalHint;

        //----- 答えの文字からランダムで一文字取得する -----
        // 初期化されておらず何も入っていなかった場合はランダムで表示
        int random;
        if (kituneORnormal) {
            if (firstHintKitune == "") {
                random = UnityEngine.Random.Range(0, CPData.kituneClearStr.Length);
                firstHintKitune = CPData.kituneClearStr.Substring(random, 1);
                CPData.kituneHint = firstHintKitune;
            }
        } else {
            if (firstHintNormal == "") {
                random = UnityEngine.Random.Range(0, CPData.normalClearStr.Length);
                firstHintNormal = CPData.normalClearStr.Substring(random, 1);
                CPData.normalHint = firstHintNormal;
            }
        }

        //----- 文字の表示 -----
        if (kituneORnormal) {
            hintCharText.text = firstHintKitune;
        } else {
            hintCharText.text = firstHintNormal;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
