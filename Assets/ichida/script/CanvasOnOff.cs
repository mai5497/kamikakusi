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

public class CanvasOnOff : MonoBehaviour
{
    private Canvas canvas;    // ���̃X�N���v�g������Canvas

    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();
        CanvasOff();    // �����͏����Ƃ�
    }

    // Update is called once per frame
    void Update() {
        if (CPData.isHint) {
            CanvasOn();
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
