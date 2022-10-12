//=============================================================================
//
// ヒントのUI（文字の方）につけるためのスクリプト
//
// 作成日:2022/10/12
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    [SerializeField]
    private int hintNum = 0;    // ヒントのオブジェクトに対応する番号

    public  int HintNum {
        // 書き換えはしないのでgetterのみ作成
        get {
            return hintNum;
        }
    }
}
