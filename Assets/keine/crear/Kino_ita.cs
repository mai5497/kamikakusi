using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kino_ita_over : MonoBehaviour
{
    // public GameObject oya;
    //�e�I�u�W�F�N�g�̍��W
    public Fade_in_gemeover fade_over;
    public Transform transform_oya;
    private SpriteRenderer oder = null;


    public bool fadeIn;
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        this.transform.position = transform_oya.parent.position;

        fadeIn = fade_over.fadeIn;
        if (!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = 4;
        }
    }
}
