using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_alpha : MonoBehaviour
{
    public Fade_title_haikei title;

    public bool title_finish;

     private SpriteRenderer image = null;
     private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        image.color = new Color(1, 1, 1, timer);

    }

    // Update is called once per frame
    void Update()
    {

        if(title_finish)
        {
            Debug.Log("ƒ{ƒ^ƒ“‚Å‚é");
            timer += Time.deltaTime;
            image.color = new Color(1, 1, 1, timer);
        }


    }
}
