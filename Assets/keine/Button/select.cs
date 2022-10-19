using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class select : MonoBehaviour
{

    private bool isInputSelect;

    Vector2 inputSelect;
    private InputAction inputAction;


    public int selectNo;

    public int selectNoMin;

    public int selectNoMax;

    public GameObject cursorObj;

    public List<GameObject> selectObjList;

    public bool isStartOK = false;



    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        inputAction = actionMap["Select"];

    }

    // Update is called once per frame
    void Update()
    {
        inputSelect = inputAction.ReadValue<Vector2>();


        UpdateSelect(ref selectNo, ref selectNoMin, ref selectNoMax, ref cursorObj, ref selectObjList);





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
    ref GameObject cursorObj, ref List<GameObject> selectObjList)
    {
        // ������
        if (inputSelect.y > -0.0f)
        {
            if (!isInputSelect)
            {
                selectNo--;
                isInputSelect = true;
            }
        }
        // �E����
        else if (inputSelect.y < 0.0f)
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


        if (selectNo == selectNoMin)
        {
            isStartOK = true;
        }
        else
        {
            isStartOK = false;
        }



        // �J�[�\�����W�ύX
        cursorObj.transform.position = selectObjList[selectNo].transform.position;
    }
}
