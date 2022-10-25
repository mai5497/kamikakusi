using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Fox_text : MonoBehaviour
{
    private Vector2 Pos;
    public Transform transform_oya;
    public Text text;
    private RectTransform rec;
    public CP_move01 move;

    public string No1Text;
    public string No2Text;
    public string No3Text;

    private int count = 0;

    public Fade_out003 FadeOut;
    private bool isFadeOut_Finish;
    public bool isTextFin = false;

    private InputAction _fadeAction;
    // Start is called before the first frame update
    void Start()
    {
        Pos = new Vector2(this.transform.position.x, this.transform.position.y);
        rec = GetComponent<RectTransform>();
        
    var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _fadeAction = actionMap["Fade"];


        text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        isFadeOut_Finish = FadeOut.fadeOut_finish;
       

        //テキストの場所
        Text_Pos();
        //テキストの中身
        Text();
       

    }









    public void Text_Pos()
    {
        Pos.x = transform_oya.parent.position.x;
        Pos.y = transform_oya.parent.position.y + rec.position.y;
    }
    public void Text()
    {
      //  move.OnDisable();

        bool Fade = _fadeAction.WasPerformedThisFrame();
        if (Fade)
        {
            // text.text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            count += 1;
        }

        switch (count)
        {
            case 0:
                if (isFadeOut_Finish)
                {
                    text.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                }
                text.text = No1Text;
                break;

            case 1:
                text.text = No2Text;
                break;

            case 3:
                text.text = No3Text;

                break;

            case 4:
                text.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                CPData.isPose = false;
              //  move.OnEnable();
                //  isTextFin = true;
                break;
        }


    }
}

    //public bool GetTextFinish()
    //{
    //    return isTextFin;
    //}}
