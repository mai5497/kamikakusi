using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class list_zu : MonoBehaviour
{
    private idou_zu idou;
    private GameUIManager manager;
    private bool UIok;

    private SpriteRenderer spr = null;
    private bool DeleteOk;
    public bool nextUI1;
    private float time = 0.0f;
    public int col;

    // Start is called before the first frame update
    void Start()
    {
        idou = GameObject.Find("idou_zu").GetComponent<idou_zu>();
        manager = GameObject.Find("GameUIManager").GetComponent<GameUIManager>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        DeleteOk = false;
        col = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!nextUI1)
        {
            UIok = idou.nextUI;
        }
       


        DeleteOk = manager.UI_out;


            if (UIok)
            {
                spr.color = new Color(1, 1, 1, col);


            time += Time.deltaTime;
            if (time >= 2)
            {
                if (DeleteOk)
                {
                    col = 0;
                    spr.color = new Color(1, 1, 1, col);
                    nextUI1 = true;
                }
            }

        }


    }
}
