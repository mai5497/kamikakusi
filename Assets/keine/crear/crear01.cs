using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crear01 : MonoBehaviour
{
    public FoxByakko crear;
    bool isCrearOk;


    public Fade_in_crear Fade_in;

    public SpriteRenderer img;
    // bool fadein;
   // public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        //ŒÏŒ©‚Â‚¯‚½ƒNƒŠƒA”»’è
        isCrearOk = crear.isClear;


        if(isCrearOk)
        {
            Fade_in.fade_in_use(img);  
        }






        
    }
}
