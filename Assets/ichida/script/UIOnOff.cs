//=============================================================================
//
// UIの表示のOn・Offの切り替え
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
using UnityEngine.UI;


public class UIOnOff : MonoBehaviour
{
    private Image image;    // このスクリプトをつけたUIのImage

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        UIOff();    // 初期は消しとく
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UIの表示を消す
    /// </summary>
    public void UIOff() {
        image.enabled = false;
    }

    /// <summary>
    /// UIの表示をする
    /// </summary>
    public void UIOn() {
        image.enabled = true;
    }
}
