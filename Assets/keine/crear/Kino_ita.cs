using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kino_ita_over : MonoBehaviour
{
    // public GameObject oya;
    //親オブジェクトの座標
    public Fade_in_gemeover fade_over;
    public Transform transform_oya;

    private SpriteRenderer oder = null;

    public bool fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position = transform_oya.position;
        this.transform.position = transform_oya.parent.position;

        fadeIn = fade_over.fadeIn;
    //    Debug.Log("asfdasfasfsf" + fadeIn);
        if (!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = 4;
        }


    }
}
