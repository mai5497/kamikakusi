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
    private int hintNum;    // 何番目のヒントのオブジェクトか格納
                            // この番号と対応するヒントが出るため
                            // ヒント側でも設定が必要
    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ

    private const float lookStopTime = 3.0f;    // 注視して止まらないといけない時間
    //private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間

    private float lookStopTimer;    // 注視して止まらないといけない時間のカウント用タイマー

    private GameObject canvas;  // シーンのキャンバスにオブジェクトとUIヒント番号を比較して表示するのをいれてあるので
                                // キャンバスを取得する
    private ShowHintManager _ShowHint; // 上で書いたオブジェクトとUIのヒント番号を比較して表示する為のスクリプト


    // Start is called before the first frame update
    void Start()
    {
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        canvas = GameObject.Find("Canvas");
        _ShowHint = canvas.GetComponent<ShowHintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWindowColl) {
            lookStopTimer -= Time.deltaTime;    
            if (lookStopTimer < 0) {
                _ShowHint.ShowHintUI(hintNum);  // このオブジェクトのヒント番号を持っていく
            }
            /*
             * もう一度注視で表示することを想定していないのでlookStopTimerの初期化は無し
             */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "FoxWindow") {
            isWindowColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }
}
