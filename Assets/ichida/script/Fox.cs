//=============================================================================
//
// 狐
//
// 作成日:2022/10/11
// 作成者:伊地田真衣
// 編集者：八木橋慧音
//
// <開発履歴>
// 2022/10/11 作成
// 2022/10/17 白狐の演出追加
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour {
    //private GameObject foxWindow;
    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ
    private SpriteRenderer sr;  // 狐のスプライトレンダラー
    private float alpha;    // 狐のアルファ値

    private const float lookStopTime = 3.0f;    // 注視して止まらないといけない時間
    private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間

    private float lookStopTimer;    // 注視して止まらないといけない時間のカウント用タイマー

    private bool isByakko_delete = true;
    private bool isByakko_flag = false;
    private bool isByakko_return = true;


    // Start is called before the first frame update
    void Start() {
        //foxWindow = GameObject.FindWithTag("FoxWindow");
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);
    }

    // Update is called once per frame
    void Update() {
        //----- 注視判定 -----
        if (isWindowColl) {
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;
            if (lookStopTimer < 0 && alpha < 1.0f) {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
            } else if (alpha >= 1.0f) {
                //    isByakko_delete = false;
                //}
            }

            //----- 白狐演出 -----
            //if (!isByakko_delete) {
            //    alpha -= Time.deltaTime / appearTime;
            //    sr.color = new Color(1, 1, 1, alpha);
            //    isByakko_flag = true;

            //}
            //if (isByakko_return) {
            //    if (isByakko_flag) {
            //        isByakko_return = false;
            //        //GetByakko_delete();   
            //    }
            //}

        }

        //public bool GetByakko_delete() {
        //    return isByakko_flag;
        //}

    }
}
