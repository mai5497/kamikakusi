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
    public bool isDash = false;

    [Header("移動スピード")]
    public float fSpeed = 0.04f;    // プレイヤーの移動と窓の移動スピード兼ねてる

    //アクション取得用
    private InputAction _moveAction, _kituneAction, _cyuusiAction, _poseAction, _hintOpenKeyAction, _hintOpenButtonAction, _hintCloseAction;
    //private CP_move_input ActionAssets;        // InputActionを扱う

    private LensManager _LensManager;

    enum Mode {
        not_mado,
        in_mado,
    }

    Mode mode = Mode.not_mado;

    void Start() {
        _LensManager = this.GetComponent<LensManager>();
    }

    void Awake() {
        //ActionAssets = new CP_move_input();            // InputActionインスタンスを生成
        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _moveAction = actionMap["Move"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
        _hintOpenKeyAction = actionMap["HintKey"];
        _hintOpenButtonAction = actionMap["HintButton"];
        _hintCloseAction = actionMap["HintClose"];
    }

    private void OnEnable() {
        //---Actionイベントを登録
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;
        _hintOpenKeyAction.started += OpenHintKey;
        _hintOpenButtonAction.canceled += OpenHintButton;

        _hintCloseAction.canceled += CloseHint;

        //---InputActionの有効化
        //ActionAssets.Player.Enable();
    }

    private void OnDisable() {
        //---InputActionの無効化
        //ActionAssets.Player.Disable();
    }


    void Update() {
        if (!CPData.isLens) {   // レンズ使用中処理か移動処理か
            if (!CPData.isKokkurisan) {
                //プレイヤーの移動処理
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Vector2 move = _moveAction.ReadValue<Vector2>();

                transform.Translate(
                    move.x * fSpeed,
                    0.0f,
                    0.0f);

               // bool fire = _fireAction.IsPressed();

                //if (fire)
                //{
                //    Debug.Log("shiftキーが押された！");
                //    isDash = true;
                //}
                //else
                //{
                //    Debug.Log("aaaaaaaaaaaaaasdasadsa！");
                //    isDash = false;
                //}

                //if (isDash)
                //{
                //    fSpeed = 0.02f;
                //}
                //else
                //{
                //    isDash = false;

                //    if (!isDash)
                //    {
                //        fSpeed = 0.01f;
                //    }
                //}
                CPData.playerPos = this.transform.position;
            }
        } else {
            /*
             * 狐の窓使用時は別スクリプト
             */
            if (CPData.isLook) {
                if (CPData.isKokkurisan) {
                    CPData.isLook = false;
                }
            }

        }
    }

    public Vector2 GetMoveValue() {
        return _moveAction.ReadValue<Vector2>();
    }

    private void OnLens(InputAction.CallbackContext obj) {
        if (CPData.isKokkurisan) {
            return;
        }
        CPData.isLens = !CPData.isLens;
    }
    private void OnPoseKey(InputAction.CallbackContext obj) {
        CPData.isPose = !CPData.isPose;
    }

    private void LookStart(InputAction.CallbackContext obj) {
        if (CPData.isKokkurisan) {
            return;
        }
        CPData.isLook = true;
    }
    private void LookFin(InputAction.CallbackContext obj) {
        CPData.isLook = false;
    }

    private void OpenHintKey(InputAction.CallbackContext obj) {
        CPData.isKokkurisan = true;
    }

    private void OpenHintButton(InputAction.CallbackContext obj) {
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
}