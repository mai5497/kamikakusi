using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class select : MonoBehaviour
{

    //private bool isInputSelect;

    Vector2 inputSelect;
    Vector2 inputSelectCon;
    //private InputAction _selectAction, _dicisionAction;

    private CP_move_input action;        // InputActionを扱う

    private bool isDicision;        // 決定キー

    private GameObject fox;         // 狐オブジェクト
    private FoxByakko _FoxByakko;   // クリアフラグ取得用

    public int selectNo;

    public int selectNoMin;

    public int selectNoMax;

    public GameObject cursorObj;

    public List<GameObject> selectObjList;

    public Fade_in_gemeover over;

    public bool NO1 = false;
    public clear_animation Clear;

    // Start is called before the first frame update
    void Start()
    {
        //var pInput = GetComponent<PlayerInput>();
        //var actionMap = pInput.currentActionMap;

        ////アクションマップからアクションを取得
        //_selectAction = actionMap["Select"];
        //_dicisionAction = actionMap["Dicision"];

        isDicision = false;
        //isInputSelect = false;
        fox = GameObject.Find("FoxByakko");
        if (fox)
        {
            _FoxByakko = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();
           // over = GameObject.Find("fade").GetComponent<Fade_in_gemeover>();
        }

        Clear = GameObject.Find("Clear").GetComponent<clear_animation>();
        over = GameObject.Find("fade_over").GetComponent<Fade_in_gemeover>();
    }


    // Update is called once per frame
    void Update()
    {
        if (fox)
        {
            if (_FoxByakko.isClear || CPData.lookCnt < 1)
            {
                Gamepad gamepad = Gamepad.current;
                if (gamepad != null)
                {
                    if (gamepad.buttonSouth.wasReleasedThisFrame)
                    {
                        isDicision = !isDicision;
                    }
                }
                Keyboard keyboard = Keyboard.current;
                if (keyboard.enterKey.wasReleasedThisFrame)
                {
                    isDicision = !isDicision;
                }

                UpdateSelect(ref selectNo, ref selectNoMin, ref selectNoMax, ref cursorObj, ref selectObjList);
            }
        }
        else
        {
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
    ref GameObject cursorObj, ref List<GameObject> selectObjList)
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (gamepad.dpad.up.wasReleasedThisFrame || gamepad.leftStick.up.wasReleasedThisFrame)
            {
                inputSelectCon.y = 1.0f;
            }
            else if (gamepad.dpad.down.wasReleasedThisFrame || gamepad.leftStick.down.wasReleasedThisFrame)
            {
                inputSelectCon.y = -1.0f;
            }
            else
            {
                inputSelectCon.y = 0.0f;
            }
        }
        Keyboard keyboard = Keyboard.current;
        if (keyboard.wKey.wasReleasedThisFrame)
        {
            inputSelect.y = 1.0f;
        }
        else if (keyboard.sKey.wasReleasedThisFrame)
        {
            inputSelect.y = -1.0f;
        }
        else
        {
            inputSelect.y = 0.0f;
        }

        // 左入力
        if (inputSelect.y > -0.0f || inputSelectCon.y > 0.0f)
        {
            //if (!isInputSelect) {
            selectNo--;
            if (SceneManager.GetActiveScene().name == SceneManagerData.titleScene)
            {
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            }
            else
            {
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
            }
            //isInputSelect = true;
            //}
        }
        // 右入力
        else if (inputSelect.y < 0.0f || inputSelectCon.y < 0.0f)
        {
            // if (!isInputSelect) {
            selectNo++;
            if (SceneManager.GetActiveScene().name == SceneManagerData.titleScene)
            {
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.TitleAudioList);
            }
            else
            {
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.GameAudioList);
            }
            //isInputSelect = true;
            //}
        }
        // 入力リセット
        //else {
        //    isInputSelect = false;
        //}

        // セレクト番号制御
        if (selectNo < selectNoMin)
        {
            selectNo = selectNoMax;
        }
        if (selectNo > selectNoMax)
        {
            selectNo = selectNoMin;
        }
        bool pushAbutton = false;
        //  if (isDicision) {
        if (gamepad != null)
        {
            pushAbutton = gamepad.aButton.wasReleasedThisFrame;


        }
        //    gamepad.aButton.wasReleasedThisFrame


        if (keyboard.enterKey.wasReleasedThisFrame || pushAbutton)
        {
            switch (selectNo)
            {
                case 0:
                    //ステージセレクトへ遷移
                    SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);
                    break;

                case 1:
                    //次のステージへ
                    //   SceneManagerFade.LoadSceneNextStage();
                    if (Clear.isClear == true)
                    {
                        //クリアなら
                        SceneManagerFade.LoadSceneNextStage();
                    }
                    if (over.isOver_fade == true)
                    {
                        //ゲームオーバーなら
                        SceneManagerFade.LoadSceneMain(SceneManagerData.nowWorldNo, SceneManagerData.nowStageNo);
                      //  Application.LoadLevel(Application.loadedLevelName);
                    }

                    break;

            }
            isDicision = false;
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

    private void Dicision(InputAction.CallbackContext obj)
    {
        isDicision = !isDicision;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        inputSelect = obj.ReadValue<Vector2>();
    }

}