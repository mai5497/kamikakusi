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
    enum eAnimState {   // �A�j���[�V�����̃X�e�[�g
        NONE = 0,
        IDLE,
        WALK,
        FOXWINDOW,
    }

    [Header("�ړ��X�s�[�h")]
    public float fSpeed = 0.04f;    // �v���C���[�̈ړ��Ƒ��̈ړ��X�s�[�h���˂Ă�

    //�A�N�V�����擾�p
    private InputAction _moveAction, _kituneAction, _cyuusiAction, _poseAction, /*_hintOpenKeyAction, _hintOpenButtonAction,*/ _hintCloseAction;
    private CP_move_input UIAction;        // InputAction������

    private eAnimState animState;   // �A�j���[�V�����X�e�[�g

    private SpriteRenderer sr;  // �v���C���[��spriterenderer
    private float oldMoveVal;

    void Start() {
        animState = eAnimState.NONE;
        sr = this.GetComponent<SpriteRenderer>();
        oldMoveVal = 0.0f;
    }

    void Awake() {
        UIAction = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _moveAction = actionMap["Move"];
        _kituneAction = actionMap["Kitune"];
        _cyuusiAction = actionMap["Cyuusi"];
        _poseAction = actionMap["Pose"];
        //_hintOpenKeyAction = actionMap["HintKey"];
        //_hintOpenButtonAction = actionMap["HintButton"];
        _hintCloseAction = actionMap["HintClose"];
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        _poseAction.canceled += OnPoseKey;
        _kituneAction.canceled += OnLens;
        _cyuusiAction.started += LookStart;
        _cyuusiAction.canceled += LookFin;
        //_hintOpenKeyAction.started += OpenHintKey;
        //_hintOpenButtonAction.canceled += OpenHintButton;

        _hintCloseAction.canceled += CloseHint;

        UIAction.UI.KeyKokkurisan.started += OpenHintKey;
        UIAction.UI.ButtonKokkurisan.started += OpenHintButton;


        //---InputAction�̗L����
        UIAction.UI.Enable();
    }

    private void OnDisable() {
        //---InputAction�̖�����
        UIAction.UI.Disable();
    }


    void Update() {
        if (CPData.isLens) {    // ���g�p���ɂȂ�����
            /*
             * �A�j���[�V�����̍Đ�
             */
            animState = eAnimState.NONE;// �Đ��I��������X�e�[�g��NONE�Ƃ��ɂ���(FOXWINDOW�ȊO)
        }

        if (!CPData.isLens) {   // �����Y�g�p���������ړ�������
            if (!CPData.isKokkurisan && !CPData.isObjNameUI) {
                //�v���C���[�̈ړ�����
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Vector2 move = _moveAction.ReadValue<Vector2>();

                transform.Translate(
                    move.x * fSpeed,
                    0.0f,
                    0.0f);

                // �v���C���[���]����
                if(move.x > -1 && oldMoveVal > 0) {
                    this.transform.localScale = new Vector3(1,1,1);
                } else if(move.x < 0 && oldMoveVal < 0){
                    this.transform.localScale = new Vector3(-1,1,1);
                }
                oldMoveVal = move.x;

                // �f�[�^�ɕۑ�
                CPData.playerPos = this.transform.position;
            }
        } else {
            /*
             * �ς̑��g�p���͕ʃX�N���v�g
             */
            if (CPData.isLook) {
                if (CPData.isKokkurisan || CPData.isObjNameUI) {
                    CPData.isLook = false;
                }
            }

            // �v���C���[�̔�\��
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

        // �v���C���[�̕\��
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
        // �L�[�{�[�h�̓L�[���������Ƃ��ɕ\�������ł��Ȃ��悤�ɂȂ��Ă���̂�true�̂�
        //CPData.isKokkurisan = true;
        StartCoroutine("DelayKokkurisan");
    }

    private void OpenHintButton(InputAction.CallbackContext obj) {
        // �R���g���[���[�̓{�^������������\����\���؂�ւ���̂Ńg�O��
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