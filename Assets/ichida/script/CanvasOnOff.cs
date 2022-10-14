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

public class CanvasOnOff : MonoBehaviour
{
    private Canvas canvas;    // このスクリプトをつけたCanvas

    // Start is called before the first frame update
    void Start() {
        canvas = GetComponent<Canvas>();
        CanvasOff();    // 初期は消しとく
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
