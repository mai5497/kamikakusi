//=============================================================================
//
// Canvas�̕\����On�EOff�̐؂�ւ�
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

public class CanvasOnOff : MonoBehaviour
{
    private Canvas canvas;    // ���̃X�N���v�g������Canvas

    private bool isTglle;
    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();

        CanvasOff();    // �����͏����Ƃ�
        isTglle = false;
    }

    // Update is called once per frame
    void Update() {
        if (CPData.isKokkurisan) {
            CanvasOn();
            Keyboard _keyboard = Keyboard.current;
            if (_keyboard != null) {
                if (_keyboard.enterKey.wasReleasedThisFrame) {
                    isTglle = !isTglle;
                }
            }
            if (isTglle) {
                
                this.GetComponent<Kokkurisan>().isAnswer = true;
                this.GetComponent<Kokkurisan>().isNormal = true;
                this.GetComponent<Kokkurisan>().isFox = false;
            } else {
                this.GetComponent<Kokkurisan>().isAnswer = true;
                this.GetComponent<Kokkurisan>().isNormal = false;
                this.GetComponent<Kokkurisan>().isFox = true;
            }
        } else {
            CanvasOff();
        }
    }

    /// <summary>
    /// UI�̕\��������
    /// </summary>
    public void CanvasOff() {
        canvas.enabled = false;
    }

    /// <summary>
    /// UI�̕\��������
    /// </summary>
    public void CanvasOn() {
        canvas.enabled = true;
    }
}
