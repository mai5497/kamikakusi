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
    private InputAction _moveAction, _kituneAction, _cyuusiAction, _poseAction, /*_hintOpenKeyAction, _hintOpenButtonAction,*/ _hintCloseAction;
    private CP_move_input UIAction;        // InputActionを扱う

    private eAnimState animState;   // アニメーションステート

    private SpriteRenderer sr;  // プレイヤーのspriterenderer
    private float oldMoveVal;

    void Start() {
        animState = eAnimState.NONE;
        sr = this.GetComponent<SpriteRenderer>();
        oldMoveVal = 0.0f;
    }

    void Awake() {
        UIAction = new CP_move_input();            // InputActionインスタンスを生成
        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _moveAction = actionMap["Move"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
        //_hintOpenKeyAction = actionMap["HintKey"];
        //_hintOpenButtonAction = actionMap["HintButton"];
        _hintCloseAction = actionMap["HintClose"];
    }

    private void OnEnable() {
        //---Actionイベントを登録
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;
        //_hintOpenKeyAction.started += OpenHintKey;
        //_hintOpenButtonAction.canceled += OpenHintButton;

        _hintCloseAction.canceled += CloseHint;

        UIAction.UI.KeyKokkurisan.started += OpenHintKey;
        UIAction.UI.ButtonKokkurisan.started += OpenHintButton;


        //---InputActionの有効化
        UIAction.UI.Enable();
    }

    private void OnDisable() {
        //---InputActionの無効化
        UIAction.UI.Disable();
    }


    void Update() {
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

                transform.Translate(
                    move.x * fSpeed,
                    0.0f,
                    0.0f);

                // プレイヤー反転処理
                if(move.x > -1 && oldMoveVal > 0) {
                    this.transform.localScale = new Vector3(1,1,1);
                } else if(move.x < 0 && oldMoveVal < 0){
                    this.transform.localScale = new Vector3(-1,1,1);
                }
                oldMoveVal = move.x;

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
        yield return null;
        CPData.isKokkurisan = true;
    }
}