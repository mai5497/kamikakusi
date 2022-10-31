using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class mado_text : MonoBehaviour
{
    private list_zu list;
    private CP_move01 move;
    //   private GameUIManager manager;
    private bool UIok;

    public Text text = null;
    private bool DeleteOk;
    public bool nextUI2;

    // Start is called before the first frame update
    void Start()
    {
        list = GameObject.Find("list_zu").GetComponent<list_zu>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        text = GetComponent<Text>();
        text.color = new Color(1, 1, 1, 0);

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
            text.color = new Color(1, 1, 1, 1);


            if (CPData.isLens)
            {
                text.color = new Color(1, 1, 1, 0);
                nextUI2 = true;
                UIok = false;
            }

        }


    }
}
