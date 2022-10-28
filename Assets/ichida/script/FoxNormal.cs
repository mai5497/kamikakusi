using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Rendering;
using Live2D.Cubism.Framework;

public class FoxNormal : MonoBehaviour
{
    //private SpriteRenderer sr;  // 狐のスプライトレンダラー
    [Header("狐のパーツ(live2d)")]
    public CubismPartsInspector pi;
    public List<CubismRenderer> crList;

    private float Alpha;    // 狐のアルファ値

    private const float appearTime = 1.5f;  // アルファ値が上がりきるまでの時間


    private bool isDelete;

    private FoxByakko fox;


    // Start is called before the first frame update
    void Start() {
        //sr = GetComponent<SpriteRenderer>();
        Alpha = 0.0f;
        //cr.Color = new Color(1, 1, 1, Alpha);
        //pi.SendMessage("B_SIRO_KITUNE", Alpha);

        fox = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();
    }

    // Update is called once per frame
    void Update() {
        this.gameObject.layer = 0;

        isDelete = fox.GetByakko_delete();

        if (isDelete) {
            Alpha += Time.deltaTime / appearTime;
            //cr.Color = new Color(1, 1, 1, Alpha);
            //pi.SendMessage("B_SIRO_KITUNE", Alpha);
            fox.isClear = true;
        }
    }
}
