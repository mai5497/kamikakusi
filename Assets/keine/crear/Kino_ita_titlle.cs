using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kino_ita_titlle : MonoBehaviour
{
    //親オブジェクトの座標
    public Fade_in_crear fade_crear;
    public Transform transform_oya;
    private SpriteRenderer oder = null;

    public bool fadeIn;
       
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1, 1, 1, 0);
        oder.sortingOrder = -1;
    }
    void Update()
    {
        this.transform.position = transform_oya.parent.position;

        if (!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = -1;
        }
    }
}
