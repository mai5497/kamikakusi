


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

    private InputAction _dicisionAction, _selectAction;

    //public bool titlle_delete;

    //public Fade_title_haikei1 Titlle;

    //public select sel;

    public bool isSelect;

    public Fade_in002 fadein;

    public bool isFade;

    private CP_move_input inputAction;

    private Vector2 selectVal;

    enum eTitleState {
        FROMSTART = 0,
        CONTINUE
    }

    private int state;

    private bool isDicision;    // ����L�[�������ꂽ��

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


    void Awake() {
        inputAction = new CP_move_input();            // InputAction�C���X�^���X�𐶐�
    }

    // Start is called before the first frame update
    void Start() {

        state = (int)eTitleState.FROMSTART;

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
    }

    private void OnEnable() {
        //�A�N�V�����}�b�v����A�N�V�������擾
        inputAction.UI.Dicision.canceled += Dicision;
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
        if (selectVal.y > 0.0f) {
            SelectUp();
        } else if (selectVal.y < 0.0f) {
            SelectDown();
        }

        if (isDicision) {
            switch (state) {
                case (int)eTitleState.FROMSTART:
                    // �͂��߂���
                    SceneManagerFade.LoadSceneMain(0, 0);
                    break;
                case (int)eTitleState.CONTINUE:
                    // �Â�����
                    SceneManagerFade.LoadSceneMain(ClearManager.GetNowWorld(), ClearManager.GetNowStage());
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

    private void SelectBoxPosUpdete() {
        /*
         * �I��g�̈ʒu�̍X�V
         * ���ꂼ��̕�����RectTransform�ƍ��킹�邱�Ƃœ����ʒu�ɕ\�����ł���
         */
        if (state == (int)eTitleState.FROMSTART) {
            selectBoxRT.localPosition = startRT.localPosition;
        } else if (state == (int)eTitleState.CONTINUE) {
            selectBoxRT.localPosition = continueRT.localPosition;
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
}
