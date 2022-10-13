using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //----- �萔��` -----
    private enum eSTATEPAUSE    // �|�[�Y����s������
    {
        RETURNGAME = 0, // �Q�[���ɖ߂�
        RETURNSELECT,    // �Z���N�g��ʂɖ߂�
        SOUSA,           // ������@
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
    [SerializeField]
    private GameObject pauseframe;
    private GameObject PauseFrame;
    [SerializeField]
    private GameObject pausecharacter;
    private GameObject PauseCharacter;
    [SerializeField]
    private GameObject backgame;
    private GameObject BackGame;
    [SerializeField]
    private GameObject gameend;
    private GameObject GameEnd;
    [SerializeField]
    private GameObject sousachara;
    private GameObject SousaChara;
    [SerializeField]
    private GameObject sousaimage;
    private GameObject SousaImage;
    [SerializeField]
    private GameObject optionchara;
    private GameObject OptionChara;
    [SerializeField]
    private GameObject selectbox_1;
    private GameObject SelectBox_1; // �ʏ�̑I���Ŏg�p
    [SerializeField]
    private GameObject selectbox_2;
    private GameObject SelectBox_2; // �ŏI�m�F�Ŏg�p
    [SerializeField]
    private GameObject decision;
    private GameObject Decision;

    [SerializeField]
    private GameObject quitquestion;
    private GameObject QuitQuestion;
    [SerializeField]
    private GameObject quityes;
    private GameObject QuitYes;
    [SerializeField]
    private GameObject quitno;
    private GameObject QuitNo;

    Canvas canvas;                          // �\���Ɏg�p����Canvas

    private bool isDecision;                // ����
    private bool isConfirm;                   // �t�@�C�i���A���T�[�H

    private CP_move_input UIActionAssets;        // InputAction��UI������
    //private InputAction LeftStickSelect;    // InputAction��select������
    //private InputAction RightStickSelect;   // InputAction��select������

    private int pauseSelect;                // �|�[�Y�I��
    private int quitSelect;                 // �ŏI�m�F�I��
    private int returnSelect;               // �߂�Ȃ��Ⴂ���Ȃ��I����
    private bool isCalledOncce = false;     // �|�[�Y����񂵂��Ă΂Ȃ�

    private float UIBasePosx;               // UI�\���̊�ʒu
    private float UIMoveSpeed = 1.5f;       // UI�\���𓮂����Ƃ��̃X�s�[�h

    private bool notShowPause;              // �|�[�Y�ł͂Ȃ��Ƃ���UI��\�����Ȃ��悤�ɂ��邽�߂̕ϐ�


    void Awake() {
        UIActionAssets = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    private void Start() {
        quitSelect = (int)eQuitState.YES;
        pauseSelect = (int)eSTATEPAUSE.RETURNGAME;       // �I���̃��[�h�̏�����
        returnSelect = pauseSelect;                      // �I���̃��[�h�̏�����

        UIBasePosx = 0.0f;  // UI�̕\���ʒu�̊�ʒu������

        notShowPause = false;   // true���\�����Ȃ��Ƃ�

        isConfirm = false;         // �{���ɂ�߂邩���m�肳�ꂽ��true


        //---�L�����o�X���w��
        canvas = GetComponent<Canvas>();

        //---���ԉ�
        PauseFrame = Instantiate(pauseframe);           // �|�[�Y�g
        PauseCharacter = Instantiate(pausecharacter);   // �|�[�Y�̕���
        BackGame = Instantiate(backgame);               // �Q�[���ɖ߂�̕���
        GameEnd = Instantiate(gameend);                 // �Q�[����߂�̕���
        SousaChara = Instantiate(sousachara);           // ������@����
        SousaImage = Instantiate(sousaimage);           // ������@�摜
        OptionChara = Instantiate(optionchara);                   // �I�v�V�����̕���
        SelectBox_1 = Instantiate(selectbox_1);         // �I��g
        SelectBox_2 = Instantiate(selectbox_2);
        Decision = Instantiate(decision);               // ���葀���������
        QuitQuestion = Instantiate(quitquestion);       // �{���ɂ�߂܂�������
        QuitYes = Instantiate(quityes);                 // �{���ɂ�߂܂����C�G�X
        QuitNo = Instantiate(quitno);                   // �{���ɂ�߂܂����́[

        //---�L�����o�X�̎q�ɂ���
        PauseFrame.transform.SetParent(this.transform, false);              // �|�[�Y�g
        SelectBox_1.transform.SetParent(this.canvas.transform, false);      // �I��g
        SelectBox_2.transform.SetParent(this.canvas.transform, false);
        PauseCharacter.transform.SetParent(this.canvas.transform, false);   // �|�[�Y�̕���
        BackGame.transform.SetParent(this.canvas.transform, false);         // �Q�[���ɖ߂�̕���
        GameEnd.transform.SetParent(this.canvas.transform, false);          // �Q�[����߂�̕���
        SousaChara.transform.SetParent(this.canvas.transform, false);       // ������@
        SousaImage.transform.SetParent(this.canvas.transform, false);       // ������@
        OptionChara.transform.SetParent(this.canvas.transform, false);           // �I�v�V�����̕���
        Decision.transform.SetParent(this.canvas.transform, false);         // ���葀���������
        QuitQuestion.transform.SetParent(this.canvas.transform, false);     // �{���ɂ�߂܂�������
        QuitYes.transform.SetParent(this.canvas.transform, false);          // �{���ɂ�߂܂����C�G�X
        QuitNo.transform.SetParent(this.canvas.transform, false);           // �{���ɂ�߂܂����́[



        //---�Q�[���X�^�[�g���͔�\��
        /*
         * TODO�F���I
         */

    }

    private void OnEnable() {
        //---Action�C�x���g��o�^
        //UIActionAssets.UI.LeftStickSelect.started += OnLeftStick;
        //UIActionAssets.UI.RightStickSelect.started += OnRightStick;
        //UIActionAssets.UI.Decision.canceled += OnDecision;


        //---InputAction�̗L����
        UIActionAssets.UI.Enable();
    }


    private void OnDisable() {
        //---InputAction�̖�����
        UIActionAssets.UI.Disable();
    }


    // Update is called once per frame
    void Update() {

        //----- �|�[�Y��UI���o���Ȃ��ׂ�if(���̏������������Ȃ��Ă��₾��������ʂŏ����������) -----
        notShowPause = false; // true���\�����Ȃ��Ƃ�
        //if (GameData.CurrentMapNumber == (int)GameData.eSceneState.BOSS1_SCENE ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial1 ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial2 ||
        //    GameData.CurrentMapNumber == (int)GameData.eSceneState.Tutorial3) {
        //    notShowPause = true;
        //}

        if (!Pause.isPause || notShowPause) {
            //----- �|�[�Y���ł͂Ȃ����̏��� -----
            //���y�Đ�
            //if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE) {
            //    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
            //    if (kitchenBgmObject) {
            //        kitchenBgmObject.GetComponent<AudioSource>().UnPause();
            //    }
            //}

            // ��\��
            /*
             * TODO�F���
             */

            isCalledOncce = false;

            return;
        } else if (Pause.isPause) {
            //----- �|�[�Y���̏��� -----
            if (!isCalledOncce) {
                //----- �|�[�Y�ɓ���������݂̂��� -----
                // �|�[�Y���ɂȂ�����\��
                /*
                 * TODO�F���
                 */

                //���y��~
                //if (GameData.CurrentMapNumber != (int)GameData.eSceneState.BOSS1_SCENE && GameData.CurrentMapNumber != (int)GameData.eSceneState.TITLE_SCENE) {
                //    GameObject kitchenBgmObject = GameObject.Find("BGMObject(Clone)");
                //    if (kitchenBgmObject) {
                //        kitchenBgmObject.GetComponent<AudioSource>().Pause();
                //    }
                //}
            }
            isCalledOncce = true;   // �������if���ɓ���Ȃ��悤�Ƀt���O�𔽓]
        }


        //----- �|�[�Y���̏��� -----
        //---�I��
        //�I�v�V�������J���Ă�Ԃ͖����ɂ���
        if (false) { 
            // �I�v�V����������Ȃ��Ƃ��ɑ��������\��

            // �|�[�Y�ɂȂ�����I��������
        } else {
            // �I�v�V�������̓I�v�V�������̑������������̂ł���
        }


        //---�I�����Ă�����̂������ŕ���
        if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
            //----- �Q�[���ɖ߂� -----
            if (isDecision) // �I�����m��
            {
                // ���艹
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // �|�[�Y����
                Pause.isPause = false;
                // �������
                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
            //----- �ŏI�m�F -----
            //SelectBox_2.GetComponent<Image>().enabled = true;

            //---�{���ɂ������̊m�F���o�����߂̌���
            if (isDecision) {
                // ���艹
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                pauseSelect = returnSelect;

                isConfirm = true;
                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
            if (isDecision) {
                // ���艹
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);

                // �Ƃ肠�������艟����邽�тɔ��]
                //SousaImage.GetComponent<Image>().enabled = !SousaImage.GetComponent<Image>().enabled;

                isDecision = false;
            }
        } else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
            //----- �I�v�V���� -----
            if (isDecision) // �I�����m��
            {
                // ���艹
                //SoundManager.Play(SoundData.eSE.SE_KETTEI, SoundData.IndelibleAudioList);
                // �I�v�V�����}�l�[�W���[���A�N�e�B�u�ɂ���

                // �������
                isDecision = false;
            }
        }
    }


    /// <summary>
    /// ���X�e�B�b�N
    /// </summary>
    /// <param name="obj"></param>
    //private void OnLeftStick(InputAction.CallbackContext obj) {
    //    if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause) {
    //        if (SousaImage.GetComponent<Image>().enabled) {
    //            // �|�[�Y���ł͂Ȃ��Ƃ��A������@�\�����͂͂���
    //            return;
    //        }
    //    }

    //    //---���X�e�b�N�̃X�e�b�N���͂��擾
    //    Vector2 doLeftStick = Vector2.zero;
    //    doLeftStick = LeftStickSelect.ReadValue<Vector2>();
    //    if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
    //        //---�����ł��|���ꂽ�珈���ɓ���
    //        if (doLeftStick.x > 0.0f) {
    //            SelectRight();
    //        } else if (doLeftStick.x < 0.0f) {
    //            SelectLeft();
    //        }
    //    } else {
    //        //---�����ł��|���ꂽ�珈���ɓ���
    //        if (doLeftStick.y > 0.0f) {
    //            SelectUp();
    //        } else if (doLeftStick.y < 0.0f) {
    //            SelectDown();
    //        }
    //    }
    //}

    //private void OnRightStick(InputAction.CallbackContext obj) {
    //    if (!Pause.isPause || SaveManager.canSave || Warp.shouldWarp || GameData.isFadeIn || GameData.isFadeOut || GameOver.GameOverFlag || Optionmanager.activeSelf || notShowPause) {
    //        if (SousaImage.GetComponent<Image>().enabled) {
    //            // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
    //            return;
    //        }
    //    }

    //    //---�E�X�e�b�N�̃X�e�b�N���͂��擾
    //    Vector2 doRightStick = Vector2.zero;
    //    doRightStick = RightStickSelect.ReadValue<Vector2>();
    //    if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {
    //        //---�����ł��|���ꂽ�珈���ɓ���
    //        if (doRightStick.x > 0.0f) {
    //            SelectRight();
    //        } else if (doRightStick.x < 0.0f) {
    //            SelectLeft();
    //        }
    //    } else {
    //        //---�����ł��|���ꂽ�珈���ɓ���
    //        if (doRightStick.y > 0.0f) {
    //            SelectUp();
    //        } else if (doRightStick.y < 0.0f) {
    //            SelectDown();
    //        }
    //    }
    //}

    /// <summary>
    /// ����{�^��
    /// </summary>
    //private void OnDecision(InputAction.CallbackContext obj) {
    //    if (Pause.isPause || !SaveManager.canSave || !Warp.shouldWarp || !GameData.isFadeIn || !GameData.isFadeOut || !GameOver.GameOverFlag || notShowPause) {
    //        // �|�[�Y���ł͂Ȃ��Ƃ��͂͂���
    //        isDecision = true;
    //    }
    //}

    /// <summary>
    /// �I��g�̍��W�X�V
    /// </summary>
    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        //if (pauseSelect == (int)eSTATEPAUSE.RETURNGAME) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = BackGame.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.RETURNTITLE) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = BackTitle.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.QUITGAME) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = GameEnd.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.SOUSA) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = SousaChara.GetComponent<RectTransform>().localPosition;

        //} else if (pauseSelect == (int)eSTATEPAUSE.OPTION) {
        //    SelectBox_1.GetComponent<RectTransform>().localPosition = Option.GetComponent<RectTransform>().localPosition;
        //} else if (pauseSelect == (int)eSTATEPAUSE.QUITQUESTION) {

        //    if (quitSelect == (int)eQuitState.YES) {
        //        SelectBox_2.GetComponent<RectTransform>().localPosition = QuitYes.GetComponent<RectTransform>().localPosition;
        //    } else if (quitSelect == (int)eQuitState.NO) {
        //        SelectBox_2.GetComponent<RectTransform>().localPosition = QuitNo.GetComponent<RectTransform>().localPosition;
        //    }
        //}
    }

    /// <summary>
    /// ������I��
    /// </summary>
    private void SelectUp() {
        // ��
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
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
        //SoundManager.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);
        quitSelect--;
        if (quitSelect < (int)eQuitState.YES) {
            quitSelect = (int)eQuitState.YES;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }
}
