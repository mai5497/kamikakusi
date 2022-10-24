//=============================================================================
//
// ���ߏ���
//
// �쐬��:2022/10/24
// �쐬��:��D��
//
// <�J������>
// 2022/10/24 �쐬
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
        // ���̏�ԂŁA���ɓ������Ă�����A����
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
            Debug.Log("�������Ă���");
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
