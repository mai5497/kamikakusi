using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    //----- 定数定義 -----
    private enum eSTATEPAUSE    // ポーズから行う操作
    {
        SOUSA = 0, // 操作
        RETURNGAME,           // 操作方法
        RETURNSELECT,    // セレクト画面に戻る
        OPTION,         // オプション
        QUITQUESTION,   // 本当にやめますか

        MAX_STATE
    }
    private enum eQuitState {
        NONE,
        YES,
        NO
    }

    //----- 変数定義 -----
    //---表示するUI
    private bool isDecision;                // 決定
    private bool isSousaShow;                  // 操作説明が出てるとき
    //private bool isConfirm;                   // ファイナルアンサー？

    private CP_move_input UIActionAssets;        // InputActionのUIを扱う

    private int pauseSelect;                // ポーズ選択
    private int quitSelect;                 // 最終確認選択

    private int oldSelect;               // 戻らなきゃいけない選択肢
    //private bool isCalledOncce = false;     // ポーズ中一回しか呼ばない

    //private bool notShowPause;              // ポーズではないときにUIを表示しないようにするための変数

    private GameObject sousaChara;               // 操作説明文字
    private RectTransform sousaCharaRT;          // 操作説明文字のレクトトランスフォーム

    private GameObject sousa;              // 操作説明してるコントローラーの画像

    private GameObject returnGame;          // ゲームに戻る
    private RectTransform returnGameRT;     // ゲームに戻るのレクトトランスフォーム

    private GameObject returnSelect;        // ステージセレクト
    private RectTransform returnSelectRT;   // ステージセレクトのレクトトランスフォーム

    private GameObject option;              // オプション
    private RectTransform optionRT;         // オプションのレクトトランスフォーム

    private GameObject selectBox;           // 選択枠
    private RectTransform selectBoxRT;      // 選択枠のレクトトランスフォーム

    private GameObject kakunin;             // ゲームを終了しますか
    private Image kakuninYes;          // 確認画面のイエス
    private Image kakuninNo;           // 確認画面のノー

    void Awake() {
        UIActionAssets = new CP_move_input();            // InputActionインスタンスを生成
    }

    private void Start() {
        quitSelect = (int)eQuitState.NONE;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // 選択のモードの初期化
        oldSelect = pauseSelect;

        sousaChara = transform.Find("Pose_charbox_1").gameObject;
        sousaCharaRT = sousaChara.GetComponent<RectTransform>();
        sousa = transform.Find("Sousa").gameObject;
        isSousaShow = false;
        sousa.SetActive(isSousaShow); // 非表示
        returnGame = transform.Find("Pose_charbox_2").gameObject;
        returnGameRT = returnGame.GetComponent<RectTransform>();
        returnSelect = transform.Find("Pose_charbox_3").gameObject;
        returnSelectRT = returnSelect.GetComponent<RectTransform>();
        option = transform.Find("Pose_charbox_4").gameObject;
        optionRT = option.GetComponent<RectTransform>();
        selectBox = transform.Find("Pose_selectbox").gameObject;
        selectBoxRT = selectBox.GetComponent<RectTransform>();
        kakunin = transform.Find("Kakunin").gameObject;
        kakuninYes = kakunin.transform.Find("Kakunin_charbox_1").GetComponent<Image>();
        kakuninNo = kakunin.transform.Find("Kakunin_charbox_2").GetComponent<Image>();
        kakunin.SetActive(false);   // 非表示
    }

    private void OnEnable() {
        //---Actionイベントを登録
        UIActionAssets.UI.Select.started += Select;
        UIActionAssets.UI.Dicision.canceled += Dicision;

        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }


    // Update is called once per frame
    void Update() {
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            if (isDecision) {   // 決定キーが押されたら
                if (quitSelect == (int)eQuitState.YES) {

                } else {
                    kakunin.SetActive(false);   // 確認画面消す
                }
                pauseSelect = oldSelect;    // さっきまで選択してたところ(セレクトへ戻る)にステートを戻す
            }
        }

        if (isDecision) {   // 決定キーが押されたら
            switch (pauseSelect) {
                //----- 操作説明表示 -----
                case (int)eSTATEPAUSE.SOUSA:
                    isSousaShow = !isSousaShow;

                    sousa.SetActive(isSousaShow);
                 
                    isDecision = false;
                    break;
                //----- ゲームに戻る(ポーズ画面解除) -----
                case (int)eSTATEPAUSE.RETURNGAME:
                    Pause.isPause = false;
                    isDecision = false;
                    break;
                //----- セレクト画面に戻る -----
                case (int)eSTATEPAUSE.RETURNSELECT:
                    if (quitSelect == (int)eQuitState.NONE) {// セレクト画面に戻るが選択されたとき
                        // 初期化
                        quitSelect = (int)eQuitState.YES;
                        oldSelect = pauseSelect;
                        pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;
                        kakunin.SetActive(true);
                    } else if (quitSelect == (int)eQuitState.YES) {
                        //ステージセレクトへ遷移
                        Pause.isPause = false;
                        SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);
                    } else if (quitSelect == (int)eQuitState.NO) {
                        // NOで戻ってきたときはリセット
                        quitSelect = (int)eQuitState.NONE;
                    }
                    isDecision = false;
                    break;
                case (int)eSTATEPAUSE.OPTION:
                    isDecision = false;
                    break;
            }
        }
    }

    private void Select(InputAction.CallbackContext obj) {
        if(!Pause.isPause) {
            return; 
        }
        //---スティック入力を取得
        Vector2 doStick;
        doStick = obj.ReadValue<Vector2>();
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            //---少しでも倒されたら処理に入る
            if (doStick.x > 0.0f) {
                SelectRight();
            } else if (doStick.x < 0.0f) {
                SelectLeft();
            }
        } else if(!isSousaShow){
            //---少しでも倒されたら処理に入る
            if (doStick.y > 0.0f) {
                SelectUp();
            } else if (doStick.y < 0.0f) {
                SelectDown();
            }
        }

    }

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void Dicision(InputAction.CallbackContext obj) {
        if (Pause.isPause) {
            // ポーズ中ではないときははじく
            isDecision = true;
        }
    }

    /// <summary>
    /// 選択枠の座標更新
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
            selectBoxRT.localPosition = returnGameRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
            selectBoxRT.localPosition = sousaCharaRT.localPosition;
        }else if(pauseSelect == (int)eSTATEPAUSE.RETURNSELECT) {
            selectBoxRT.localPosition = returnSelectRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
            selectBoxRT.localPosition = optionRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {

            // 本当にいいですかは選択枠を動かすのではなく
            // はいいいえの木の板の色味を黒っぽくすることで表現する
            if (quitSelect == (int)eQuitState.YES) {
                kakuninYes.color = new Color(1.0f,1.0f,1.0f,1.0f);
                kakuninNo.color = new Color(0.7f,0.7f,0.7f,1.0f);
            } else if (quitSelect == (int)eQuitState.NO) {
                kakuninYes.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
                kakuninNo.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    /// <summary>
    /// 上方向選択
    /// </summary>
    private void SelectUp() {
        // 音
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        pauseSelect--;
        if (pauseSelect < 0) // 例外処理
        {
            pauseSelect = 0;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 下方向選択
    /// </summary>
    private void SelectDown() {
        // 音
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        pauseSelect++;
        if (pauseSelect >= (int)eSTATEPAUSE.OPTION)   // 例外処理
        {
            pauseSelect = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }


    /// <summary>
    /// 選択右
    /// </summary>
    private void SelectRight() {
        // 音
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect++;
        if (quitSelect > (int)eQuitState.NO) {
            quitSelect = (int)eQuitState.NO;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 選択左
    /// </summary>
    private void SelectLeft() {
        // 音
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES) {
            quitSelect = (int)eQuitState.YES;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }
}
