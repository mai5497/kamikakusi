




//=============================================================================
//
//プレイヤーデータ
//
// 作成日:2022/10/11
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================














using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CP_move01 : MonoBehaviour
{
    public  bool isDash = false;

  //  public float fMove;


    //パッド用
    //public InputAction m_inputMover;
    //public Vector2 m_movementValue;
    public float fSpeed = 0.01f;
    //private void OnEnable()
    //{
    //    m_inputMover.Enable();
    //}
    //private void OnDisable()
    //{
    //    m_inputMover.Disable();
    //}



    //アクション取得用
    private InputAction _moveAction, _fireAction,_kituneAction,_cyuusiAction, _poseAction, _pose_ketteiAction, _ui_keyAction;

    //注視
    public bool isLook=false;
    //動いてるか・・まだ使ってない狐の窓の際に動かないように使う
    public bool isMove = true;
    //ポーズ
    public bool isPose = false;

    enum Mode
    {
        not_mado,
        in_mado,
    }

    Mode mode= Mode.not_mado; 

    //public float fDash = 1.0f;


    void Start()
    {
        //  float fSpeed = 0.01f;
       // fMove = 0.03f;
        var pInput = GetComponent<PlayerInput>();
      
        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _moveAction = actionMap["Move"];
        _fireAction = actionMap["Fire"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
        _pose_ketteiAction = actionMap["Pose_kettei"];
        _ui_keyAction = actionMap["UI_key"];
    }

    void Update()
    {

      //   Transform transform = this.transform;
        //Vector3 pos = transform.position


         var current = Keyboard.current;

        // // キーボード接続チェック
        // if (current == null)
        // {
        //     // キーボードが接続されていないと
        //     // Keyboard.currentがnullになる
        //     return;
        // }

        // // Aキーの入力状態取得
        // var aKey = current.aKey;
        // // Dキーの入力状態取得
        // var dKey = current.dKey;
        // //ゲームパッドの入力状態取得
        // //  var gamepad = Gamepad.leftStick.ReadValue();
        //// var gamepad = Gamepad.current.leftStick;
        // // キーが押されたかどうか
        // if (aKey.isPressed )
        // {
        //     Debug.Log("Aキーが押された！");

        //     // pos.x -= 0.01f;

        //      transform.position -= transform.right * fMove;   

        // }
        // // キーが押されたかどうか
        // else if (dKey.isPressed)
        // {
        //     Debug.Log("Dキーが押された！");

        //     // pos.x -= 0.01f;

        //     transform.position += transform.right * fMove ;

        // }



      //  bool isFire = _fireAction.triggered;

       // // shiftキーの入力状態取得
       // var shiftKey = current.shiftKey;
       //// shiftキーが押されたかどうか
       // if (shiftKey.isPressed)
       // {
       //    // Debug.Log("shiftキーが押された！");

       //     //走ってるかどうか
       //     isDash = true;
        
       // }
      
       


        //パッド用
        //m_movementValue = m_inputMover.ReadValue<Vector2>();
        //transform.Translate(
        //    m_movementValue.x * fSpeed,
        //    0.0f,
        //    0.0f);


      
         //プレイヤーの移動処理
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector2 move = _moveAction.ReadValue<Vector2>();

        transform.Translate(
            move.x * fSpeed,
            0.0f,
            0.0f);

        bool fire = _fireAction.IsPressed();
        
        if(fire)
        {
          //  Debug.Log("shiftキーが押された！");
            isDash = true;
        }
        else 
        {
          //  Debug.Log("aaaaaaaaaaaaaasdasadsa！");
            isDash = false;
        }
        
        if (isDash)
        {
            fSpeed = 0.02f;
        } 
        else 
        {
            isDash = false;

            if (!isDash) 
            {
                fSpeed = 0.01f;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //狐の窓展開！！！！！！！！！！！！！！！１１
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool KituneMado = _kituneAction.WasPerformedThisFrame();
        if(KituneMado&& mode == Mode.not_mado)
        {
          //    Debug.Log("狐の窓展開！！！！！！！！！！！！！！！！");
          //  isLook = true;
            mode =  Mode.in_mado;

        }
         else if (KituneMado && mode == Mode.in_mado)
            {
            //    Debug.Log("窓終わり！！！！！！！！！！！！！！！！");
               // isLook = false;
                 mode = Mode.not_mado;
            }
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //注視します//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        bool Cyuusi = _cyuusiAction.IsPressed();
        if(Cyuusi && mode == Mode.in_mado)
        {
         //   Debug.Log("注視！！！！！！！！！！！！！！！！");
           
        }
        else 
        {
           // Debug.Log("注視できない！！！！！！！！！！！！！！！！");

          
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //ポーズします//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool Pose = _poseAction.WasPerformedThisFrame();

        if (Pose && !isPose)
        {
            Debug.Log("ポーズ中！！！！！！！！！！！！！！！！");
            isPose = true;
        }
        else if (Pose && isPose) 
        {
            Debug.Log("ポーズじゃない！！！！！！！！！！！！！！！！");
            isPose = false;
        }

        bool Pose_kettei = _pose_ketteiAction.WasPerformedThisFrame();

        if(Pose_kettei && isPose)
        {
            Debug.Log("ポーズ中の決定！！！！！！！！！！！！！！！！");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///十字きー設定
        //bool UI_key = _ui_keyAction.IsPressed();













    }

}