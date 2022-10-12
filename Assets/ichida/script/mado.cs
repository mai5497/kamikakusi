//=============================================================================
//
// 窓　後で中身引っ越すかもと思って、名前適当
//
// 作成日:2022/10/12
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class mado : MonoBehaviour {
    private GameObject fox; // 狐のオブジェクト
    private CircleCollider2D foxCol;    // 狐のコライダー
    private Fox _Fox;   //狐オブジェクトについている狐クラス

    private CircleCollider2D windowCol;

    private bool isLooking;

    public bool IsLooKing {
        get { 
            return isLooking; 
        }
    }

    // Start is called before the first frame update
    void Start() {
        fox = GameObject.FindWithTag("Fox");
        _Fox = fox.GetComponent<Fox>();
        foxCol = fox.GetComponent<CircleCollider2D>();

        windowCol = GetComponent<CircleCollider2D>();

        isLooking = false;
    }

    // Update is called once per frame
    void Update() {
        // 適当につけた窓の移動
        Keyboard _keyboard = Keyboard.current;
        if (_keyboard != null) {
            if (_keyboard.dKey.isPressed) {
                this.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
            }
            if (_keyboard.aKey.isPressed) {
                this.transform.position = new Vector3(this.transform.position.x - 0.01f, this.transform.position.y, this.transform.position.z);
            }
            if (_keyboard.wKey.isPressed) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.01f, this.transform.position.z);
            }
            if (_keyboard.sKey.isPressed) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.01f, this.transform.position.z);
            }

            if (_keyboard.shiftKey.isPressed) {
                isLooking = true;
            } else {
                isLooking = false;
            }
        }

        // 指定した範囲にモノがあるかの判定
        if (Physics2D.OverlapCircle(fox.transform.position, 0) == windowCol) {
            if (isLooking) {
                _Fox.isWindowColl = true;
                Debug.Log("きつね！");
            } else {
                _Fox.isWindowColl = false;
            }
        }
    }
}
