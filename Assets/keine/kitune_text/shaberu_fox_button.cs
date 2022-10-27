using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaberu_fox_button : MonoBehaviour
{

    // public Fade_out003 FadeOut;
   public Fade_out003 Fade;

    private bool isFadeOut_Finish;
    private SpriteRenderer spr = null;
    public Fox_text text;
    private bool isTextFox;
    // Start is called before the first frame update
    void Start()
    {
      //  text = GameObject.Find("Text(fox)").GetComponent<Fox_text>();
        //名前でオブジェクト取得
        Fade = GameObject.Find("fadeIn").GetComponent<Fade_out003>();
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        CPData.isPose = true;

    }

    // Update is called once per frame
    void Update()
    {
      
        isFadeOut_Finish = Fade.fadeOut_finish;
        if (isFadeOut_Finish)
        {
            //   Debug.Log(isFadeOut_Finish);
            spr.color = new Color(1, 1, 1, 1);

        }
        //if(CPData.isPose==false)
        //{
        //  Destroy(this.gameObject);


       // isTextFox = text.isTextFin;

        if (text.isTextFin)
        {
            spr.color = new Color(1, 1, 1, 0);
        }
    }


}

