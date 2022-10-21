using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hyouji : MonoBehaviour
{
    public Fade_in_crear fade_crear;
    public bool fadeIn;
    private SpriteRenderer oder = null;
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1,1,1,0);
    }
    void Update()
    {
        fadeIn = fade_crear.fadeIn;

        if(!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = 6;
        }
    }
}
