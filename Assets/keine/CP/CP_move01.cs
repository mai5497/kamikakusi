




//=============================================================================
//
//�v���C���[�f�[�^
//
// �쐬��:2022/10/11
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/12 �쐬
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


    //�p�b�h�p
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



    //�A�N�V�����擾�p
    private InputAction _moveAction, _fireAction,_kituneAction,_cyuusiAction, _poseAction, _pose_ketteiAction, _ui_keyAction;

    //����
    public bool isLook=false;
    //�����Ă邩�E�E�܂��g���ĂȂ��ς̑��̍ۂɓ����Ȃ��悤�Ɏg��
    public bool isMove = true;
    //�|�[�Y
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
      
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
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

        // // �L�[�{�[�h�ڑ��`�F�b�N
        // if (current == null)
        // {
        //     // �L�[�{�[�h���ڑ�����Ă��Ȃ���
        //     // Keyboard.current��null�ɂȂ�
        //     return;
        // }

        // // A�L�[�̓��͏�Ԏ擾
        // var aKey = current.aKey;
        // // D�L�[�̓��͏�Ԏ擾
        // var dKey = current.dKey;
        // //�Q�[���p�b�h�̓��͏�Ԏ擾
        // //  var gamepad = Gamepad.leftStick.ReadValue();
        //// var gamepad = Gamepad.current.leftStick;
        // // �L�[�������ꂽ���ǂ���
        // if (aKey.isPressed )
        // {
        //     Debug.Log("A�L�[�������ꂽ�I");

        //     // pos.x -= 0.01f;

        //      transform.position -= transform.right * fMove;   

        // }
        // // �L�[�������ꂽ���ǂ���
        // else if (dKey.isPressed)
        // {
        //     Debug.Log("D�L�[�������ꂽ�I");

        //     // pos.x -= 0.01f;

        //     transform.position += transform.right * fMove ;

        // }



      //  bool isFire = _fireAction.triggered;

       // // shift�L�[�̓��͏�Ԏ擾
       // var shiftKey = current.shiftKey;
       //// shift�L�[�������ꂽ���ǂ���
       // if (shiftKey.isPressed)
       // {
       //    // Debug.Log("shift�L�[�������ꂽ�I");

       //     //�����Ă邩�ǂ���
       //     isDash = true;
        
       // }
      
       


        //�p�b�h�p
        //m_movementValue = m_inputMover.ReadValue<Vector2>();
        //transform.Translate(
        //    m_movementValue.x * fSpeed,
        //    0.0f,
        //    0.0f);


      
         //�v���C���[�̈ړ�����
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector2 move = _moveAction.ReadValue<Vector2>();

        transform.Translate(
            move.x * fSpeed,
            0.0f,
            0.0f);

        bool fire = _fireAction.IsPressed();
        
        if(fire)
        {
          //  Debug.Log("shift�L�[�������ꂽ�I");
            isDash = true;
        }
        else 
        {
          //  Debug.Log("aaaaaaaaaaaaaasdasadsa�I");
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

        //�ς̑��W�J�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�P�P
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool KituneMado = _kituneAction.WasPerformedThisFrame();
        if(KituneMado&& mode == Mode.not_mado)
        {
          //    Debug.Log("�ς̑��W�J�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
          //  isLook = true;
            mode =  Mode.in_mado;

        }
         else if (KituneMado && mode == Mode.in_mado)
            {
            //    Debug.Log("���I���I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
               // isLook = false;
                 mode = Mode.not_mado;
            }
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //�������܂�//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        bool Cyuusi = _cyuusiAction.IsPressed();
        if(Cyuusi && mode == Mode.in_mado)
        {
         //   Debug.Log("�����I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
           
        }
        else 
        {
           // Debug.Log("�����ł��Ȃ��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

          
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //�|�[�Y���܂�//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool Pose = _poseAction.WasPerformedThisFrame();

        if (Pose && !isPose)
        {
            Debug.Log("�|�[�Y���I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            isPose = true;
        }
        else if (Pose && isPose) 
        {
            Debug.Log("�|�[�Y����Ȃ��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            isPose = false;
        }

        bool Pose_kettei = _pose_ketteiAction.WasPerformedThisFrame();

        if(Pose_kettei && isPose)
        {
            Debug.Log("�|�[�Y���̌���I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///�\�����[�ݒ�
        //bool UI_key = _ui_keyAction.IsPressed();













    }

}