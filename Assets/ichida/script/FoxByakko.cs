using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxByakko : MonoBehaviour
{
    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ
    private SpriteRenderer sr;  // 狐のスプライトレンダラー
    private float alpha;    // 狐のアルファ値

    private const float lookStopTime = 3.0f;    // 注視して止まらないといけない時間
    private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間

    private float lookStopTimer;    // 注視して止まらないといけない時間のカウント用タイマー

    private bool isByakko_delete = false;   // 白狐が消えたらtrue
    private bool isDeleting;    // 消してる最中のフラグ

    private Kokkurisan _Kokkurisan;  // こっくりさんのスクリプト取得
    [System.NonSerialized]
    public bool isClear;    // ゲームクリアのフラグ

    private bool oldIsLook;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        isDeleting = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);

        _Kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
        isClear = false;

        oldIsLook = CPData.isLook;
    }

    // Update is called once per frame
    void Update() {
        //----- 注視判定 -----
        if(!isWindowColl && !oldIsLook && CPData.isLook) {    // 窓が当たっていないときに注視されたとき
            CPData.lookCnt--;
        }

        if (_Kokkurisan.isClear && isWindowColl && CPData.isLook && !isDeleting) {
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;

            if (lookStopTimer < 0 && alpha <= 1.0f) {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
            } else if (alpha > 1.0) {
                isDeleting = true;
            }
        } else if (isDeleting) {
            alpha -= Time.deltaTime / appearTime;
            sr.color = new Color(1, 1, 1, alpha);
            if (alpha < 0.0f) {
                isByakko_delete = true;
            }
        }

        oldIsLook = CPData.isLook;
    }
    public bool GetByakko_delete() {
        return isByakko_delete;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }
}
