//=============================================================================
//
// UI�̕\����On�EOff�̐؂�ւ�
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
using UnityEngine.UI;


public class UIOnOff : MonoBehaviour
{
    private Image image;    // ���̃X�N���v�g������UI��Image

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        UIOff();    // �����͏����Ƃ�
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UI�̕\��������
    /// </summary>
    public void UIOff() {
        image.enabled = false;
    }

    /// <summary>
    /// UI�̕\��������
    /// </summary>
    public void UIOn() {
        image.enabled = true;
    }
}
