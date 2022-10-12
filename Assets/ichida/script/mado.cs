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
    private CircleCollider2D foxCol;    // �ς̃R���C�_�[
    private Fox _Fox;   //�σI�u�W�F�N�g�ɂ��Ă���σN���X

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
        // �K���ɂ������̈ړ�
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

        // �w�肵���͈͂Ƀ��m�����邩�̔���
        if (Physics2D.OverlapCircle(fox.transform.position, 0) == windowCol) {
            if (isLooking) {
                _Fox.isWindowColl = true;
                Debug.Log("���ˁI");
            } else {
                _Fox.isWindowColl = false;
            }
        }
    }
}
