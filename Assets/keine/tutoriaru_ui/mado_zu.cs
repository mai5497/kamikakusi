using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mado_zu : MonoBehaviour
{
    private list_zu list;
    private CP_move01 move;
    //   private GameUIManager manager;
    private bool UIok;

    private SpriteRenderer spr = null;
    private bool DeleteOk;
    public bool nextUI2;

    // Start is called before the first frame update
    void Start()
    {
        list = GameObject.Find("list_zu").GetComponent<list_zu>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (!nextUI2)
        {
            UIok = list.nextUI1;
        }
     
        //  DeleteOk = manager.UI_out;


        if (UIok)
        {
            spr.color = new Color(1, 1, 1, 1);


            if (CPData.isLens)
            {
                spr.color = new Color(1, 1, 1, 0);
                nextUI2 = true;
                UIok = false;
            }

        }


    }
}
