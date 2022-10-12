//=============================================================================
//
// ヒント表示のUIとオブジェクトの架け橋をする
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


public class ShowHintManager : MonoBehaviour {
    private Image[] hintsimage = new Image[5];          // 最大ヒント数5個だろうって思ってるから５個
    private GameObject[] hintsobj = new GameObject[5];  // タグ検索、スクリプト取得が楽するため一回UIをゲームオブジェクトで取得
    private HintUI[] _hintUIs = new HintUI[5];          // ここでUIのヒント番号を管理している
    private UIOnOff[] _UIOnOff = new UIOnOff[5];        // UIの表示非表示を管理する為取得

    private GameObject hintbox;
    private UIOnOff hintOnOff;

    // Start is called before the first frame update
    void Start() {
        hintsobj = GameObject.FindGameObjectsWithTag("UIHint"); // ヒントタグが付いた全てのオブジェクトを取得する

        //----- 検索したUIのオブジェクトをヒント番号順に並べ替え -----
        for (int i = 0; i < hintsobj.Length; i++) {    // ヒント番号はHintUiクラスが持っている為取得
            _hintUIs[i] = hintsobj[i].GetComponent<HintUI>();
        }
        GameObject _work;    // 並べ替え用に使うワーク領域 
        HintUI _hintUI;
        // ヒント番号順に並べ替え
        for (int j = 0; j < hintsobj.Length; j++) {
            for (int i = 0; i < hintsobj.Length; i++) {
                if (_hintUIs[i].HintNum != i) {
                    // 三点交換
                    _work = hintsobj[_hintUIs[i].HintNum];      // 一時退避
                    _hintUI = _hintUIs[_hintUIs[i].HintNum];

                    hintsobj[_hintUIs[i].HintNum] = hintsobj[i];
                    _hintUIs[_hintUIs[i].HintNum] = _hintUIs[i];

                    hintsobj[i] = _work;
                    _hintUIs[i] = _hintUI;
                }
            }
        }
        // 並べ替え後のUIのImageを取得する
        for (int i = 0; i < hintsobj.Length; i++) {
            hintsimage[i] = hintsobj[i].GetComponent<Image>();
            _UIOnOff[i] = hintsobj[i].GetComponent<UIOnOff>();
        }

        // ヒントのボックスの取得
        hintbox = GameObject.Find("hintbar");
        hintOnOff = hintbox.GetComponent<UIOnOff>();
    }

    /// <summary>
    /// 渡されたヒント番号に対応するUIを表示する
    /// </summary>
    /// <param name="_hintNum"></param>
    public void ShowHintUI(int _hintNum) {
        hintOnOff.UIOn();
        _UIOnOff[_hintNum].UIOn();
    }


    /// <summary>
    /// 渡されたヒント番号に対応するUIを非表示にする
    /// </summary>
    /// <param name="_hintNum"></param>
    public void HideHintUI(int _hintNum) {
        hintOnOff.UIOff();
        _UIOnOff[_hintNum].UIOff();
    }
}
