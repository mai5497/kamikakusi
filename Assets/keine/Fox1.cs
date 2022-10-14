//=============================================================================
//
// 狐
//
// 作成日:2022/10/11
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/11 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox1 : MonoBehaviour {
    //private GameObject foxWindow;
    [System.NonSerialized]
    //public bool isWindowColl;   // 窓と当たったかフラグ
    private SpriteRenderer sr;  // 狐のスプライトレンダラー
    private float Alpha;    // 狐のアルファ値

   // private const float lookStopTime = 3.0f;    // 注視して止まらないといけない時間
    private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間

  //  private float lookStopTimer;    // 注視して止まらないといけない時間のカウント用タイマー

    public bool isByakko;

    public bool byakko;

    public Fox fox;


    // Start is called before the first frame update
    void Start() {
        //foxWindow = GameObject.FindWithTag("FoxWindow");
        //isWindowColl = false;

        //lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        Alpha = 0.0f;
        sr.color = new Color(1, 1, 1, Alpha);
        //白虎が出て来てから消えた判定
       
    }

    // Update is called once per frame
    void Update() {
        //----- 注視判定 -----
        byakko = fox.GetByakko_delete();

        if (byakko)
        {
          //  Debug.Log("狐でるよおおおおおおおお");

            // CPData.isRightAnswer = true;
            //lookStopTimer -= Time.deltaTime;

            // if (lookStopTimer < 0 && alpha < 1.0f)
            // {
                 Alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, Alpha);
         //   Debug.Log("alpha" + Alpha);

            //}



        }
    }
}
