//=============================================================================
//
// 見つける狐の管理
//
// 作成日:2022/10/20
// 作成者:伊地田真衣
// 編集者:泉優樹
//
// <開発履歴>
// 2022/10/20 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Rendering;
using Live2D.Cubism.Framework;

public class FoxByakko : MonoBehaviour {
    [System.NonSerialized]
    public bool isWindowColl;   // 窓と当たったかフラグ
    [Header("狐のパーツ(live2d)")]
    public CubismPartsInspector pi;
    public List<CubismRenderer> crList;
    private List<float> crAlphaList = new List<float>();
    private Animator animator;
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

    private ZoomLens _ZoomLens; // ズームの補間値の取得用

    private bool isSave = false;

    // 狐を見つけた時にエフェクトを出す
    public List<ParticleSystem> efFindList;

    private bool isInitAlpha = false;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        isDeleting = false;

        lookStopTimer = lookStopTime;

        //sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        foreach (CubismRenderer cr in crList)
        {
            crAlphaList.Add(cr.Color.a);
            //cr.Color = new Color(cr.Color.r, cr.Color.g, cr.Color.b, alpha);
        }
        //rc.Opacity = alpha;
        //pi.SendMessage("B_SIRO_KITUNE", alpha);
        animator = pi.gameObject.GetComponent<Animator>();

        _Kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
        isClear = false;

        oldIsLook = CPData.isLook;

        // シーン開始時現在のステージを保存
        ClearManager.SaveNowStage();

        if (GameObject.Find("CanvasLens")) {
            _ZoomLens = GameObject.Find("CanvasLens").GetComponent<ZoomLens>();
        }
    }

    // Update is called once per frame
    void Update() {
        this.gameObject.layer = 0;

        //----- 注視判定 -----
        //if (!isWindowColl && !oldIsLook && CPData.isLook) {    // 窓が当たっていないときに注視されたとき
        //    if (_ZoomLens.valueZoomLerp > 0.9) {
        //        CPData.lookCnt--;
        //    }
        //}
        if (!isWindowColl && oldIsLook && !CPData.isLook) {    // 窓が当たっていないときに注視されたとき
            if (_ZoomLens.valueZoomLerp > 0.9) {
                CPData.lookCnt--;
                SoundManager2.Play(SoundData.eSE.SE_MISS, SoundData.GameAudioList);
            }
        }

        if (_Kokkurisan.isClear && isWindowColl && CPData.isLook && !isDeleting) {
            // あたり！
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;
            for (int i = 0; i < crList.Count; i++)
            {
                crList[i].gameObject.SetActive(true);
            }

            // 当たりの時に注視し終わった時はズームを解除させない
            if (_ZoomLens.valueZoomLerp > 0.9)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CP_move01>().foxFind = true;
            }
            if (lookStopTimer < 0 && alpha <= 1.0f) {
                alpha += Time.deltaTime / appearTime;
                if (isInitAlpha == false)
                {
                    isInitAlpha = true;
                }
                else
                {
                    for (int i = 0; i < crList.Count; i++)
                    {
                        crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, alpha);
                        if (alpha < crAlphaList[i])
                        {
                            crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, alpha);
                        }
                        else
                        {
                            crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, crAlphaList[i]);
                        }
                    }
                }
                //pi.SendMessage("B_SIRO_KITUNE", alpha);

            } else if (alpha > 1.0) {
                isDeleting = true;
                foreach (ParticleSystem ef in efFindList) {
                    ef.Play();
                    SoundManager2.Play(SoundData.eSE.SE_GET, SoundData.GameAudioList);
                    animator.SetBool("isChange", true);
                }
            }
        } else if (isDeleting) {
            alpha -= Time.deltaTime / appearTime;
            //for (int i = 0; i < crList.Count; i++)
            //{
            //    if (alpha < crAlphaList[i])
            //    {
            //        crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, alpha);
            //    }
            //    else
            //    {
            //        crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, crAlphaList[i]);
            //    }
            //}
            //pi.SendMessage("B_SIRO_KITUNE", alpha);
            //animator.SetBool("isChange", false);
            if (alpha < 0.0f) {
                isByakko_delete = true;
            }
        }
        // 解除したらリセット
        else
        {
            lookStopTimer = lookStopTime;
            alpha = 0;
            for (int i = 0; i < crList.Count; i++)
            {
                crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, alpha);
                if (alpha < crAlphaList[i])
                {
                    crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, alpha);
                }
                else
                {
                    crList[i].Color = new Color(crList[i].Color.r, crList[i].Color.g, crList[i].Color.b, crAlphaList[i]);
                }
            }
            animator.SetBool("isChange", false);
            //pi.SendMessage("B_SIRO_KITUNE", alpha);
        }

        oldIsLook = CPData.isLook;

        // クリアしたらクリアステージを保存
        if (isClear && isSave == false) {
            ClearManager.SaveClearStage();
            isSave = true;
        }
    }
    public bool GetByakko_delete() {
        return isByakko_delete;
    }


    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.tag == "FoxWindow") {
    //        isWindowColl = true;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isWindowColl = true;
        }
    }
}
