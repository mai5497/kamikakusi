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

public class CP_move01 : MonoBehaviour
{
    public  bool isDash = false;

    [Header("移動スピード")]
    public float fSpeed = 0.01f;

    //アクション取得用
    private InputAction _moveAction, _fireAction,_kituneAction,_cyuusiAction, _poseAction, _pose_ketteiAction, _ui_keyAction;

    //注視
    //[Header("注視してるか")]
    //[SerializeField]
    //private bool isLook=false;
    ////動いてるか・・まだ使ってない狐の窓の際に動かないように使う
    //[SerializeField]
    //private bool isMove = true;
    ////ポーズ
    //[Header("ポーズしてるか")]
    //[SerializeField]
    //private bool isPose = false;


    enum Mode
    {
        not_mado,
        in_mado,
    }

    Mode mode= Mode.not_mado; 

    void Start()
    {
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
    }

    void Update()
    {
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
            CPData.isLook = true;
        }
        else 
        {
            // Debug.Log("注視してない！！！！！！！！！！！！！！！！");
            CPData.isLook = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //ポーズします//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool Pose = _poseAction.WasPerformedThisFrame();

        if (Pose && !CPData.isPose)
        {
            //Debug.Log("ポーズ中！！！！！！！！！！！！！！！！");
            CPData.isPose = true;
        }
        else if (Pose && CPData.isPose) 
        {
            //Debug.Log("ポーズじゃない！！！！！！！！！！！！！！！！");
            CPData.isPose = false;
        }
    }

}