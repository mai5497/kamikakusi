using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_fin : MonoBehaviour
{

    public Fox_text text;
    bool isText;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        isText = text.isTextFin;

        if(isText)
        {
         //   Destroy(this.gameObject);
        }
        
    }
}
