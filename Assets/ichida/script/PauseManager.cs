using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //----- 定数定義 -----
    private enum eSTATEPAUSE    // ポーズから行う操作
    {
        RETURNGAME = 0, // ゲームに戻る
        RETURNSELECT,    // セレクト画面に戻る
        SOUSA,           // 操作方法
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
    [SerializeField]
    private GameObject pauseframe;
    private GameObject PauseFrame;
    [SerializeField]
    private GameObject pausecharacter;
    private GameObject PauseCharacter;
    [SerializeField]
    private GameObject backgame;
    private GameObject BackGame;
    [SerializeField]
    private GameObject gameend;
    private GameObject GameEnd;
    [SerializeField]
    private GameObject sousachara;
    private GameObject SousaChara;
    [SerializeField]
    private GameObject sousaimage;
    private GameObject SousaImage;
    [SerializeField]
    private GameObject optionchara;
    private GameObject OptionChara;
    [SerializeField]
    private GameObject selectbox_1;
    private GameObject SelectBox_1; // 通常の選択で使用
    [SerializeField]
    private GameObject selectbox_2;
    private GameObject SelectBox_2; // 最終確認で使用
    [SerializeField]
    private GameObject decision;
    private GameObject Decision;

    [SerializeField]
    private GameObject quitquestion;
    private GameObject QuitQuestion;
    [SerializeField]
    private GameObject quityes;
    private GameObject QuitYes;
    [SerializeField]
    private GameObject quitno;
    private GameObject QuitNo;

    Canvas canvas;                          // 表示に使用するCanvas

    private bool isDecision;                // 決定
    private bool isConfirm;                   // ファイナルアンサー？

    private CP_move_input UIActionAssets;        // InputActionのUIを扱う
    //private InputAction LeftStickSelect;    // InputActionのselectを扱う
    //private InputAction RightStickSelect;   // InputActionのselectを扱う

    private int pauseSelect;                // ポーズ選択
    private int quitSelect;                 // 最終確認選択
    private int returnSelect;               // 戻らなきゃいけない選択肢
    private bool isCalledOncce = false;     // ポーズ中一回しか呼ばない

    private float UIBasePosx;               // UI表示の基準位置
    private float UIMoveSpeed = 1.5f;       // UI表示を動かすときのスピード

    private bool notShowPause;              // ポーズではないときにUIを表示しないようにするための変数


    void Awake() {
        UIActionAssets = new CP_move_input();            // InputActionインスタンスを生成
    }

    private void Start() {
        quitSelect = (int)eQuitState.YES;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // 選択のモードの初期化
        returnSelect = pauseSelect;                      // 選択のモードの初期化

        UIBasePosx = 0.0f;  // UIの表示位置の基準位置初期化

        notShowPause = false;   // trueが表示しないとき

        isConfirm = false;         // 本当にやめるかが確定されたらtrue


        //---キャンバスを指定
        canvas = GetComponent<Canvas>();

        //---実態化
        PauseFrame = Instantiate(pauseframe);           // ポーズ枠
        PauseCharacter = Instantiate(pausecharacter);   // ポーズの文字
        BackGame = Instantiate(backgame);               // ゲームに戻るの文字
        GameEnd = Instantiate(gameend);                 // ゲームやめるの文字
        SousaChara = Instantiate(sousachara);           // 操作方法文字
        SousaImage = Instantiate(sousaimage);           // 操作方法画像
        OptionChara = Instantiate(optionchara);                   // オプションの文字
        SelectBox_1 = Instantiate(selectbox_1);         // 選択枠
        SelectBox_2 = Instantiate(selectbox_2);
        Decision = Instantiate(decision);               // 決定操作説明文字
        QuitQuestion = Instantiate(quitquestion);       // 本当にやめますか文字
        QuitYes = Instantiate(quityes);                 // 本当にやめますかイエス
        QuitNo = Instantiate(quitno);                   // 本当にやめますかのー

        //---キャンバスの子にする
        PauseFrame.transform.SetParent(this.transform, false);              // ポーズ枠
        SelectBox_1.transform.SetParent(this.canvas.transform, false);      // 選択枠
        SelectBox_2.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);   // ポーズの文字
        BackGame.transform.SetParent(this.canvas.transform, false);         // ゲームに戻るの文字
        GameEnd.transform.SetParent(this.canvas.transform, false);          // ゲームやめるの文字
        SousaChara.transform.SetParent(this.canvas.transform, false);       // 操作方法
        SousaImage.transform.SetParent(this.canvas.transform, false);       // 操作方法
        OptionChara.transform.SetParent(this.canvas.transform, false);           // オプションの文字
        Decision.transform.SetParent(this.canvas.transform, false);         // 決定操作説明文字
        QuitQuestion.transform.SetParent(this.canvas.transform, false);     // 本当にやめますか文字
        QuitYes.transform.SetParent(this.canvas.transform, false);          // 本当にやめますかイエス
        QuitNo.transform.SetParent(this.canvas.transform, false);           // 本当にやめますかのー



        //---ゲームスタート時は非表示
        /*
         * TODO：作る！
         */

    }

    private void OnEnable() {
        //---Actionイベントを登録
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        //UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }


    // Update is called once per frame
    void Update() {

        //----- ポーズのUIを出さない為のif(下の条件式が長くなっていやだったから別で書いちゃった) -----
        notShowPause = false; // trueが表示しないとき
        //if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial1 ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial2 ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial3) {
        //    notShowPause = true;
        //}

        if (!Pause.isPause || notShowPause) {
            //----- ポーズ中ではない時の処理 -----
            //音楽再生
            //if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE) {
            //    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
            //    if (kitchenBgmObject) {
            //        kitchenBgmObject.GetComponent<AudioSource>().UnPause();
            //    }
            //}

            // 非表示
            /*
             * TODO：作る
             */

            isCalledOncce = false;

            return;
        } else if (Pause.isPause) {
            //----- ポーズ中の処理 -----
            if (!isCalledOncce) {
                //----- ポーズに入ったら一回のみする -----
                // ポーズ中になったら表示
                /*
                 * TODO：作る
                 */

                //音楽停止
                //if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE) {
                //    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                //    if (kitchenBgmObject) {
                //        kitchenBgmObject.GetComponent<AudioSource>().Pause();
                //    }
                //}
            }
            isCalledOncce = true;   // もう上のif文に入らないようにフラグを反転
        }


        //----- ポーズ中の処理 -----
        //---選択
        //オプションが開いてる間は無効にする
        if (false) { 
            // オプション中じゃないときに操作説明を表示

            // ポーズになったら選択させる
        } else {
            // オプション中はオプション中の操作説明があるのでけす
        }


        //---選択しているものが何かで分岐
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
            //----- ゲームに戻る -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // ポーズ解除
                Pause.isPause = false;
                // 決定解除
                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            //----- 最終確認 -----
            //SelectBox_2.GetComponent<Image>().enabled = true;

            //---本当にいいかの確認を出すための決定
            if (isDecision) {
                // 決定音
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                pauseSelect = returnSelect;

                isConfirm = true;
                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
            if (isDecision) {
                // 決定音
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                // とりあえず決定押されるたびに反転
                //SousaImage.GetComponent<Image>().enabled = !SousaImage.GetComponent<Image>().enabled;

                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
            //----- オプション -----
            if (isDecision) // 選択を確定
            {
                // 決定音
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // オプションマネージャーをアクティブにする

                // 決定解除
                isDecision = false;
            }
        }
    }


    /// <summary>
    /// 左スティック
    /// </summary>
    /// <param name="obj"></param>
    //private void OnLeftStick(InputAction.CallbackContext obj) {
    //    if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause) {
    //        if (SousaImage.GetComponent<Image>().enabled) {
    //            // ポーズ中ではないとき、操作方法表示中ははじく
    //            return;
    //        }
    //    }

    //    //---左ステックのステック入力を取得
    //    Vector2 doLeftStick = Vector2.zero;
    //    doLeftStick = LeftStickSelect.ReadValue<Vector2>();
    //    if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
    //        //---少しでも倒されたら処理に入る
    //        if (doLeftStick.x > 0.0f) {
    //            SelectRight();
    //        } else if (doLeftStick.x < 0.0f) {
    //            SelectLeft();
    //        }
    //    } else {
    //        //---少しでも倒されたら処理に入る
    //        if (doLeftStick.y > 0.0f) {
    //            SelectUp();
    //        } else if (doLeftStick.y < 0.0f) {
    //            SelectDown();
    //        }
    //    }
    //}

    //private void OnRightStick(InputAction.CallbackContext obj) {
    //    if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause) {
    //        if (SousaImage.GetComponent<Image>().enabled) {
    //            // ポーズ中ではないときははじく
    //            return;
    //        }
    //    }

    //    //---右ステックのステック入力を取得
    //    Vector2 doRightStick = Vector2.zero;
    //    doRightStick = RightStickSelect.ReadValue<Vector2>();
    //    if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
    //        //---少しでも倒されたら処理に入る
    //        if (doRightStick.x > 0.0f) {
    //            SelectRight();
    //        } else if (doRightStick.x < 0.0f) {
    //            SelectLeft();
    //        }
    //    } else {
    //        //---少しでも倒されたら処理に入る
    //        if (doRightStick.y > 0.0f) {
    //            SelectUp();
    //        } else if (doRightStick.y < 0.0f) {
    //            SelectDown();
    //        }
    //    }
    //}

    /// <summary>
    /// 決定ボタン
    /// </summary>
    //private void OnDecision(InputAction.CallbackContext obj) {
    //    if (Pause.isPause || !SaveManager.canSave || !Warp.shouldWarp || !GameData.isFadeIn || !GameData.isFadeOut || !GameOver.GameOverFlag || notShowPause) {
    //        // ポーズ中ではないときははじく
    //        isDecision = true;
    //    }
    //}

    /// <summary>
    /// 選択枠の座標更新
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        //if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = SousaChara.GetComponent<RectTransform>().localPosition;

        //} else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {

        //    if (quitSelect == (int)eQuitState.YES) {
        //        SelectBox_2.GetComponent<RectTransform>().localPosition = QuitYes.GetComponent<RectTransform>().localPosition;
        //    } else if (quitSelect == (int)eQuitState.NO) {
        //        SelectBox_2.GetComponent<RectTransform>().localPosition = QuitNo.GetComponent<RectTransform>().localPosition;
        //    }
        //}
    }

    /// <summary>
    /// 上方向選択
    /// </summary>
    private void SelectUp() {
        // 音
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES) {
            quitSelect = (int)eQuitState.YES;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }
}
