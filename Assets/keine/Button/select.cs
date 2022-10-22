using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class select : MonoBehaviour {

    //private bool isInputSelect;

    Vector2 inputSelect;
    //private InputAction _selectAction, _dicisionAction;

    private CP_move_input action;        // InputAction������

    private bool isDicision;        // ����L�[

    private GameObject fox;         // �σI�u�W�F�N�g
    private FoxByakko _FoxByakko;   // �N���A�t���O�擾�p

    public int selectNo;

    public int selectNoMin;

    public int selectNoMax;

    public GameObject cursorObj;

    public List<GameObject> selectObjList;

    public bool NO1 = false;


    // Start is called before the first frame update
    void Start() {
        //var pInput = GetComponent<PlayerInput>();
        //var actionMap = pInput.currentActionMap;

        ////�A�N�V�����}�b�v����A�N�V�������擾
        //_selectAction = actionMap["Select"];
        //_dicisionAction = actionMap["Dicision"];

        isDicision = false;
        //isInputSelect = false;
        fox = GameObject.Find("FoxByakko");
        if (fox) {
            _FoxByakko = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();
        }
    }


    // Update is called once per frame
    void Update() {
        if (fox) {
            if (_FoxByakko.isClear || CPData.lookCnt < 1) {
                Gamepad gamepad = Gamepad.current;
                if (gamepad != null) {
                    if (gamepad.buttonSouth.wasReleasedThisFrame) {
                        isDicision = !isDicision;
                    }
                }
                Keyboard keyboard = Keyboard.current;
                if (keyboard.enterKey.wasReleasedThisFrame) {
                    isDicision = !isDicision;
                }

                UpdateSelect(ref selectNo, ref selectNoMin, ref selectNoMax, ref cursorObj, ref selectObjList);
            }
        } else {
            UpdateSelect(ref selectNo, ref selectNoMin, ref selectNoMax, ref cursorObj, ref selectObjList);
        }




    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectNo"></param>
    /// <param name="selectNoMin"></param>
    /// <param name="selectNoMax"></param>
    /// <param name="cursorObj"></param>
    /// <param name="selectObjList"></param>
    private void UpdateSelect(ref int selectNo, ref int selectNoMin, ref int selectNoMax,
    ref GameObject cursorObj, ref List<GameObject> selectObjList) {
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null) {
            inputSelect.y = gamepad.dpad.y.ReadValue();
        }
        Keyboard keyboard = Keyboard.current;
        if (keyboard.wKey.wasReleasedThisFrame) {
            inputSelect.y = 1.0f;
        }else if (keyboard.sKey.wasReleasedThisFrame) {
            inputSelect.y = -1.0f;
        } else {
            inputSelect.y = 0.0f;
        }

        // ������
        if (inputSelect.y > -0.0f) {
            //if (!isInputSelect) {
                selectNo--;
                //isInputSelect = true;
            //}
        }
        // �E����
        else if (inputSelect.y < 0.0f) {
           // if (!isInputSelect) {
                selectNo++;
                //isInputSelect = true;
            //}
        }
        // ���̓��Z�b�g
        //else {
        //    isInputSelect = false;
        //}

        // �Z���N�g�ԍ�����
        if (selectNo < selectNoMin) {
            selectNo = selectNoMax;
        }
        if (selectNo > selectNoMax) {
            selectNo = selectNoMin;
        }

        if (isDicision) {
            switch (selectNo) {
                case 0:
                    //�X�e�[�W�Z���N�g�֑J��
                    SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);
                    break;

                case 1:
                    //���̃X�e�[�W��
                    SceneManagerFade.LoadSceneNextStage();
                    break;

            }
            isDicision = false;
        }


        //�^�C�g���p
        if (selectNo == selectNoMin) {
            //�͂��߂���
            NO1 = true;
        } else {
            NO1 = false;
        }

        // �J�[�\�����W�ύX
        cursorObj.transform.position = selectObjList[selectNo].transform.position;
    }

    private void Dicision(InputAction.CallbackContext obj) {
        isDicision = !isDicision;
    }

    private void Select(InputAction.CallbackContext obj) {
        inputSelect = obj.ReadValue<Vector2>();
    }

}