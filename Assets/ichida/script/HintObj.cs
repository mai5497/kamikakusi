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

public class HintObj : MonoBehaviour
{

    [SerializeField]
    private string objName;    // このオブジェクトの名前をインスペクターで設定しておく
    [SerializeField]
    private string uraObjName;


    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ

    private mado _mado;     // 窓オブジェクトについている窓スクリプト

    private const float lookStopTime = 3.0f;    // 注視して止まらないといけない時間

    private float lookStopTimer;    // 注視して止まらないといけない時間のカウント用タイマー

    //private GameObject canvas;  // シーンのキャンバスにオブジェクトとUIヒント番号を比較して表示するのをいれてあるので
    //                            // キャンバスを取得する
    //private ShowHintManager _ShowHint; // 上で書いたオブジェクトとUIのヒント番号を比較して表示する為のスクリプト

    // Start is called before the first frame update
    void Start()
    {
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        //canvas = GameObject.Find("Canvas");
        //if (canvas != null) {
        //    _ShowHint = canvas.GetComponent<ShowHintManager>();
        //}

        _mado = GameObject.Find("Lens").GetComponent<mado>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWindowColl && CPData.isLook) {
            lookStopTimer -= Time.deltaTime;
            CPData.isRightAnswer = true;
            //if (lookStopTimer < 0) {
            //    if (canvas != null) {
            //        _ShowHint.ShowHintUI(hintNum);  // このオブジェクトのヒント番号を持っていく
            //    }
            //}
            /*
             * 注視でヒントが出なくなったのでコメントアウト
             * もう一度注視で表示することを想定していないのでlookStopTimerの初期化は無し
             */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
            _mado.SetLookObjName(objName,uraObjName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null,null);
        }
    }

    public string GetObjName() {
        return objName;
    }
    public string GetUraObjName() {
        return uraObjName;
    }
}
