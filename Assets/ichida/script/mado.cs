//=============================================================================
//
// ���@��Œ��g�����z�������Ǝv���āA���O�K��
//
// �쐬��:2022/10/12
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/12 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class mado : MonoBehaviour {
    private GameObject fox; // �ς̃I�u�W�F�N�g
    private Fox _Fox;   //�σI�u�W�F�N�g�ɂ��Ă���σN���X


    // Start is called before the first frame update
    void Start() {
        fox = GameObject.FindWithTag("Fox");
        _Fox = fox.GetComponent<Fox>();
    }

    // Update is called once per frame
    void Update() {
        // �K���ɂ������̈ړ�
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

        // �w�肵���͈͂Ƀ��m�����邩�̔���
        if (Physics.OverlapSphere(fox.transform.position, 0).Length > 0) {
            _Fox.isWindowColl = true;
        }
    }
}
