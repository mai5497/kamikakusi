//=============================================================================
//
// Canvasの表示のOn・Offの切り替え
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

public class CanvasOnOff : MonoBehaviour
{
    private Canvas canvas;    // このスクリプトをつけたCanvas

    private bool isKituneORHuman;   // 狐だったらtrue

    private CP_move_input PlayerActionAssets;        // InputActionのUIを扱う

    void Awake() {
        PlayerActionAssets = new CP_move_input();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();

        CanvasOff();    // 初期は消しとく
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
        //---InputActionの無効化
        PlayerActionAssets.Player.Disable();
    }

    /// <summary>
    /// UIの表示を消す
    /// </summary>
    public void CanvasOff() {
        canvas.enabled = false;
    }

    /// <summary>
    /// UIの表示をする
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
