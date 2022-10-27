using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    //----- �萔��` -----
    private enum eSTATEPAUSE    // �|�[�Y����s������
    {
        SOUSA = 0, // ����
        RETURNGAME,           // ������@
        RETURNSELECT,    // �Z���N�g��ʂɖ߂�
        OPTION,         // �I�v�V����
        QUITQUESTION,   // �{���ɂ�߂܂���

        MAX_STATE
    }
    private enum eQuitState {
        NONE,
        YES,
        NO
    }

    //----- �ϐ���` -----
    //---�\������UI
    private bool isDecision;                // ����
    private bool isSousaShow;                  // ����������o�Ă�Ƃ�
    //private bool isConfirm;                   // �t�@�C�i���A���T�[�H

    private CP_move_input UIActionAssets;        // InputAction��UI������

    private int pauseSelect;                // �|�[�Y�I��
    private int quitSelect;                 // �ŏI�m�F�I��

    private int oldSelect;               // �߂�Ȃ��Ⴂ���Ȃ��I����
    //private bool isCalledOncce = false;     // �|�[�Y����񂵂��Ă΂Ȃ�

    //private bool notShowPause;              // �|�[�Y�ł͂Ȃ��Ƃ���UI��\�����Ȃ��悤�ɂ��邽�߂̕ϐ�

    private GameObject sousaChara;               // �����������
    private RectTransform sousaCharaRT;          // ������������̃��N�g�g�����X�t�H�[��

    private GameObject sousa;              // ����������Ă�R���g���[���[�̉摜

    private GameObject returnGame;          // �Q�[���ɖ߂�
    private RectTransform returnGameRT;     // �Q�[���ɖ߂�̃��N�g�g�����X�t�H�[��

    private GameObject returnSelect;        // �X�e�[�W�Z���N�g
    private RectTransform returnSelectRT;   // �X�e�[�W�Z���N�g�̃��N�g�g�����X�t�H�[��

    private GameObject option;              // �I�v�V����
    private RectTransform optionRT;         // �I�v�V�����̃��N�g�g�����X�t�H�[��

    private GameObject selectBox;           // �I��g
    private RectTransform selectBoxRT;      // �I��g�̃��N�g�g�����X�t�H�[��

    private GameObject kakunin;             // �Q�[�����I�����܂���
    private Image kakuninYes;          // �m�F��ʂ̃C�G�X
    private Image kakuninNo;           // �m�F��ʂ̃m�[

    void Awake() {
        UIActionAssets = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    private void Start() {
        quitSelect = (int)eQuitState.NONE;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // �I���̃��[�h�̏�����
        oldSelect = pauseSelect;

        sousaChara = transform.Find("Pose_charbox_1").gameObject;
        sousaCharaRT = sousaChara.GetComponent<RectTransform>();
        sousa = transform.Find("Sousa").gameObject;
        isSousaShow = false;
        sousa.SetActive(isSousaShow); // ��\��
        returnGame = transform.Find("Pose_charbox_2").gameObject;
        returnGameRT = returnGame.GetComponent<RectTransform>();
        returnSelect = transform.Find("Pose_charbox_3").gameObject;
        returnSelectRT = returnSelect.GetComponent<RectTransform>();
        option = transform.Find("Pose_charbox_4").gameObject;
        optionRT = option.GetComponent<RectTransform>();
        selectBox = transform.Find("Pose_selectbox").gameObject;
        selectBoxRT = selectBox.GetComponent<RectTransform>();
        kakunin = transform.Find("Kakunin").gameObject;
        kakuninYes = kakunin.transform.Find("Kakunin_charbox_1").GetComponent<Image>();
        kakuninNo = kakunin.transform.Find("Kakunin_charbox_2").GetComponent<Image>();
        kakunin.SetActive(false);   // ��\��
    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        UIActionAssets.UI.Select.started += Select;
        UIActionAssets.UI.Dicision.canceled += Dicision;

        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }


    // Update is called once per frame
    void Update() {
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            if (isDecision) {   // ����L�[�������ꂽ��
                if (quitSelect == (int)eQuitState.YES) {

                } else {
                    kakunin.SetActive(false);   // �m�F��ʏ���
                }
                pauseSelect = oldSelect;    // �������܂őI�����Ă��Ƃ���(�Z���N�g�֖߂�)�ɃX�e�[�g��߂�
            }
        }

        if (isDecision) {   // ����L�[�������ꂽ��
            switch (pauseSelect) {
                //----- ��������\�� -----
                case (int)eSTATEPAUSE.SOUSA:
                    isSousaShow = !isSousaShow;

                    sousa.SetActive(isSousaShow);
                 
                    isDecision = false;
                    break;
                //----- �Q�[���ɖ߂�(�|�[�Y��ʉ���) -----
                case (int)eSTATEPAUSE.RETURNGAME:
                    Pause.isPause = false;
                    isDecision = false;
                    break;
                //----- �Z���N�g��ʂɖ߂� -----
                case (int)eSTATEPAUSE.RETURNSELECT:
                    if (quitSelect == (int)eQuitState.NONE) {// �Z���N�g��ʂɖ߂邪�I�����ꂽ�Ƃ�
                        // ������
                        quitSelect = (int)eQuitState.YES;
                        oldSelect = pauseSelect;
                        pauseSelect = (int)eSTATEPAUSE.QUITQUESTION;
                        kakunin.SetActive(true);
                    } else if (quitSelect == (int)eQuitState.YES) {
                        //�X�e�[�W�Z���N�g�֑J��
                        Pause.isPause = false;
                        SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);
                    } else if (quitSelect == (int)eQuitState.NO) {
                        // NO�Ŗ߂��Ă����Ƃ��̓��Z�b�g
                        quitSelect = (int)eQuitState.NONE;
                    }
                    isDecision = false;
                    break;
                case (int)eSTATEPAUSE.OPTION:
                    isDecision = false;
                    break;
            }
        }
    }

    private void Select(InputAction.CallbackContext obj) {
        if(!Pause.isPause) {
            return; 
        }
        //---�X�e�B�b�N���͂��擾
        Vector2 doStick;
        doStick = obj.ReadValue<Vector2>();
        if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doStick.x > 0.0f) {
                SelectRight();
            } else if (doStick.x < 0.0f) {
                SelectLeft();
            }
        } else if(!isSousaShow){
            //---�����ł��|���ꂽ�珈���ɓ���
            if (doStick.y > 0.0f) {
                SelectUp();
            } else if (doStick.y < 0.0f) {
                SelectDown();
            }
        }

    }

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void Dicision(InputAction.CallbackContext obj) {
        if (Pause.isPause) {
            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
            isDecision = true;
        }
    }

    /// <summary>
    /// �I��g�̍��W�X�V
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
            selectBoxRT.localPosition = returnGameRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
            selectBoxRT.localPosition = sousaCharaRT.localPosition;
        }else if(pauseSelect == (int)eSTATEPAUSE.RETURNSELECT) {
            selectBoxRT.localPosition = returnSelectRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
            selectBoxRT.localPosition = optionRT.localPosition;
        } else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {

            // �{���ɂ����ł����͑I��g�𓮂����̂ł͂Ȃ�
            // �͂��������̖؂̔̐F���������ۂ����邱�Ƃŕ\������
            if (quitSelect == (int)eQuitState.YES) {
                kakuninYes.color = new Color(1.0f,1.0f,1.0f,1.0f);
                kakuninNo.color = new Color(0.7f,0.7f,0.7f,1.0f);
            } else if (quitSelect == (int)eQuitState.NO) {
                kakuninYes.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
                kakuninNo.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    /// <summary>
    /// ������I��
    /// </summary>
    private void SelectUp() {
        // ��
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        pauseSelect--;
        if (pauseSelect < 0) // ��O����
        {
            pauseSelect = 0;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �������I��
    /// </summary>
    private void SelectDown() {
        // ��
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        pauseSelect++;
        if (pauseSelect >= (int)eSTATEPAUSE.OPTION)   // ��O����
        {
            pauseSelect = (int)eSTATEPAUSE.OPTION;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }


    /// <summary>
    /// �I���E
    /// </summary>
    private void SelectRight() {
        // ��
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect++;
        if (quitSelect > (int)eQuitState.NO) {
            quitSelect = (int)eQuitState.NO;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �I����
    /// </summary>
    private void SelectLeft() {
        // ��
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES) {
            quitSelect = (int)eQuitState.YES;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }
}
