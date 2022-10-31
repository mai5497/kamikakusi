using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cyuusi_zu : MonoBehaviour
{
    private mado_zu mado;
    private CP_move01 move;
    //   private GameUIManager manager;
    private bool UIok;

    private SpriteRenderer spr = null;
    private bool DeleteOk;
    public bool nextUI3;
    private float time = 0.0f;

    private Text childText;   // 子オブジェクトについているテキスト

    // Start is called before the first frame update
    void Start()
    {
        mado = GameObject.Find("mado_zu").GetComponent<mado_zu>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        childText = this.transform.GetChild(0).GetComponent<Text>();
        childText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!nextUI3)
        {
            UIok = mado.nextUI2;
        }

        //  DeleteOk = manager.UI_out;


        if (UIok)
        {
            spr.color = new Color(1, 1, 1, 1);
            childText.enabled = true;

            time += Time.deltaTime;
            if (time >= 1)
            {
                if (CPData.isLook && CPData.isLens)
                {
                    spr.color = new Color(1, 1, 1, 0);
                    childText.enabled = false;

                    nextUI3 = true;
                    UIok = false;
                }
            }

        }


    }
}
