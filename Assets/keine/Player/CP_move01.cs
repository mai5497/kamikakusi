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

public class CP_move01 : MonoBehaviour {
    public bool isDash = false;

    [Header("�ړ��X�s�[�h")]
    public float fSpeed = 0.01f;    // �v���C���[�̈ړ��Ƒ��̈ړ��X�s�[�h���˂Ă�

    //�A�N�V�����擾�p
    private InputAction _moveAction, _fireAction, _kituneAction, _cyuusiAction, _poseAction;
    //private CP_move_input ActionAssets;        // InputAction������

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
        //ActionAssets = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _moveAction = actionMap["Move"];
        _fireAction = actionMap["Fire"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;

        //---InputAction�̗L����
        //ActionAssets.Player.Enable();
    }

    private void OnDisable() {
        //---InputAction�̖�����
        //ActionAssets.Player.Disable();
    }


    void Update() {
        if (!CPData.isLens) {
            //�v���C���[�̈ړ�����
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Vector2 move = _moveAction.ReadValue<Vector2>();

            transform.Translate(
                move.x * fSpeed,
                0.0f,
                0.0f);

            bool fire = _fireAction.IsPressed();

            if (fire) {
                //  Debug.Log("shift�L�[�������ꂽ�I");
                isDash = true;
            } else {
                //  Debug.Log("aaaaaaaaaaaaaasdasadsa�I");
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
            //�ς̑��W�J�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�P�P
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //if (CPData.isLens) {
            //    //    Debug.Log("�ς̑��W�J�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            //    //  isLook = true;
            //    mode = Mode.in_mado;
            //} else {
            //    //    Debug.Log("���I���I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            //    // isLook = false;
            //    mode = Mode.not_mado;
            //}

            CPData.playerPos = this.transform.position;

        } else {


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            //�������܂�//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //bool Cyuusi = _cyuusiAction.IsPressed();
            //if (Cyuusi && mode == Mode.in_mado) {
            //    //   Debug.Log("�����I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            //    CPData.isLook = true;
            //} else {
            //    // Debug.Log("�������ĂȂ��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            //    CPData.isLook = false;
            //}

        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //�|�[�Y���܂�//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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