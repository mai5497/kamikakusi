//=============================================================================
//
// ’†ŒiƒJƒƒ‰(https://kinokorori.hatenablog.com/entry/2019/01/16/000000)
//
// ì¬“ú:2022/10/20
// ì¬Ò:ò—D÷
//
// <ŠJ”­—š—ğ>
// 2022/10/20 ì¬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiddle : MonoBehaviour
{
    private GameObject player;
    private Vector3 startPlayerOffset;
    private Vector3 startCameraPos;
    public float rate = 0.12f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPlayerOffset = player.transform.position;
        startCameraPos = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = (player.transform.position - startPlayerOffset) * rate;
        this.transform.position = startCameraPos + v;
    }
}
