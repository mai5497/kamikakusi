
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
    // 当たり判定初期座標
    private Vector2 hitInitPos;
    // 当たり判定の補正
    private float hitCameraHosei = 0.35f;
    private float hitPlayerHosei = 0.1f;
    // プレイヤーオブジェクト
    private GameObject cpObj;
    // カメラとプレイヤーの距離
    private float cameraToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        switch (this.gameObject.layer) 
        {
            // Middle1
            case 7:
                hitCameraHosei = GameObject.Find("CameraMiddle1").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle2
            case 9:
                hitCameraHosei = GameObject.Find("CameraMiddle2").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle3
            case 10:
                hitCameraHosei = GameObject.Find("CameraMiddle3").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
        }
        
        hitInitPos = this.GetComponent<BoxCollider2D>().offset;

        cpObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        // 当たり判定の座標変更
        if (this.gameObject.layer == 7 || this.gameObject.layer == 9 || this.gameObject.layer == 10)
        {
            Vector2 hitPos;
            cameraToPlayer = Camera.main.transform.position.x - cpObj.transform.position.x;
            // 左を向いていたら
            if (cpObj.transform.localScale.x < 0)
            {
                hitPos.x = hitInitPos.x - (cameraToPlayer * hitCameraHosei + cpObj.transform.position.x * hitPlayerHosei);
                hitPos.y = hitInitPos.y;
                this.GetComponent<BoxCollider2D>().offset = hitPos;
            }
            // 右を向いていたら
            else if (cpObj.transform.localScale.x > 0)
            {
                hitPos.x = hitInitPos.x + (cameraToPlayer * hitCameraHosei + cpObj.transform.position.x * hitPlayerHosei);
                hitPos.y = hitInitPos.y;
                this.GetComponent<BoxCollider2D>().offset = hitPos;
            }
        }

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
