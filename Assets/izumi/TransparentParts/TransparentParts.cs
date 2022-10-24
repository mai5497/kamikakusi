//=============================================================================
//
// 透過処理
//
// 作成日:2022/10/24
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/24 作成
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
        // 窓の状態で、窓に当たっていたら、透過
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
            Debug.Log("当たっている");
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
