using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hyouji_over1 : MonoBehaviour
{
    public Fade_in_gemeover fade_over;
    private SpriteRenderer oder = null;

    public bool fadeIn;
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
            oder.sortingOrder = 9;
        }
    }
}
