using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clear_animation : MonoBehaviour
{
    Animator anim;

    public Fade_in_crear fade_crear;
    public bool fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        fadeIn = fade_crear.fadeIn;

       // Debug.Log("aaaaaa"+fadeIn);

        if (!fadeIn)
        {
            
            anim.SetBool("Clear_finish",true);
        }

    }
}
