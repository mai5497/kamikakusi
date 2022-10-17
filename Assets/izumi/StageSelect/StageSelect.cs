//=============================================================================
//
// �X�e�[�W�Z���N�g����
//
// �쐬��:2022/10/17
// �쐬��:��D��
//
// <�J������>
// 2022/10/17 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    // ���[�h
    public enum Mode
    {
        WorldSelectInit,
        WorldSelectUpdate,
        WorldSelectToStageSelect,
        StageSelectInit,
        StageSelectUpdate,
        StageSelectToWorldSelect,
    }
    [Header("���[�h")]
    public Mode mode = Mode.WorldSelectInit;
    [Header("���C���V�[����")]
    public string mainSceneName;
    [Header("�^�C�g���V�[����")]
    public string titleSceneName;
    [Header("���[���h�Z���N�g����X�e�[�W�Z���N�g�ւ̑J�ڑ��x")]
    public float speedWorldSelectToStageSelect;
    // ���[���h�Z���N�g����X�e�[�W�Z���N�g�ւ̑J�ڕ�Ԓl
    private float lerpWorldSelectToStageSelect;
    private List<Vector3> positionInitWorldSelectToStageSelectList=new List<Vector3>();

    // ���͗p///////////////////////////////////////////////
    [Space(30)]
    [HeaderAttribute("�y���͗p�z")]
    // PlayerInput
    [Header("PlayerInput")]
    public PlayerInput playerInput;
    // InputAction�C�x���g
    private InputAction selectAction;   // �I��
    private InputAction dicisionAction; // ����
    private InputAction backAction;     // �߂�
    // ���͒l
    private bool isInputSelect = false; // �I����͎�t�t���O
    private Vector2 inputSelect;        // �I��
    private bool inputDicision;         // ����
    private bool inputBack;             // �߂�
    /// ///////////////////////////////////////////////////

    // ���[���h�Z���N�g�p///////////////////////////////////
    [Space(30)]
    [HeaderAttribute("�y���[���h�Z���N�g�p�z")]
    [Header("���[���h�Z���N�g�e�I�u�W�F�N�g")]
    public GameObject worldSelectParentObj;
    [Header("�J�[�\���I�u�W�F�N�g")]
    public GameObject cursorWorldObj;
    [Header("���[���h�Z���N�g�I�u�W�F�N�g���X�g")]
    public List<GameObject> selectWorldObjList;
    // ���[���h�I��ԍ�
    private int selectWorldNo;
    [Header("���[���h�I�������ԍ�")]
    public int selectWorldNoInit;
    [Header("���[���h�I���ŏ��ԍ�")]
    public int selectWorldNoMin;
    [Header("���[���h�I���ő�ԍ�")]
    public int selectWorldNoMax;
    ////////////////////////////////////////////////////////

    // �X�e�[�W�Z���N�g�p/////////////////////////////////
    [Space(30)]
    [HeaderAttribute("�y�X�e�[�W�Z���N�g�p�z")]
    [Header("�X�e�[�W�Z���N�g�e�I�u�W�F�N�g")]
    public GameObject stageSelectParentObj;
    [Header("�I�����[���h�e�I�u�W�F�N�g")]
    public GameObject stageSelectSelectWorldParentObj;
    [Header("�I�����[���h�I�u�W�F�N�g���X�g")]
    public List<GameObject> stageSelectSelectWorldObjList;
    [Header("�J�[�\���I�u�W�F�N�g")]
    public GameObject cursorStageObj;
    [Header("�X�e�[�W�Z���N�g�I�u�W�F�N�g���X�g")]
    public List<GameObject> selectStageObjList;
    // �X�e�[�W�I��ԍ�
    private int selectStageNo;
    [Header("�X�e�[�W�I�������ԍ�")]
    public int selectStageNoInit;
    [Header("�X�e�[�W�I���ŏ��ԍ�")]
    public int selectStageNoMin;
    [Header("�X�e�[�W�I���ő�ԍ�")]
    public int selectStageNoMax;
    /// //////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        #region ���͏�����
        var pInput = playerInput.GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;
        selectAction = actionMap["Select"];
        dicisionAction = actionMap["Dicision"];
        dicisionAction.canceled += OnDicision;
        backAction = actionMap["Back"];
        backAction.canceled += OnBack;
        #endregion

        #region �Z���N�g������
        selectWorldNo = selectWorldNoInit;
        cursorWorldObj.transform.position = selectWorldObjList[selectWorldNo - selectWorldNoMin].transform.position;
        selectStageNo = selectStageNoInit;
        cursorStageObj.transform.position = selectStageObjList[selectStageNo - selectStageNoMin].transform.position;

        foreach (GameObject obj in selectWorldObjList)
        {
            positionInitWorldSelectToStageSelectList.Add(obj.transform.position);
        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        #region ���͍X�V
        InputUpdate();
        #endregion

        #region ���[�h�ʃZ���N�g�X�V����
        switch (mode)
        {
            // ���[���h�Z���N�g������
            case Mode.WorldSelectInit:
                worldSelectParentObj.SetActive(true);
                stageSelectParentObj.SetActive(false);
                mode = Mode.WorldSelectUpdate;
                break;
            // ���[���h�Z���N�g�X�V
            case Mode.WorldSelectUpdate:
                UpdateSelect(ref selectWorldNo, ref selectWorldNoMin, ref selectWorldNoMax, ref cursorWorldObj, ref selectWorldObjList);
                break;
            // ���[���h�Z���N�g����X�e�[�W�Z���N�g��
            case Mode.WorldSelectToStageSelect:
                lerpWorldSelectToStageSelect += Time.deltaTime * speedWorldSelectToStageSelect;

                for (int i = 0; i < stageSelectSelectWorldObjList.Count; i++)
                {
                    Vector3 nowPosition;
                    if (i == selectWorldNo)
                    {
                        //���Ɋ񂹂�
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], stageSelectSelectWorldParentObj.transform.position, lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                        cursorWorldObj.transform.position = nowPosition;
                    }
                    else
                    {
                        //�E�ɑ|��
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], new Vector3(15, 0, 0), lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                    }
                }

                if (lerpWorldSelectToStageSelect >= 1.0f)
                {
                    lerpWorldSelectToStageSelect = 0;
                    mode = Mode.StageSelectInit;
                }
                break;
            // �X�e�[�W�Z���N�g������
            case Mode.StageSelectInit:
                worldSelectParentObj.SetActive(false);
                stageSelectParentObj.SetActive(true);
                foreach (GameObject obj in stageSelectSelectWorldObjList)
                {
                    obj.SetActive(false);
                }
                stageSelectSelectWorldObjList[selectWorldNo - selectWorldNoMin].SetActive(true);
                mode = Mode.StageSelectUpdate;
                break;
            // �X�e�[�W�Z���N�g�X�V
            case Mode.StageSelectUpdate:
                UpdateSelect(ref selectStageNo, ref selectStageNoMin, ref selectStageNoMax, ref cursorStageObj, ref selectStageObjList);
                break;
        }
        #endregion


        #region ����
        if (inputDicision == true)
        {
            switch (mode)
            {
                // ���[���h�Z���N�g�̏ꍇ,���[���h�Z���N�g����X�e�[�W�Z���N�g�ւ̑J��
                case Mode.WorldSelectUpdate:
                    mode = Mode.WorldSelectToStageSelect;
                    break;
                // �X�e�[�W�Z���N�g�̏ꍇ,���C���V�[����
                case Mode.StageSelectUpdate:
                    SceneManager.LoadScene(mainSceneName);
                    break;
            }
            inputDicision = false;
        }
        #endregion

        #region �߂�
        if (inputBack == true)
        {
            switch (mode)
            {
                // ���[���h�Z���N�g�̏ꍇ,�^�C�g���V�[����
                case Mode.WorldSelectUpdate:
                    SceneManager.LoadScene(titleSceneName);
                    break;
                // �X�e�[�W�Z���N�g�̏ꍇ,���[���h�Z���N�g��
                case Mode.StageSelectUpdate:
                    mode = Mode.WorldSelectInit;
                    break;
            }
            inputBack = false;
        }
        #endregion
    }

    /// <summary>
    /// ���͍X�V 
    /// </summary>
    private void InputUpdate()
    {
        inputSelect = selectAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// �Z���N�g�����X�V
    /// </summary>
    private void UpdateSelect(ref int selectNo, ref int selectNoMin, ref int selectNoMax,
        ref GameObject cursorObj, ref List<GameObject> selectObjList)
    {
        // ������
        if (inputSelect.x < -0.0f)
        {
            if (!isInputSelect)
            {
                selectNo--;
                isInputSelect = true;
            }
        }
        // �E����
        else if (inputSelect.x > 0.0f)
        {
            if (!isInputSelect)
            {
                selectNo++;
                isInputSelect = true;
            }
        }
        // ���̓��Z�b�g
        else
        {
            isInputSelect = false;
        }

        // �Z���N�g�ԍ�����
        if (selectNo < selectNoMin)
        {
            selectNo = selectNoMax;
        }
        if (selectNo > selectNoMax)
        {
            selectNo = selectNoMin;
        }

        // �J�[�\�����W�ύX
        cursorObj.transform.position = selectObjList[selectNo - selectNoMin].transform.position;
    }

    /// <summary>
    /// ����{�^��
    /// </summary>
    private void OnDicision(InputAction.CallbackContext obj)
    {
        inputDicision = !inputDicision;
    }

    /// <summary>
    /// �߂�{�^��
    /// </summary>
    private void OnBack(InputAction.CallbackContext obj)
    {
        inputBack = !inputBack;
    }
}
