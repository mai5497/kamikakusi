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
    private GameObject fox; // 狐のオブジェクト(親)

    private string lookObjName; // 今見ているオブジェクトの名前
    private string lookUraObjName; // 今見ているオブジェクトの名前

    private Kokkurisan kokkurisan;

    // Start is called before the first frame update
    void Start() {
        kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();

        fox = GameObject.Find("Fox");

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(lookObjName);

        kokkurisan.kituneAnswerStr = lookUraObjName;
        kokkurisan.normalAnswerStr = lookObjName;

        if (kokkurisan.isClear) {
            fox.transform.position = this.transform.position;
        }
    }

    /// <summary>
    /// 今見ているオブジェクトの名前を保存
    /// </summary>
    /// <param name="_lookObjName"></param>
    public void SetLookObjName(string _lookObjName,string _lookUraObjName) {
        lookObjName = _lookObjName;
        lookUraObjName = _lookUraObjName;
    }

    /// <summary>
    /// 今見ているオブジェクトの名前の取得
    /// </summary>
    /// <returns></returns>
    public string GetLookObjName() {
        return lookObjName;
    }
    public string GetLookUraObjName() {
        return lookUraObjName;
    }

}
