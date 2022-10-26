using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    //private InputAction LeftStickSelect;    // InputActionのselectを扱う
    //private InputAction RightStickSelect;   // InputActionのselectを扱う

    private int pauseSelect;                // ポーズ選択
    private int quitSelect;                 // 最終確認選択

    //private int returnSelect;               // 戻らなきゃいけない選択肢
    //private bool isCalledOncce = false;     // ポーズ中一回しか呼ばない

    //private float UIBasePosx;               // UI表示の基準位置
    //private float UIMoveSpeed = 1.5f;       // UI表示を動かすときのスピード

    //private bool notShowPause;              // ポーズではないときにUIを表示しないようにするための変数

    private GameObject sousaChara;               // 操作説明
    private RectTransform sousaCharaRT;          // 操作説明のレクトトランスフォーム

    private GameObject sousa;              // 
    //private RectTransform optionRT;         // オプションのレクトトランスフォーム


    private GameObject returnGame;          // ゲームに戻る
    private RectTransform returnGameRT;     // ゲームに戻るのレクトトランスフォーム

    private GameObject returnSelect;        // ステージセレクト
    private RectTransform returnSelectRT;   // ステージセレクトのレクトトランスフォーム

    private GameObject option;              // オプション
    private RectTransform optionRT;         // オプションのレクトトランスフォーム


    private GameObject selectBox;           // 選択枠
    private RectTransform selectBoxRT;      // 選択枠のレクトトランスフォーム


    void Awake() {
        UIActionAssets = new CP_move_input();            // InputActionインスタンスを生成
    }

    private void Start() {
        quitSelect = (int)eQuitState.YES;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // 選択のモードの初期化
        //returnSelect = pauseSelect;                      // 選択のモードの初期化

        //UIBasePosx = 0.0f;  // UIの表示位置の基準位置初期化

        //notShowPause = false;   // trueが表示しないとき

        //isConfirm = false;         // 本当にやめるかが確定されたらtrue

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

        //---ゲームスタート時は非表示

    }

    private void OnEnable() {
        //---スティックの値を取るための設定
        //LeftStickSelect = UIActionAssets.UI.Select;
        //RightStickSelect = UIActionAssets.UI.Select;

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
        if (isDecision) {   // 決定キーが押されたら
            switch (pauseSelect) {
                case (int)eSTATEPAUSE.SOUSA:
                    isSousaShow = !isSousaShow;

                    sousa.SetActive(isSousaShow);
                 
                    isDecision = false;
                    break;
                case (int)eSTATEPAUSE.RETURNGAME:
                    CPData.isPose = false;
                    isDecision = false;
                    break;
                case (int)eSTATEPAUSE.RETURNSELECT:
                    
                    //ステージセレクトへ遷移
                    SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);

                    isDecision = false;
                    break;
                case (int)eSTATEPAUSE.OPTION:

                    break;
            }
        }
    }

    private void Select(InputAction.CallbackContext obj) {
        if (!Pause.isPause) {
            //if (SousaImage.GetComponent<Image>().enabled) {
            //    // ポーズ中ではないとき、操作方法表示中ははじく
            //    return;
            //}
        }

        //---左ステックのステック入力を取得
        Vector2 doLeftStick = Vector2.zero;
        doLeftStick = obj.ReadValue<Vector2>();
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            //---少しでも倒されたら処理に入る
            if (doLeftStick.x > 0.0f) {
                SelectRight();
            } else if (doLeftStick.x < 0.0f) {
                SelectLeft();
            }
        } else if(!isSousaShow){
            //---少しでも倒されたら処理に入る
            if (doLeftStick.y > 0.0f) {
                SelectUp();
            } else if (doLeftStick.y < 0.0f) {
                SelectDown();
            }
        }

    }

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void Dicision(InputAction.CallbackContext obj) {
        if (CPData.isPose) {
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

            //if (quitSelect == (int)eQuitState.YES) {
            //    SelectBox_2.GetComponent<RectTransform>().localPosition = QuitYes.GetComponent<RectTransform>().localPosition;
            //} else if (quitSelect == (int)eQuitState.NO) {
            //    SelectBox_2.GetComponent<RectTransform>().localPosition = QuitNo.GetComponent<RectTransform>().localPosition;
            //}
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
