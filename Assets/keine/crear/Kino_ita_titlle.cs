using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kino_ita_titlle : MonoBehaviour
{
    // public GameObject oya;
    //親オブジェクトの座標
    public Fade_in_crear fade_crear;
    public Transform transform_oya;
 //   private SpriteRenderer oder = null;
    private SpriteRenderer oder = null;

    public bool fadeIn;
    // Start is called before the first frame update


   
       

    void Start()
    {
        oder = GetComponent<SpriteRenderer>();
        oder.color = new Color(1, 1, 1, 0);
        oder.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // this.transform.position = transform_oya.position;
        this.transform.position = transform_oya.parent.position;

     //   fadeIn = fade_crear.fadeIn;
    //    Debug.Log("asfdasfasfsf" + fadeIn);
        if (!fadeIn)
        {
            oder.color = new Color(1, 1, 1, 1);
            oder.sortingOrder = -1;
        }


    }
}
