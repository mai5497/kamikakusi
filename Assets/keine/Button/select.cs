using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class select : MonoBehaviour
{

    private bool isInputSelect;

    Vector2 inputSelect;
    private InputAction inputAction, _inputAction;


    public int selectNo;

    public int selectNoMin;

    public int selectNoMax;

    public GameObject cursorObj;

    public List<GameObject> selectObjList;

    public bool NO1 = false;



    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        inputAction = actionMap["Select"];
        _inputAction = actionMap["Fade"];


    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;
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
        // 左入力
        if (inputSelect.y > -0.0f)
        {
            if (!isInputSelect)
            {
                selectNo--;
                isInputSelect = true;
            }
        }
        // 右入力
        else if (inputSelect.y < 0.0f)
        {
            if (!isInputSelect)
            {
                selectNo++;
                isInputSelect = true;
            }
        }
        // 入力リセット
        else
        {
            isInputSelect = false;
        }

        // セレクト番号制御
        if (selectNo < selectNoMin)
        {
            selectNo = selectNoMax;
        }
        if (selectNo > selectNoMax)
        {
            selectNo = selectNoMin;
        }


        bool Fade = _inputAction.WasPerformedThisFrame();
        switch(selectNo)
        {
            case 0:
                //ステージセレクト
            break;

          case 1:
                //次のステージへ
          break;

        }


        //タイトル用
        if (selectNo == selectNoMin)
        {
            //はじめから
            NO1 = true;
        }
        else
        {
            NO1 = false;
        }





        // カーソル座標変更
        cursorObj.transform.position = selectObjList[selectNo].transform.position;
    }
}
