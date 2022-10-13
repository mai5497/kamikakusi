//=============================================================================
//
//�v���C���[�f�[�^
//
// �쐬��:2022/10/11
// �쐬��:���؋��d��
// �ҏW��:�ɒn�c�^��
//
// <�J������>
// 2022/10/12 �쐬
// 2022/10/13 �}�[�W�̂��߂ɂ��낢��ύX
//
//=============================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CP_move01 : MonoBehaviour
{
    public  bool isDash = false;

    [Header("�ړ��X�s�[�h")]
    public float fSpeed = 0.01f;

    //�A�N�V�����擾�p
    private InputAction _moveAction, _fireAction,_kituneAction,_cyuusiAction, _poseAction, _pose_ketteiAction, _ui_keyAction;

    //����
    //[Header("�������Ă邩")]
    //[SerializeField]
    //private bool isLook=false;
    ////�����Ă邩�E�E�܂��g���ĂȂ��ς̑��̍ۂɓ����Ȃ��悤�Ɏg��
    //[SerializeField]
    //private bool isMove = true;
    ////�|�[�Y
    //[Header("�|�[�Y���Ă邩")]
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
      
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _moveAction = actionMap["Move"];
        _fireAction = actionMap["Fire"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
    }

    void Update()
    {
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
            CPData.isLook = true;
        }
        else 
        {
            // Debug.Log("�������ĂȂ��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            CPData.isLook = false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //�|�[�Y���܂�//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool Pose = _poseAction.WasPerformedThisFrame();

        if (Pose && !CPData.isPose)
        {
            //Debug.Log("�|�[�Y���I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            CPData.isPose = true;
        }
        else if (Pose && CPData.isPose) 
        {
            //Debug.Log("�|�[�Y����Ȃ��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            CPData.isPose = false;
        }
    }

}