//=============================================================================
//
// “§‰ßˆ—
//
// ì¬“ú:2022/10/24
// ì¬Ò:ò—D÷
//
// <ŠJ”­—š—ğ>
// 2022/10/24 ì¬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentParts : MonoBehaviour
{
    private bool isColFoxWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ‘‹‚Ìó‘Ô‚ÅA‘‹‚É“–‚½‚Á‚Ä‚¢‚½‚çA“§‰ß
        if (CPData.isLens && isColFoxWindow)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isColFoxWindow = true;
            Debug.Log("“–‚½‚Á‚Ä‚¢‚é");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isColFoxWindow = false;
        }
    }
}
