//=============================================================================
//
// ヒントのオブジェクトにつけるスクリプト
//
// 作成日:2022/10/12
// 作成者:伊地田真衣
// 編集者:泉優樹
//
// <開発履歴>
// 2022/10/12 作成
// 2022/10/26 多重スクロール用に当たり判定の動的変更
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

    [System.NonSerialized]
    public bool isCheckThis;    // このオブジェクトが窓で覗かれたか

    // 当たり判定初期座標
    private Vector2 hitInitPos;
    // 当たり判定の補正
    private float hitCameraHosei = 1.0f;
    private float hitPlayerHosei = 0.25f;
    // カメラとプレイヤーの距離
    private float cameraToPlayer;

    // 使用カメラ
    private Camera cameraUse;

    // プレイヤーオブジェクト
    private GameObject cpObj;

    // ズームレンズ
    private ZoomLens zoomLens;

    // 子オブジェクト
    List<SpriteRenderer> childrenList = new List<SpriteRenderer>();

    // 子オブジェクト初期座標
    Vector3 localInitPos;
    // 子オブジェクトの初期オフセット
    Vector2 localInitOffset;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        _mado = GameObject.Find("Lens").GetComponent<mado>();

        isCheckThis = false;

        switch (this.gameObject.layer)
        {
            // Middle1
            case 7:
                cameraUse = GameObject.Find("CameraMiddle1").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle1").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle2
            case 9:
                cameraUse = GameObject.Find("CameraMiddle2").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle2").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle3
            case 10:
                cameraUse = GameObject.Find("CameraMiddle3").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle3").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
        }

        hitInitPos = this.GetComponent<CapsuleCollider2D>().offset;

        cpObj = GameObject.FindGameObjectWithTag("Player");

        zoomLens = GameObject.Find("CanvasLens").GetComponent<ZoomLens>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            childrenList.Add(this.transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        foreach (SpriteRenderer child in childrenList)
        {
            if (child.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
            {
                localInitPos = child.transform.localPosition;
                localInitOffset = this.GetComponent<CapsuleCollider2D>().offset;
            }
        }
    }


    void Update()
    {
        if (zoomLens.isZoom == false)
        {
            // 当たり判定の座標変更
            if (this.gameObject.layer == 7 || this.gameObject.layer == 9 || this.gameObject.layer == 10)
            {
                Vector2 hitPos;
                cameraToPlayer = Camera.main.transform.position.x - cpObj.transform.position.x;
                hitPos.x = hitInitPos.x + (cameraToPlayer * hitCameraHosei + Camera.main.transform.position.x * hitPlayerHosei);
                hitPos.y = hitInitPos.y;
                this.GetComponent<CapsuleCollider2D>().offset = hitPos;
            }
        }

        if (CPData.isLens)
        {
            foreach (SpriteRenderer child in childrenList)
            {
                switch (child.maskInteraction)
                {
                    // 後ろオブジェクト
                    case SpriteMaskInteraction.VisibleInsideMask:
                        child.gameObject.layer = 0;
                        child.transform.localPosition = new Vector3(localInitPos.x + this.GetComponent<CapsuleCollider2D>().offset.x - localInitOffset.x, localInitPos.y + this.transform.GetComponent<CapsuleCollider2D>().offset.y - localInitOffset.y, localInitPos.z);
                        break;
                }
            }
        }
        else
        {
            foreach (SpriteRenderer child in childrenList)
            {
                switch (child.maskInteraction)
                {
                    // 前オブジェクト
                    case SpriteMaskInteraction.VisibleOutsideMask:
                        child.enabled = true;
                        break;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isWindowColl = true;
            _mado.SetLookObjName(objName, uraObjName);
            //----- こっくりさんの紙のカウント -----
            if (isCheckThis == false && CPData.paperCnt > 0)
            {
                if (CPData.isLens && CPData.isKokkurisan)
                {
                    isCheckThis = true;
                    CPData.paperCnt--;
                }
            }

            if (CPData.isLens)
            {
                foreach (SpriteRenderer child in childrenList)
                {
                    switch (child.maskInteraction)
                    {
                        // 前オブジェクト
                        case SpriteMaskInteraction.VisibleOutsideMask:
                            child.enabled = false;
                            break;
                    }
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null, null);

            if (CPData.isLens)
            {
                foreach (SpriteRenderer child in childrenList)
                {
                    switch (child.maskInteraction)
                    {
                        // 前オブジェクト
                        case SpriteMaskInteraction.VisibleOutsideMask:
                            child.enabled = true;
                            break;
                    }
                }
            }
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
