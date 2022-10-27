using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class idou_zu : MonoBehaviour
{
    private Fox_text text;
    private CP_move01 move;

    private bool isTextFin;
    private bool DeleteOk;

    public bool nextUI;

    private SpriteRenderer spr=null;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Text (fox)").GetComponent<Fox_text>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        DeleteOk = false;
    }

    // Update is called once per frame
    void Update()
    {


        isTextFin = text.isTextFin;
        DeleteOk = move.isUiOk;

        if (isTextFin)
        {
            spr.color = new Color(1, 1, 1, 1);


            if (DeleteOk)
            {
                spr.color = new Color(1, 1, 1, 0);
                nextUI = true;
            }

        }



    }
}
