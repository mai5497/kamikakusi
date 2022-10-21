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
    void Start()
    {
        
    }
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
