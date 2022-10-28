using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Rendering;
using Live2D.Cubism.Framework;

public class FoxNormal : MonoBehaviour
{
    //private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    [Header("�ς̃p�[�c(live2d)")]
    public CubismPartsInspector pi;
    public List<CubismRenderer> crList;

    private float Alpha;    // �ς̃A���t�@�l

    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���


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
