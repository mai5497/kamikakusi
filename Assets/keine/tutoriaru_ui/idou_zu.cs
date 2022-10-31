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
    private float time = 0.0f;
    private SpriteRenderer spr=null;

    private Text childText;   // �q�I�u�W�F�N�g�ɂ��Ă���e�L�X�g
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Text (fox)").GetComponent<Fox_text>();
        move = GameObject.Find("CP_001").GetComponent<CP_move01>();

        //----- �ŏ��͔�\�� -----
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        childText = this.transform.GetChild(0).GetComponent<Text>();
        childText.enabled = false;
        
        DeleteOk = false;
    }

    // Update is called once per frame
    void Update()
    {


        isTextFin = text.isTextFin;
        DeleteOk = move.isUiOk;

        if (isTextFin)
        {
            // �\��
            spr.color = new Color(1, 1, 1, 1);
            childText.enabled = true;

            time += Time.deltaTime;
            if (time >= 1)
            {
              
                if (DeleteOk)
                {
                    spr.color = new Color(1, 1, 1, 0);
                    childText.enabled = false;
                    nextUI = true;
                }
            }

        }



    }
}
