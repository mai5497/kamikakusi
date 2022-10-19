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
    public float fSpeed = 0.04f;    // �v���C���[�̈ړ��Ƒ��̈ړ��X�s�[�h���˂Ă�

    //�A�N�V�����擾�p
    private InputAction _moveAction, _kituneAction, _cyuusiAction, _poseAction, _hintOpenKeyAction, _hintOpenButtonAction, _hintCloseAction;
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
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
        _hintOpenKeyAction = actionMap["HintKey"];
        _hintOpenButtonAction = actionMap["HintButton"];
        _hintCloseAction = actionMap["HintClose"];
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;
        _hintOpenKeyAction.started += OpenHintKey;
        _hintOpenButtonAction.canceled += OpenHintButton;

        _hintCloseAction.canceled += CloseHint;

        //---InputAction�̗L����
        //ActionAssets.Player.Enable();
    }

    private void OnDisable() {
        //---InputAction�̖�����
        //ActionAssets.Player.Disable();
    }


    void Update() {
        if (!CPData.isLens) {   // �����Y�g�p���������ړ�������
            if (!CPData.isKokkurisan) {
                //�v���C���[�̈ړ�����
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Vector2 move = _moveAction.ReadValue<Vector2>();

                transform.Translate(
                    move.x * fSpeed,
                    0.0f,
                    0.0f);

               // bool fire = _fireAction.IsPressed();

                //if (fire)
                //{
                //    Debug.Log("shift�L�[�������ꂽ�I");
                //    isDash = true;
                //}
                //else
                //{
                //    Debug.Log("aaaaaaaaaaaaaasdasadsa�I");
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
             * �ς̑��g�p���͕ʃX�N���v�g
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