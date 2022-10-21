//=============================================================================
//
//プレイヤーデータ
//
// 作成日:2022/10/11
// 作成者:八木橋慧音
// 編集者:伊地田真衣
//
// <開発履歴>
// 2022/10/12 作成
// 2022/10/13 マージのためにいろいろ変更
// 2022/10/21 InputPlayer消すために入力回り変更
//
//=============================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CP_move01 : MonoBehaviour {
    enum eAnimState {   // アニメーションのステート
        NONE = 0,
        IDLE,
        WALK,
        FOXWINDOW,
    }

    [Header("移動スピード")]
    public float fSpeed = 0.04f;    // プレイヤーの移動と窓の移動スピード兼ねてる

    //アクション取得用
    private InputAction _moveAction;
    private CP_move_input PlayerAction;        // InputActionを扱う
    private CP_move_input UIAction;        // InputActionを扱う

    private eAnimState animState;   // アニメーションステート

    private SpriteRenderer sr;  // プレイヤーのspriterenderer
    private float oldMoveVal;

    private FoxByakko _FoxByakko;

    GameObject wallObj_left;
    GameObject wallObj_right;

    public bool canMoveLeft;
    public bool canMoveRight;

    public float Wall_player_left = 0.0f;
    public float Wall_player_right = 0.0f;

    Vector2 wallLeftPos;
    Vector2 wallRightPos;

    void Start() {
        animState = eAnimState.NONE;
        sr = this.GetComponent<SpriteRenderer>();
        oldMoveVal = 0.0f;

        _FoxByakko = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();

        wallObj_left = GameObject.FindGameObjectWithTag("TestWallLeft");
        wallObj_right = GameObject.FindGameObjectWithTag("TestWallRight");

        wallLeftPos = wallObj_left.transform.position;
        wallRightPos = wallObj_right.transform.position;


        Wall_player_left = this.transform.localScale.x / 2 + wallObj_left.transform.localScale.x / 2 + 0.04f;
        Wall_player_right = this.transform.localScale.x / 2 + wallObj_right.transform.localScale.x / 2 + 0.04f;


    }

    void Awake() {
        PlayerAction = new CP_move_input();            // InputActionインスタンスを生成
        UIAction = new CP_move_input();
    }

    private void OnEnable() {
        //---Actionイベントを登録
        _moveAction = PlayerAction.Player.Move;
        PlayerAction.Player.Pose.canceled += OnPoseKey;
        PlayerAction.Player.Kitune.canceled += OnLens;
        PlayerAction.Player.Cyuusi.started += LookStart;
        PlayerAction.Player.Cyuusi.canceled += LookFin;

        PlayerAction.Player.HintClose.canceled += CloseHint;

        UIAction.UI.KeyKokkurisan.started += OpenHintKey;
        UIAction.UI.ButtonKokkurisan.started += OpenHintButton;


        //---InputActionの有効化
        UIAction.UI.Enable();
        PlayerAction.Player.Enable();
    }

    private void OnDisable() {
        _moveAction.Disable();

        //---InputActionの無効化
        UIAction.UI.Disable();
        PlayerAction.Player.Disable();
    }


    void Update() {
        if (_FoxByakko.isClear || CPData.lookCnt < 1) {   // クリアしたら
            CPData.isLook = false;  // 注視やめ
            CPData.isLens = false;  // 窓使用やめ

            _moveAction.Disable();

            return;
        }

        if (CPData.isLens) {    // 窓使用中になったら
            /*
             * アニメーションの再生
             */
            animState = eAnimState.NONE;// 再生終了したらステートをNONEとかにする(FOXWINDOW以外)
        }

        if (!CPData.isLens) {   // レンズ使用中処理か移動処理か
            if (!CPData.isKokkurisan && !CPData.isObjNameUI) {
                //プレイヤーの移動処理
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Vector2 move = _moveAction.ReadValue<Vector2>();

                // プレイヤー反転処理
                if(move.x > -1 && oldMoveVal > 0) {
                    this.transform.localScale = new Vector3(1,1,1);
                } else if(move.x < 0 && oldMoveVal < 0){
                    this.transform.localScale = new Vector3(-1,1,1);
                }
                oldMoveVal = move.x;


                bool stopLeft = false;
                bool stopRight = false;

                if (wallLeftPos.x + Wall_player_left >= this.transform.position.x) {
                    stopLeft = true;
                }

                if (wallRightPos.x - Wall_player_right <= this.transform.position.x) {
                    stopRight = true;
                }


                if (move.x > -0.0f && !stopRight) {
                    canMoveLeft = true;
                    transform.Translate(
                            move.x * fSpeed * Time.deltaTime,
                            0.0f,
                            0.0f);
                }
                if (move.x < 0.0f && !stopLeft) {
                    canMoveRight = true;
                    transform.Translate(
                             move.x * fSpeed * Time.deltaTime,
                             0.0f,
                             0.0f);
                }


                // データに保存
                CPData.playerPos = this.transform.position;
            }
        } else {
            /*
             * 狐の窓使用時は別スクリプト
             */
            if (CPData.isLook) {
                if (CPData.isKokkurisan || CPData.isObjNameUI) {
                    CPData.isLook = false;
                }
            }

            // プレイヤーの非表示
            if(animState != eAnimState.FOXWINDOW) {
                sr.color = new Color(1,1,1,0.0f);
            }

        }
    }

    public Vector2 GetMoveValue() {
        return _moveAction.ReadValue<Vector2>();
    }

    private void OnLens(InputAction.CallbackContext obj) {
        //if (CPData.isKokkurisan || CPData.isObjNameUI) {
        //    return;
        //}
        CPData.isLens = !CPData.isLens;

        // プレイヤーの表示
        if (!CPData.isLens) {
            sr.color = new Color(1, 1, 1, 1.0f);
        }
    }
    private void OnPoseKey(InputAction.CallbackContext obj) {
        CPData.isPose = !CPData.isPose;
    }

    private void LookStart(InputAction.CallbackContext obj) {
        if (CPData.isKokkurisan || CPData.isObjNameUI) {
            return;
        }
        CPData.isLook = true;
    }
    private void LookFin(InputAction.CallbackContext obj) {
        CPData.isLook = false;
    }

    private void OpenHintKey(InputAction.CallbackContext obj) {
        // キーボードはキーを押したときに表示しかできないようになっているのでtrueのみ
        //CPData.isKokkurisan = true;
        StartCoroutine("DelayKokkurisan");
    }

    private void OpenHintButton(InputAction.CallbackContext obj) {
        // コントローラーはボタンを押したら表示非表示切り替えるのでトグル
        CPData.isKokkurisan = !CPData.isKokkurisan;
    }


    private void CloseHint(InputAction.CallbackContext obj) {
        if (CPData.isKokkurisan) {
            Keyboard keyboard = Keyboard.current;
            if (keyboard != null) {
                if (!keyboard.enterKey.wasReleasedThisFrame &&
                    !keyboard.escapeKey.wasReleasedThisFrame &&
                    !keyboard.eKey.wasReleasedThisFrame &&
                    !keyboard.qKey.wasReleasedThisFrame) {
                    CPData.isKokkurisan = false;
                }
            }
        }
    }

    private IEnumerator DelayKokkurisan() {
        yield return null;  // 1フレーム後にisKokkurisanをtrueにする
        CPData.isKokkurisan = true;
    }
}