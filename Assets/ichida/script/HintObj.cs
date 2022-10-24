//=============================================================================
//
// ヒントのオブジェクトにつけるスクリプト
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

public class HintObj : MonoBehaviour {

    //[System.NonSerialized]
    public string objName;    // このオブジェクトの名前をインスペクターで設定しておく
    //[System.NonSerialized]
    public bool _isHatenaHuman; // ？に変換するか
    //[System.NonSerialized]
    public string hatenaObjName;

    //[System.NonSerialized]
    public string uraObjName;
    //[System.NonSerialized]
    public bool _isHatenaFox; // ？に変換するか
    //[System.NonSerialized]
    public string hatenaUraName;


    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ

    private mado _mado;     // 窓オブジェクトについている窓スクリプト

    private GameObject objList;             // オブジェクトの名前を表示するスクリプトが入っている
   
    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        _mado = GameObject.Find("Lens").GetComponent<mado>();
    }

    // Update is called once per frame
    void Update() {
        if (isWindowColl && CPData.isLook) {
            CPData.isRightAnswer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
            _mado.SetLookObjName(objName, uraObjName);
            
            //if (_isHatenaHuman) {
            //    _ObjNameListUI.SetText(hatenaObjName, objName);
            //}
            //if (_isHatenaFox) {
            //    _ObjNameListUI.SetText(hatenaUraName, uraObjName);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null, null);
        }
    }

    public string GetObjName() {
        return objName;
    }
    public string GetNormalHatenaStr() {
        return hatenaObjName;
    }

    public string GetUraObjName() {
        return uraObjName;
    }
    public string GetUraHatenaStr() {
        return hatenaUraName;
    }
}
