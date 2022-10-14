//=============================================================================
//
// 窓　後で中身引っ越すかもと思って、名前適当
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

using UnityEngine.InputSystem;

public class mado : MonoBehaviour {
    private GameObject fox; // 狐のオブジェクト
    private CircleCollider2D foxCol;    // 狐のコライダー
    private Fox _Fox;   //狐オブジェクトについている狐クラス

    private CircleCollider2D windowCol;

    private string lookObjName; // 今見ているオブジェクトの名前

    // Start is called before the first frame update
    void Start() {
        fox = GameObject.FindWithTag("Fox");
        _Fox = fox.GetComponent<Fox>();
        foxCol = fox.GetComponent<CircleCollider2D>();

        windowCol = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(lookObjName);
        if (!CPData.isLens) {
            return; // 窓使用中でなければこのスクリプトにやることはないので返す
        }

        // 指定した範囲にモノがあるかの判定
        if (Physics2D.OverlapCircle(fox.transform.position, 0) == windowCol) {
            if (CPData.isLook) {
                _Fox.isWindowColl = true;
                Debug.Log("きつね！");
            } else {
                _Fox.isWindowColl = false;
            }
        }
    }

    /// <summary>
    /// 今見ているオブジェクトの名前を保存
    /// </summary>
    /// <param name="_lookObjName"></param>
    public void SetLookObjName(string _lookObjName) {
        lookObjName = _lookObjName;
    }

    /// <summary>
    /// 今見ているオブジェクトの名前の取得
    /// </summary>
    /// <returns></returns>
    public string GetLookObjName() {
        return lookObjName;
    }
}
