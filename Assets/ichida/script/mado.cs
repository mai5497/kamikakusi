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
    private Fox _Fox;   //狐オブジェクトについている狐クラス


    // Start is called before the first frame update
    void Start() {
        fox = GameObject.FindWithTag("Fox");
        _Fox = fox.GetComponent<Fox>();
    }

    // Update is called once per frame
    void Update() {
        // 適当につけた窓の移動
        Keyboard _keyboard = Keyboard.current;
        if (_keyboard != null) {
            if (_keyboard.dKey.wasPressedThisFrame) {
                this.transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
            }
            if (_keyboard.aKey.wasPressedThisFrame) {
                this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
            }
            if (_keyboard.wKey.wasPressedThisFrame) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);
            }
            if (_keyboard.sKey.wasPressedThisFrame) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.1f, this.transform.position.z);
            }

            if (_keyboard.shiftKey.wasPressedThisFrame) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.1f, this.transform.position.z);
            }
        }

        // 指定した範囲にモノがあるかの判定
        if (Physics.OverlapSphere(fox.transform.position, 0).Length > 0) {
            _Fox.isWindowColl = true;
        }
    }
}
