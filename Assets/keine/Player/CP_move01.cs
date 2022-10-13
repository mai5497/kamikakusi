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
    public float fSpeed = 0.01f;    // プレイヤーの移動と窓の移動スピード兼ねてる

    //アクション取得用
    private InputAction _moveAction, _fireAction, _kituneAction, _cyuusiAction, _poseAction;
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
        _fireAction = actionMap["Fire"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
    }

    private void OnEnable() {
        //---Actionイベントを登録
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;

        //---InputActionの有効化
        //ActionAssets.Player.Enable();
    }

    private void OnDisable() {
        //---InputActionの無効化
        //ActionAssets.Player.Disable();
    }


    void Update() {
        if (!CPData.isLens) {
            //プレイヤーの移動処理
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector2 move = _moveAction.ReadValue<Vector2>();

            transform.Translate(
                move.x * fSpeed,
                0.0f,
                0.0f);

            bool fire = _fireAction.IsPressed();

            if (fire) {
                //  Debug.Log("shiftキーが押された！");
                isDash = true;
            } else {
                //  Debug.Log("aaaaaaaaaaaaaasdasadsa！");
                isDash = false;
            }

            if (isDash) {
                fSpeed = 0.02f;
            } else {
                isDash = false;

                if (!isDash) {
                    fSpeed = 0.01f;
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //狐の窓展開！！！！！！！！！！！！！！！１１
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //if (CPData.isLens) {
            //    //    Debug.Log("狐の窓展開！！！！！！！！！！！！！！！！");
            //    //  isLook = true;
            //    mode = Mode.in_mado;
            //} else {
            //    //    Debug.Log("窓終わり！！！！！！！！！！！！！！！！");
            //    // isLook = false;
            //    mode = Mode.not_mado;
            //}

            CPData.playerPos = this.transform.position;

        } else {


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            //注視します//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //bool Cyuusi = _cyuusiAction.IsPressed();
            //if (Cyuusi && mode == Mode.in_mado) {
            //    //   Debug.Log("注視！！！！！！！！！！！！！！！！");
            //    CPData.isLook = true;
            //} else {
            //    // Debug.Log("注視してない！！！！！！！！！！！！！！！！");
            //    CPData.isLook = false;
            //}

        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //ポーズします//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    public Vector2 GetMoveValue() {
        return _moveAction.ReadValue<Vector2>();
    }

    private void OnLens(InputAction.CallbackContext obj) {
        CPData.isLens = !CPData.isLens;
    }
    private void OnPoseKey(InputAction.CallbackContext obj) {
        CPData.isPose = !CPData.isPose;
    }

    private void LookStart(InputAction.CallbackContext obj) {
        CPData.isLook = true;
    }
    private void LookFin(InputAction.CallbackContext obj) {
        CPData.isLook = false;
    }

}