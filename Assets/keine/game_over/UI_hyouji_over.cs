using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hyouji_over : MonoBehaviour
{
    public Fade_in_gemeover fade_over;
    public bool fadeIn;
    private SpriteRenderer oder = null;
    // Start is called before the first frame update
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        //  oder.sortingOrder;
        oder.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        fadeIn = fade_over.fadeIn;

        if (!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = 6;
        }



    }
}
