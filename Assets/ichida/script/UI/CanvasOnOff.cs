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

    private bool isKituneORHuman;   // �ς�������true

    private CP_move_input PlayerActionAssets;        // InputAction��UI������

    void Awake() {
        PlayerActionAssets = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();

        CanvasOff();    // �����͏����Ƃ�
        isKituneORHuman = false;
    }

    private void OnEnable() {
        PlayerActionAssets.Player.HintSwitch.started += SwitchHint;

        PlayerActionAssets.Player.Enable();
    }

    // Update is called once per frame
    void Update() {
        if (CPData.isKokkurisan) {
            CanvasOn();
            if (isKituneORHuman) {
                this.GetComponent<Kokkurisan>().isAnswer = true;
                this.GetComponent<Kokkurisan>().isNormal = false;
                this.GetComponent<Kokkurisan>().isFox = true;
            } else {
                this.GetComponent<Kokkurisan>().isAnswer = true;
                this.GetComponent<Kokkurisan>().isNormal = true;
                this.GetComponent<Kokkurisan>().isFox = false;
            }
        } else {
            CanvasOff();
            this.GetComponent<Kokkurisan>().isAnswer = false;
        }
    }

    private void OnDisable() {
        //---InputAction�̖�����
        PlayerActionAssets.Player.Disable();
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

    private void SwitchHint(InputAction.CallbackContext obj) {
        if (CPData.isKokkurisan) {
            if (obj.ReadValue<float>() > 0) {
                isKituneORHuman = true;
            } else {
                isKituneORHuman = false;
            }
        }
    }
}
