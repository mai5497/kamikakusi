using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Over : MonoBehaviour
{
    // Start is called before the first frame update

 // public  Transform transform_player;
    private Vector2 Pos;
    public Transform transform_oya;

    private RectTransform rec;


    void Start()
    {
        //  transform = this.transform.position.x;    
        Pos = new Vector2(this.transform.position.x, this.transform.position.y);
        rec = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log(rec.position.x);
        //   Pos = new Vector2(this.transform.position.x, this.transform.position.y);
        //  Pos.x  = transform_player.transform.position.x;
        Pos.x = transform_oya.parent.position.x+ rec.position.x;
        Pos.y = transform_oya.parent.position.y + rec.position.y;
    }
}
