//=============================================================================
//
// ステージセレクト処理
//
// 作成日:2022/10/17
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/17 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    // モード
    public enum Mode
    {
        WorldSelectInit,
        WorldSelectUpdate,
        WorldSelectToStageSelect,
        StageSelectInit,
        StageSelectUpdate,
        StageSelectToWorldSelect,
    }
    [Header("モード")]
    public Mode mode = Mode.WorldSelectInit;
    [Header("ワールドセレクトからステージセレクトへの遷移速度")]
    public float speedWorldSelectToStageSelect;
    [Header("ステージセレクトからワールドセレクトへの遷移速度")]
    public float speedStageSelectToWorldSelect;
    // ワールドセレクトからステージセレクトへの遷移補間値
    private float lerpWorldSelectToStageSelect;
    // ワールドセレクトからステージセレクトへ遷移前の初期位置リスト
    private List<Vector3> positionInitWorldSelectToStageSelectList=new List<Vector3>();

    // 入力用///////////////////////////////////////////////
    [Space(30)]
    [HeaderAttribute("【入力用】")]
    // PlayerInput
    [Header("PlayerInput")]
    public PlayerInput playerInput;
    // InputActionイベント
    private InputAction selectAction;   // 選択
    private InputAction dicisionAction; // 決定
    private InputAction backAction;     // 戻る
    // 入力値
    private bool isInputSelect = false; // 選択入力受付フラグ
    private Vector2 inputSelect;        // 選択
    private bool inputDicision;         // 決定
    private bool inputBack;             // 戻る
    /// ///////////////////////////////////////////////////

    // ワールドセレクト用///////////////////////////////////
    [Space(30)]
    [HeaderAttribute("【ワールドセレクト用】")]
    [Header("ワールドセレクト親オブジェクト")]
    public GameObject worldSelectParentObj;
    [Header("カーソルオブジェクト")]
    public GameObject cursorWorldObj;
    [Header("ワールドセレクトオブジェクトリスト")]
    public List<GameObject> selectWorldObjList;
    // ワールド選択番号
    private int selectWorldNo;
    [Header("ワールド選択初期番号")]
    public int selectWorldNoInit;
    [Header("ワールド選択最小番号")]
    public int selectWorldNoMin;
    [Header("ワールド選択最大番号")]
    public int selectWorldNoMax;
    // ワールド選択最大番号の初期値(inspectorで設定した値)
    private int selectWorldNoMaxInit;
    ////////////////////////////////////////////////////////

    // ステージセレクト用/////////////////////////////////
    [Space(30)]
    [HeaderAttribute("【ステージセレクト用】")]
    [Header("ステージセレクト親オブジェクト")]
    public GameObject stageSelectParentObj;
    [Header("選択ワールド親オブジェクト")]
    public GameObject stageSelectSelectWorldParentObj;
    [Header("選択ワールドオブジェクトリスト")]
    public List<GameObject> stageSelectSelectWorldObjList;
    [Header("カーソルオブジェクト")]
    public GameObject cursorStageObj;
    [Header("ステージセレクトオブジェクトリスト")]
    public List<GameObject> selectStageObjList;
    [Header("ステージセレクトオブジェクト_クリア")]
    public Sprite selectStageClearObj;
    [Header("ステージセレクトオブジェクトリスト_未クリア")]
    public Sprite selectStageCanObj;
    [Header("ステージセレクトオブジェクトリスト_できない")]
    public Sprite selectStageCanNotObj;
    // ステージ選択番号
    private int selectStageNo;
    [Header("ステージ選択初期番号")]
    public int selectStageNoInit;
    [Header("ステージ選択最小番号")]
    public int selectStageNoMin;
    [Header("ステージ選択最大番号")]
    public int selectStageNoMax;
    // ステージ選択最大番号の初期値(inspectorで設定した値)
    private int selectStageNoMaxInit;
    /// //////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        #region 入力初期化
        var pInput = playerInput.GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;
        selectAction = actionMap["Select"];
        dicisionAction = actionMap["Dicision"];
        dicisionAction.canceled += OnDicision;
        backAction = actionMap["Back"];
        backAction.canceled += OnBack;
        #endregion

        #region セレクト初期化
        selectWorldNo = selectWorldNoInit;
        cursorWorldObj.transform.position = selectWorldObjList[selectWorldNo - selectWorldNoMin].transform.position;
        selectStageNo = selectStageNoInit;
        cursorStageObj.transform.position = selectStageObjList[selectStageNo - selectStageNoMin].transform.position;
        selectStageNoMaxInit = selectStageNoMax;
        selectWorldNoMaxInit = selectWorldNoMax;

        foreach (GameObject obj in selectWorldObjList)
        {
            positionInitWorldSelectToStageSelectList.Add(obj.transform.position);
        }
        #endregion

        #region サウンド再生
        for (int i = 0; i < SoundData.SelectAudioList.Length; ++i) {
            SoundData.SelectAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager2.Play(SoundData.eBGM.BGM_SELECT, SoundData.SelectAudioList);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region 入力更新
        InputUpdate();
        #endregion

        #region モード別セレクト更新処理
        switch (mode)
        {
            // ワールドセレクト初期化
            case Mode.WorldSelectInit:
                worldSelectParentObj.SetActive(true);
                stageSelectParentObj.SetActive(false);
                selectStageNoMax = selectStageNoMaxInit;

                selectWorldNoMax = 0;
                // ワールドセレクトの表示をクリア状況により変更
                for (int i = selectWorldNoMin + 1; i <= selectWorldNoMaxInit; i++)
                {
                    if (ClearManager.GetClearWorld((i - 1) - selectWorldNoMin))
                    {
                        selectWorldNoMax = i;
                        selectWorldObjList[i - selectWorldNoMin].SetActive(true);
                    }
                    else
                    {
                        selectWorldObjList[i - selectWorldNoMin].SetActive(false);
                    }
                }

                mode = Mode.WorldSelectUpdate;
                break;
            // ワールドセレクト更新
            case Mode.WorldSelectUpdate:
                UpdateSelect(ref selectWorldNo, ref selectWorldNoMin, ref selectWorldNoMax, ref cursorWorldObj, ref selectWorldObjList);
                break;
            // ワールドセレクトからステージセレクトへ
            case Mode.WorldSelectToStageSelect:
                lerpWorldSelectToStageSelect += Time.deltaTime * speedWorldSelectToStageSelect;

                for (int i = 0; i < stageSelectSelectWorldObjList.Count; i++)
                {
                    Vector3 nowPosition;
                    if (i == selectWorldNo)
                    {
                        //左に寄せる
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], stageSelectSelectWorldParentObj.transform.position, lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                        cursorWorldObj.transform.position = nowPosition;
                    }
                    else
                    {
                        //右に掃く
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], new Vector3(15, 0, 0), lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                    }
                }

                if (lerpWorldSelectToStageSelect >= 1.0f)
                {
                    lerpWorldSelectToStageSelect = 1.0f;
                    mode = Mode.StageSelectInit;
                }
                break;
            // ステージセレクト初期化
            case Mode.StageSelectInit:
                worldSelectParentObj.SetActive(false);
                stageSelectParentObj.SetActive(true);
                foreach (GameObject obj in stageSelectSelectWorldObjList)
                {
                    obj.SetActive(false);
                }
                stageSelectSelectWorldObjList[selectWorldNo - selectWorldNoMin].SetActive(true);

                // データが無ければステージ最大数を変更
                for (int i = 0; i < SceneManagerData.mainSceneStrArray.GetLength(1); i++)
                {
                    if (SceneManagerData.mainSceneStrArray[selectWorldNo - selectWorldNoMin, i] == null)
                    {
                        selectStageNoMax = (selectWorldNoMin + i) - 2;
                    }
                }

                // ステージオブジェクトのスプライトをクリア状況により変更
                for (int i = 0; i < SceneManagerData.mainSceneStrArray.GetLength(1); i++)
                {
                    if (i <= selectStageNoMax)
                    {
                        // クリア
                        if (ClearManager.GetClearStage(selectWorldNo - selectWorldNoMin, i))
                        {
                            selectStageObjList[i].GetComponent<SpriteRenderer>().sprite = selectStageClearObj;
                        }
                        // できる
                        else
                        {
                            selectStageObjList[i].GetComponent<SpriteRenderer>().sprite = selectStageCanObj;
                        }
                    }
                    // できない
                    else
                    {
                        selectStageObjList[i].GetComponent<SpriteRenderer>().sprite = selectStageCanNotObj;
                    }
                }



                mode = Mode.StageSelectUpdate;
                break;
            // ステージセレクト更新
            case Mode.StageSelectUpdate:
                UpdateSelect(ref selectStageNo, ref selectStageNoMin, ref selectStageNoMax, ref cursorStageObj, ref selectStageObjList);
                //if (ClearManager.GetClearStage(selectWorldNo - selectWorldNoMin, selectStageNo - selectStageNoMin))
                //{

                //}
                break;
            // ステージセレクトからワールドセレクトへ
            case Mode.StageSelectToWorldSelect:
                worldSelectParentObj.SetActive(true);
                stageSelectParentObj.SetActive(false);
                lerpWorldSelectToStageSelect -= Time.deltaTime * speedStageSelectToWorldSelect;

                for (int i = 0; i < stageSelectSelectWorldObjList.Count; i++)
                {
                    Vector3 nowPosition;
                    if (i == selectWorldNo)
                    {
                        //左を戻す
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], stageSelectSelectWorldParentObj.transform.position, lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                        cursorWorldObj.transform.position = nowPosition;
                    }
                    else
                    {
                        //右を戻す
                        nowPosition = Vector3.Lerp(positionInitWorldSelectToStageSelectList[i - selectWorldNoMin], new Vector3(15, 0, 0), lerpWorldSelectToStageSelect);
                        selectWorldObjList[i - selectWorldNoMin].transform.position = nowPosition;
                    }
                }

                if (lerpWorldSelectToStageSelect <= 0.0f)
                {
                    lerpWorldSelectToStageSelect = 0.0f;
                    mode = Mode.WorldSelectInit;
                }
                break;
        }
        #endregion


        #region 決定
        if (inputDicision == true)
        {
            switch (mode)
            {
                // ワールドセレクトの場合,ワールドセレクトからステージセレクトへの遷移
                case Mode.WorldSelectUpdate:
                    lerpWorldSelectToStageSelect = 0.0f;
                    mode = Mode.WorldSelectToStageSelect;
                    break;
                // ステージセレクトの場合,メインシーンへ
                case Mode.StageSelectUpdate:
                    //SceneManager.LoadScene(mainSceneName[selectWorldNo].List[selectStageNo]);
                    SceneManagerFade.LoadSceneMain(selectWorldNo - selectStageNoMin, selectStageNo - selectStageNoMin);
                    break;
            }
            inputDicision = false;
        }
        #endregion

        #region 戻る
        if (inputBack == true)
        {
            switch (mode)
            {
                // ワールドセレクトの場合,タイトルシーンへ
                case Mode.WorldSelectUpdate:
                    //SceneManager.LoadScene(titleSceneName);
                    SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.Title);
                    break;
                // ステージセレクトの場合,ワールドセレクトへ
                case Mode.StageSelectUpdate:
                    lerpWorldSelectToStageSelect = 1.0f;
                    mode = Mode.StageSelectToWorldSelect;

                    // ステージ選択番号を最小値に戻す
                    selectStageNo = selectStageNoMin;
                    break;
            }
            inputBack = false;
        }
        #endregion
    }

    /// <summary>
    /// 入力更新 
    /// </summary>
    private void InputUpdate()
    {
        inputSelect = selectAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// セレクト処理更新
    /// </summary>
    private void UpdateSelect(ref int selectNo, ref int selectNoMin, ref int selectNoMax,
        ref GameObject cursorObj, ref List<GameObject> selectObjList)
    {
        // 左入力
        if (inputSelect.x < -0.0f)
        {
            if (!isInputSelect)
            {
                selectNo--;
                isInputSelect = true;
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.SelectAudioList);
            }
        }
        // 右入力
        else if (inputSelect.x > 0.0f)
        {
            if (!isInputSelect)
            {
                selectNo++;
                isInputSelect = true;
                SoundManager2.Play(SoundData.eSE.SE_SELECT, SoundData.SelectAudioList);
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

        // カーソル座標変更
        cursorObj.transform.position = selectObjList[selectNo - selectNoMin].transform.position;
    }

    /// <summary>
    /// 決定ボタン
    /// </summary>
    private void OnDicision(InputAction.CallbackContext obj)
    {
        inputDicision = !inputDicision;
        SoundManager2.Play(SoundData.eSE.SE_DICISION, SoundData.SelectAudioList);
    }

    /// <summary>
    /// 戻るボタン
    /// </summary>
    private void OnBack(InputAction.CallbackContext obj)
    {
        inputBack = !inputBack;
        SoundManager2.Play(SoundData.eSE.SE_BACK, SoundData.SelectAudioList);
    }
}
