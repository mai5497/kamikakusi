using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class cyuusi_text : MonoBehaviour
{
    private mado_zu mado;
    private CP_move01 move;
    //   private GameUIManager manager;
    private bool UIok;

    public Text text = null;
    private bool DeleteOk;
    public bool nextUI3;

    // Start is called before the first frame update
    void Start()
    {
        mado = GameObject.Find("mado_zu").GetComponent<mado_zu>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1, 0);

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
        //    text.sortingOrder = 10;
            text.color = new Color(1, 1, 1, 1);


            if (CPData.isLook && CPData.isLens)
            {
                text.color = new Color(1, 1, 1, 0);
                nextUI3 = true;
                UIok = false;
            }

        }


    }
}
