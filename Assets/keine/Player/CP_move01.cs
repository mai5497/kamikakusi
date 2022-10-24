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
// 2022/10/21 InputPlayer�������߂ɓ��͉��ύX
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
    private InputAction _moveAction;
    private CP_move_input PlayerAction;        // InputAction������
    private CP_move_input UIAction;        // InputAction������

    private eAnimState animState;   // �A�j���[�V�����X�e�[�g

    //private SpriteRenderer sr;  // �v���C���[��spriterenderer
    private float oldMoveVal;

    private FoxByakko _FoxByakko;

    GameObject wallObj_left;
    GameObject wallObj_right;

    public bool canMoveLeft;
    public bool canMoveRight;

    public float Wall_player_left = 0.0f;
    public float Wall_player_right = 0.0f;

    Vector2 wallLeftPos;
    Vector2 wallRightPos;

    public Animator animator;

    // �����x
    private float moveAccel = 0.0f;
    [Header("�ړ�����_���x")]
    public float moveAccelSpeed;
    [Header("�ړ�����_���x")]
    public float moveDecelSpeed;

    // ���݂̃t���[��
    private int nowFrame = 0;
    // �ۑ�����t���[����
    private const int moveFrameMax = 10;
    // ���t���[���O���g�p���邩
    private const int useFrame = 5;
    // �v���C���[���x�𖈃t���[���ۑ����Ă��郊�X�g
    private float[] moveBeforeList = new float[moveFrameMax];

    void Start() {
        animState = eAnimState.NONE;
        //sr = this.GetComponent<SpriteRenderer>();
        oldMoveVal = 0.0f;

        _FoxByakko = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();

        wallObj_left = GameObject.FindGameObjectWithTag("TestWallLeft");
        wallObj_right = GameObject.FindGameObjectWithTag("TestWallRight");

        wallLeftPos = wallObj_left.transform.position;
        wallRightPos = wallObj_right.transform.position;


        Wall_player_left = this.transform.localScale.x / 2 + wallObj_left.transform.localScale.x / 2 + 0.04f;
        Wall_player_right = this.transform.localScale.x / 2 + wallObj_right.transform.localScale.x / 2 + 0.04f;


    }

    void Awake() {
        PlayerAction = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
        UIAction = new CP_move_input();
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        _moveAction = PlayerAction.Player.Move;
        PlayerAction.Player.Pose.canceled += OnPoseKey;
        PlayerAction.Player.Kitune.canceled += OnLens;
        PlayerAction.Player.Cyuusi.started += LookStart;
        PlayerAction.Player.Cyuusi.canceled += LookFin;

        PlayerAction.Player.HintClose.canceled += CloseHint;

        UIAction.UI.KeyKokkurisan.started += OpenHintKey;
        UIAction.UI.ButtonKokkurisan.started += OpenHintButton;


        //---InputAction�̗L����
        UIAction.UI.Enable();
        PlayerAction.Player.Enable();
    }

    private void OnDisable() {
        _moveAction.Disable();

        //---InputAction�̖�����
        UIAction.UI.Disable();
        PlayerAction.Player.Disable();
    }


    void Update() {
        if (_FoxByakko.isClear || CPData.lookCnt < 1) {   // �N���A������
            CPData.isLook = false;  // �������
            CPData.isLens = false;  // ���g�p���

            _moveAction.Disable();

            return;
        }

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

                // �w�肵���t���[���̃v���C���[�̈ړ����x�����o��
                int frame = nowFrame - useFrame;
                if (frame < 0)
                {
                    frame = moveFrameMax + frame;
                }
                // �v���C���[���]����
                if (moveBeforeList[frame] > 0 && oldMoveVal > 0) {
                    this.transform.localScale = new Vector3(1,1,1);
                } else if(moveBeforeList[frame] < 0 && oldMoveVal < 0){
                    this.transform.localScale = new Vector3(-1,1,1);
                }
                oldMoveVal = move.x;


                bool stopLeft = false;
                bool stopRight = false;

                if (wallLeftPos.x + Wall_player_left >= this.transform.position.x) {
                    stopLeft = true;
                }

                if (wallRightPos.x - Wall_player_right <= this.transform.position.x) {
                    stopRight = true;
                }

                bool isWalk = false;
                if (move.x > -0.0f && !stopRight) {
                    canMoveLeft = true;
                    isWalk = true;
                }
                if (move.x < 0.0f && !stopLeft) {
                    canMoveRight = true;
                    isWalk = true;
                }

                Debug.Log(move);

                if (isWalk)
                {
                    UpdateWalk(move.x);
                }
                else
                {
                    UpdateIdle();
                }


                // �f�[�^�ɕۑ�
                CPData.playerPos = this.transform.position;
            }
            else
            {
                UpdateIdle();
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

            UpdateIdle();

            // �v���C���[�̔�\��
            //if(animState != eAnimState.FOXWINDOW) {
            //    sr.color = new Color(1,1,1,0.0f);
            //}

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
        //if (!CPData.isLens) {
        //    sr.color = new Color(1, 1, 1, 1.0f);
        //}
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
        yield return null;  // 1�t���[�����isKokkurisan��true�ɂ���
        CPData.isKokkurisan = true;
    }

    // ��������
    private void UpdateWalk(float move)
    {
        // �ړ�����
        transform.Translate(
        move * moveAccel * fSpeed * Time.deltaTime,
        0.0f,
        0.0f);
        // �����A�j���[�V����
        animator.SetBool("Run", true);
        // ��������
        moveAccel += moveAccelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);

        // �����ϊ��ŉ����̃��Z�b�g/////
        int beforeFrame = nowFrame - 1;
        if (beforeFrame < 0)
        {
            beforeFrame = moveFrameMax - 1;
        }
        if (move < 0 && moveBeforeList[beforeFrame] > 0)
        {
            moveAccel = 0;
        }
        else if (move > 0 && moveBeforeList[beforeFrame] < 0)
        {
            moveAccel = 0;
        }
        ///////////////////////
        
        // ���݃t���[���Ƀv���C���[�̑��x��ۑ�
        moveBeforeList[nowFrame] = move;
        nowFrame++;
        if (nowFrame >= moveFrameMax)
        {
            nowFrame = 0;
        }
    }
    // �ҋ@����
    private void UpdateIdle()
    {
        // �w�肵���t���[���̃v���C���[�̈ړ����x�����o��
        int frame = nowFrame - useFrame;
        if (frame < 0)
        {
            frame = moveFrameMax + frame;
        }
        // �ړ�����
        transform.Translate(
        moveBeforeList[frame] * moveAccel * fSpeed * Time.deltaTime,
        0.0f,
        0.0f);
        // �����A�j���[�V�����̉���
        animator.SetBool("Run", false);
        // ��������
        moveAccel -= moveDecelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);
    }
}