using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class idou_text : MonoBehaviour
{
    private Fox_text text;
    private CP_move01 move;

    private bool isTextFin;
    private bool DeleteOk;

    public bool nextUI;
    private float time = 0.0f;
    public Text text_idou;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Text (fox)").GetComponent<Fox_text>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        //spr = GetComponent<Text>();
        //spr.color = new Color(1, 1, 1, 0);
        text_idou.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        DeleteOk = false;
    }

    // Update is called once per frame
    void Update()
    {


        isTextFin = text.isTextFin;
        DeleteOk = move.isUiOk;

        if (isTextFin)
        {
            text_idou.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            time += Time.deltaTime;
            if (time >= 1)
            {
              

                if (DeleteOk)
                {
                    text_idou.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    nextUI = true;
                }
            }

        }



    }
}
