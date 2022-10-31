using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class list_text : MonoBehaviour
{
    private idou_zu idou;
    public Text text_list;
    private GameUIManager manager;

    private bool isTextFin;
    private bool DeleteOk;

    public bool nextUI1;
    private bool UIok;
    public int col;
    //  public Text text_idou;
    // Start is called before the first frame update
    void Start()
    {
        idou = GameObject.Find("idou_zu").GetComponent<idou_zu>();
        manager = GameObject.Find("GameUIManager").GetComponent<GameUIManager>();
        //   text = GameObject.Find("Text (fox)").GetComponent<Fox_text>();
        // move = GameObject.Find("CP_001").GetComponent<CP_move01>();
        //spr = GetComponent<Text>();
        //spr.color = new Color(1, 1, 1, 0);
        text_list.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        col = 1;
        DeleteOk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!nextUI1)
        {
            UIok = idou.nextUI;
        }



        DeleteOk = manager.UI_out;


        if (UIok)
        {
          //  spr.color = new Color(1, 1, 1, col);
            text_list.color = new Color(1.0f, 1.0f, 1.0f, col);

            if (DeleteOk)
            {
                col = 0;
                text_list.color = new Color(1.0f, 1.0f, 1.0f, col);
                nextUI1 = true;
            }

        }



    }
}
