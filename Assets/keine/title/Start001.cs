


//=============================================================================
//�X�^�[�g�{�^��
//
//
// �쐬��:2022/10/13
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/18 �쐬
//
//=============================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start001 : MonoBehaviour {

    private InputAction _selectAction;

    //public bool titlle_delete;

    //public Fade_title_haikei1 Titlle;

    //public select sel;

    //public bool isSelect;

    //public Fade_in002 fadein;

    public bool isFade;

    private CP_move_input inputAction;

    private Vector2 selectVal;

    enum eTitleState {
        FROMSTART = 0,
        CONTINUE,
        EXIT
    }

    private enum eQuitState {
        NONE,
        YES,
        NO
    }


    private int state;                  // �I�����̃X�e�[�g
    private int quitSelect;                 // �ŏI�m�F�I��
    private int oldSelect;               // �߂�Ȃ��Ⴂ���Ȃ��I����

    private bool isDicision;    // ����L�[�������ꂽ��
    private bool isExitGame;    // �Q�[���I�����邩

    private Fade_title_haikei1 pressAnyButton;
    private ObservedValue<bool> isPressAnyButton;   // �v���X�G�j�[�{�^�����ꂽ�u�Ԃ����m����

    private bool canDicision;   // ����L�[�������Ă��ǂ���
                                // �v���X�G�j�[�L�[�Ɠ����t���[���ŗ���Ƃ����Ɍ��肵�����ƂɂȂ��Ă��܂�

    private GameObject selectBoxObj;
    private RectTransform selectBoxRT;      // �I��g�̃��N�g�g�����X�t�H�[��
    private GameObject startObj;
    private RectTransform startRT;          // ���߂���̃��N�g�g�����X�t�H�[��
    private GameObject continueObj;
    private RectTransform continueRT;       // ��������̃��N�g�g�����X�t�H�[��

    private GameObject kakuninObj;          // �m�F��ʂ̃|�b�v�A�b�v
    private Image kakuninYes;
    private Image kakuninNo;
    void Awake() {
        inputAction = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {

        state = (int)eTitleState.FROMSTART;
        quitSelect = (int)eQuitState.NONE;
        oldSelect = state;

        canDicision = false;

        pressAnyButton = GameObject.Find("bg").GetComponent<Fade_title_haikei1>();

        isPressAnyButton = new ObservedValue<bool>(false);  // false�ŏ�����
        isPressAnyButton.OnValueChange += () => { canDicision = false; };

        selectBoxObj = GameObject.Find("cursor");
        selectBoxRT = selectBoxObj.GetComponent<RectTransform>();
        selectBoxObj.SetActive(false);
        startObj = GameObject.Find("UI_001");
        startRT = startObj.GetComponent<RectTransform>();
        startObj.SetActive(false);
        continueObj = GameObject.Find("UI_002");
        continueRT = continueObj.GetComponent<RectTransform>();
        continueObj.SetActive(false);
        kakuninObj = GameObject.Find("Kakunin");
        kakuninYes = kakuninObj.transform.Find("Kakunin_charbox_1").GetComponent<Image>();
        kakuninNo = kakuninObj.transform.Find("Kakunin_charbox_2").GetComponent<Image>();
        kakuninObj.SetActive(false);
    }

    private void OnEnable() {
        //�A�N�V�����}�b�v����A�N�V�������擾
        inputAction.UI.Dicision.canceled += Dicision;
        inputAction.UI.Back.canceled += Back;
        _selectAction = inputAction.UI.Select;

        //---InputAction�̗L����
        inputAction.UI.Enable();
    }

    private void OnDisable() {
        //---InputAction�̖�����
        inputAction.UI.Disable();
    }

    // Update is called once per frame
    void Update() {
        //titlle_delete = Titlle.GetTitlle_delete();
        //isSelect = sel.NO1;

        //var current = Keyboard.current;
        isPressAnyButton.Value = pressAnyButton.fading;

        if (isPressAnyButton.Value) {
            selectBoxObj.SetActive(true);
            startObj.SetActive(true);
            continueObj.SetActive(true);
        }

        selectVal = _selectAction.ReadValue<Vector2>();
        //---�����ł��|���ꂽ�珈���ɓ���
        if (state == (int)eTitleState.EXIT) {
            if (selectVal.x > 0.0f) {
                SelectRight();
            } else if (selectVal.x < 0.0f) {
                SelectLeft();
            }
        } else {
            if (selectVal.y > 0.0f) {
                SelectUp();
            } else if (selectVal.y < 0.0f) {
                SelectDown();
            }
        }

        if (state == (int)eTitleState.EXIT) {
            if (quitSelect == (int)eQuitState.NONE) {
                // ������
                quitSelect = (int)eQuitState.YES;
                state = (int)eTitleState.EXIT;
                kakuninObj.SetActive(true);
            }
        }

        if (isDicision) {
            switch (state) {
                case (int)eTitleState.FROMSTART:
                    // �͂��߂���
                    SceneManagerFade.LoadSceneMain(0, 0);
                    isDicision = false;

                    break;
                case (int)eTitleState.CONTINUE:
                    // �Â�����
                    SceneManagerFade.LoadSceneMain(ClearManager.GetNowWorld(), ClearManager.GetNowStage());
                    isDicision = false;

                    break;
                case (int)eTitleState.EXIT:
                    // �Q�[������߂�
                    if (quitSelect == (int)eQuitState.NONE) {


                    } else if (quitSelect == (int)eQuitState.YES) {
                        // �Q�[������߂�
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    } else if (quitSelect == (int)eQuitState.NO) {
                        // NO�Ŗ߂��Ă����Ƃ��̓��Z�b�g
                        quitSelect = (int)eQuitState.NONE;
                        kakuninObj.SetActive(false);
                        state = oldSelect;    // �������܂őI�����Ă��Ƃ���ɃX�e�[�g��߂�
                    }
                    isDicision = false;

                    break;
            }
        }
    }

    private void Dicision(InputAction.CallbackContext obj) {
        if (!canDicision) {
            canDicision = true; // �v���X�G�j�[�L�[�Ɠ����t���[���ł����ɓ���Ȃ��ׂ�if
            return;
        }
        SoundManager2.Play(SoundData.eSE.SE_DICISION, SoundData.TitleAudioList);

        isDicision = true;
    }

    private void Back(InputAction.CallbackContext obj) {
        oldSelect = state;
        state = (int)eTitleState.EXIT;
    }


    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        if (state == (int)eTitleState.FROMSTART) {
            selectBoxRT.localPosition = startRT.localPosition;
        } else if (state == (int)eTitleState.CONTINUE) {
            selectBoxRT.localPosition = continueRT.localPosition;
        } else if (state == (int)eTitleState.EXIT) {

            // �{���ɂ����ł����͑I��g�𓮂����̂ł͂Ȃ�
            // �͂��������̖؂̔̐F���������ۂ����邱�Ƃŕ\������
            if (quitSelect == (int)eQuitState.YES) {
                kakuninYes.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                kakuninNo.color = new Color(0.7f, 0.7f, 0.7f, 1.0f);
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
        state--;
        if (state < (int)eTitleState.FROMSTART) // ��O����
        {
            state = (int)eTitleState.FROMSTART;
        }
        SelectBoxPosUpdete();   // �I��g�̍X�V
    }

    /// <summary>
    /// �������I��
    /// </summary>
    private void SelectDown() {
        // ��
        SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.IndelibleAudioList);

        state++;
        if (state > (int)eTitleState.CONTINUE)   // ��O����
        {
            state = (int)eTitleState.CONTINUE;
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
