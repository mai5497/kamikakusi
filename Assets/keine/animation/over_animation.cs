using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class over_animation : MonoBehaviour
{
    Animator anim;

    public Fade_in_gemeover fade_over;
    public bool fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        fadeIn = fade_over.fadeIn;

       // Debug.Log("aaaaaa"+fadeIn);

        if (!fadeIn)
        {
            
            anim.SetBool("Over_finish",true);
        }

    }
}
