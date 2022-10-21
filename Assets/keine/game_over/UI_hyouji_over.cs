using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hyouji_over : MonoBehaviour
{
    public Fade_in_gemeover fade_over;
    public bool fadeIn;
    private SpriteRenderer oder = null;
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1, 1, 1, 0);
    }
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
