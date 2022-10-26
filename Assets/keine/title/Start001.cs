


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

    private InputAction _dicisionAction, _selectAction;

    //public bool titlle_delete;

    //public Fade_title_haikei1 Titlle;

    //public select sel;

    public bool isSelect;

    public Fade_in002 fadein;

    public bool isFade;

    private CP_move_input inputAction;

    private Vector2 selectVal;

    enum eTitleState {
        FROMSTART = 0,
        CONTINUE
    }

    private int state;

    private bool isDicision;    // 決定キーが押されたか

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


    void Awake() {
        inputAction = new CP_move_input();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {

        state = (int)eTitleState.FROMSTART;

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
    }

    private void OnEnable() {
        //アクションマップからアクションを取得
        inputAction.UI.Dicision.canceled += Dicision;
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
        if (selectVal.y > 0.0f) {
            SelectUp();
        } else if (selectVal.y < 0.0f) {
            SelectDown();
        }

        if (isDicision) {
            switch (state) {
                case (int)eTitleState.FROMSTART:
                    // はじめから
                    SceneManagerFade.LoadSceneMain(0, 0);
                    break;
                case (int)eTitleState.CONTINUE:
                    // つづきから
                    SceneManagerFade.LoadSceneMain(ClearManager.GetNowWorld(), ClearManager.GetNowStage());
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

    private void SelectBoxPosUpdete() {
        /*
         * 選択枠の位置の更新
         * それぞれの文字のRectTransformと合わせることで同じ位置に表示ができる
         */
        if (state == (int)eTitleState.FROMSTART) {
            selectBoxRT.localPosition = startRT.localPosition;
        } else if (state == (int)eTitleState.CONTINUE) {
            selectBoxRT.localPosition = continueRT.localPosition;
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
}
