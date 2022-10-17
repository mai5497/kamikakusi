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

    private bool isTglle;
    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();

        CanvasOff();    // 初期は消しとく
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
}
