using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaberu_fox : MonoBehaviour
{

    public Fade_out003 FadeOut;
    private bool isFadeOut_Finish;
    private SpriteRenderer spr = null;


    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.color = new Color(1, 1, 1, 0);
        CPData.isPose = true;

    }

    // Update is called once per frame
    void Update()
    {
        isFadeOut_Finish = FadeOut.fadeOut_finish;
        if(isFadeOut_Finish)
        {
            Debug.Log(isFadeOut_Finish);
            spr.color = new Color(1, 1, 1, 1);

        }
        if(CPData.isPose==false)
        {
            spr.color = new Color(1, 1, 1, 1);
        }


    }
}
