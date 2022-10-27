


//=============================================================================
//スタートボタン
//
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/18 作成
//
//=============================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start001 : MonoBehaviour {

    private InputAction _selectAction;

    //public bool titlle_delete;

    //public Fade_title_haikei1 Titlle;

    //public select sel;

    //public bool isSelect;

    //public Fade_in002 fadein;

    public bool isFade;

    private CP_move_input inputAction;

    private Vector2 selectVal;

    enum eTitleState {
        FROMSTART = 0,
        CONTINUE,
        EXIT
    }

    private enum eQuitState {
        NONE,
        YES,
        NO
    }


    private int state;                  // 選択肢のステート
    private int quitSelect;                 // 最終確認選択
    private int oldSelect;               // 戻らなきゃいけない選択肢

    private bool isDicision;    // 決定キーが押されたか
    private bool isExitGame;    // ゲーム終了するか

    private Fade_title_haikei1 pressAnyButton;
    private ObservedValue<bool> isPressAnyButton;   // プレスエニーボタンされた瞬間を検知する

    private bool canDicision;   // 決定キーを押しても良いか
                                // プレスエニーキーと同じフレームで来るとすぐに決定したことになってしまう

    private GameObject selectBoxObj;
    private RectTransform selectBoxRT;      // 選択枠のレクトトランスフォーム
    private GameObject startObj;
    private RectTransform startRT;          // 初めからのレクトトランスフォーム
    private GameObject continueObj;
    private RectTransform continueRT;       // 続きからのレクトトランスフォーム

    private GameObject kakuninObj;          // 確認画面のポップアップ
    private Image kakuninYes;
    private Image kakuninNo;
    void Awake() {
        inputAction = new CP_move_input();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {

        state = (int)eTitleState.FROMSTART;
        quitSelect = (int)eQuitState.NONE;
        oldSelect = state;

        canDicision = false;

        pressAnyButton = GameObject.Find("bg").GetComponent<Fade_title_haikei1>();

        isPressAnyButton = new ObservedValue<bool>(false);  // falseで初期化
        isPressAnyButton.OnValueChange += () => { canDicision = false; };

        selectBoxObj = GameObject.Find("cursor");
        selectBoxRT = selectBoxObj.GetComponent<RectTransform>();
        selectBoxObj.SetActive(false);
        startObj = GameObject.Find("UI_001");
        startRT = startObj.GetComponent<RectTransform>();
        startObj.SetActive(false);
        continueObj = GameObject.Find("UI_002");
        continueRT = continueObj.GetComponent<RectTransform>();
        continueObj.SetActive(false);
        kakuninObj = GameObject.Find("Kakunin");
        kakuninYes = kakuninObj.transform.Find("Kakunin_charbox_1").GetComponent<Image>();
        kakuninNo = kakuninObj.transform.Find("Kakunin_charbox_2").GetComponent<Image>();
        kakuninObj.SetActive(false);
    }

    private void OnEnable() {
        //アクションマップからアクションを取得
        inputAction.UI.Dicision.canceled += Dicision;
        inputAction.UI.Back.canceled += Back;
        _selectAction = inputAction.UI.Select;

        //---InputActionの有効化
        inputAction.UI.Enable();
    }

    private void OnDisable() {
        //---InputActionの無効化
        inputAction.UI.Disable();
    }

    // Update is called once per frame
    void Update() {
        //titlle_delete = Titlle.GetTitlle_delete();
        //isSelect = sel.NO1;

        //var current = Keyboard.current;
        isPressAnyButton.Value = pressAnyButton.fading;

        if (isPressAnyButton.Value) {
            selectBoxObj.SetActive(true);
            startObj.SetActive(true);
            continueObj.SetActive(true);
        }

        selectVal = _selectAction.ReadValue<Vector2>();
        //---少しでも倒されたら処理に入る
        if (state == (int)eTitleState.EXIT) {
            if (selectVal.x > 0.0f) {
                SelectRight();
            } else if (selectVal.x < 0.0f) {
                SelectLeft();
            }
        } else {
            if (selectVal.y > 0.0f) {
                SelectUp();
            } else if (selectVal.y < 0.0f) {
                SelectDown();
            }
        }

        if (state == (int)eTitleState.EXIT) {
            if (quitSelect == (int)eQuitState.NONE) {
                // 初期化
                quitSelect = (int)eQuitState.YES;
                state = (int)eTitleState.EXIT;
                kakuninObj.SetActive(true);
            }
        }

        if (isDicision) {
            switch (state) {
                case (int)eTitleState.FROMSTART:
                    // はじめから
                    SceneManagerFade.LoadSceneMain(0, 0);
                    isDicision = false;

                    break;
                case (int)eTitleState.CONTINUE:
                    // つづきから
                    SceneManagerFade.LoadSceneMain(ClearManager.GetNowWorld(), ClearManager.GetNowStage());
                    isDicision = false;

                    break;
                case (int)eTitleState.EXIT:
                    // ゲームをやめる
                    if (quitSelect == (int)eQuitState.NONE) {


                    } else if (quitSelect == (int)eQuitState.YES) {
                        // ゲームをやめる
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    } else if (quitSelect == (int)eQuitState.NO) {
                        // NOで戻ってきたときはリセット
                        quitSelect = (int)eQuitState.NONE;
                        kakuninObj.SetActive(false);
                        state = oldSelect;    // さっきまで選択してたところにステートを戻す
                    }
                    isDicision = false;

                    break;
            }
        }
    }

    private void Dicision(InputAction.CallbackContext obj) {
        if (!canDicision) {
            canDicision = true; // プレスエニーキーと同じフレームでここに入らない為のif
            return;
        }
        SoundManager2.Play(SoundData.eSE.SE_DICISION, SoundData.TitleAudioList);

        isDicision = true;
    }

    private void Back(InputAction.CallbackContext obj) {
        oldSelect = state;
        state = (int)eTitleState.EXIT;
    }


    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        if (state == (int)eTitleState.FROMSTART) {
            selectBoxRT.localPosition = startRT.localPosition;
        } else if (state == (int)eTitleState.CONTINUE) {
            selectBoxRT.localPosition = continueRT.localPosition;
        } else if (state == (int)eTitleState.EXIT) {

            // 本当にいいですかは選択枠を動かすのではなく
            // はいいいえの木の板の色味を黒っぽくすることで表現する
            if (quitSelect == (int)eQuitState.YES) {
                kakuninYes.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                kakuninNo.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
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
        state--;
        if (state < (int)eTitleState.FROMSTART) // 例外処理
        {
            state = (int)eTitleState.FROMSTART;
        }
        SelectBoxPosUpdete();   // 選択枠の更新
    }

    /// <summary>
    /// 下方向選択
    /// </summary>
    private void SelectDown() {
        // 音
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        state++;
        if (state > (int)eTitleState.CONTINUE)   // 例外処理
        {
            state = (int)eTitleState.CONTINUE;
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
