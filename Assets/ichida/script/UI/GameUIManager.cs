//=============================================================================
//
// ゲームのUIの管理
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/17 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    private GameObject canvasObj;       // UI表示のためのキャンバスがあるオブジェクトの格納
    private Canvas canvas;              // UI表示のためのキャンバス

    [SerializeField]
    private GameObject gameUI;          // UIのあるパネルをインスペクターで格納
    private GameObject gameUIEntity;    // Instatiateで実体化させる用の変数

    [SerializeField]
    private GameObject hintUI;         // ヒントとオブジェクトの名前を表示するUI
    private GameObject hintUIEntity;   // ヒントとオブジェクトの名前を表示するUIの実体

    private CP_move_input UIActionAssets;        // InputActionのUIを扱う

    private GameObject tutorial;
    private bool isTutorial;


    void Awake() {
        UIActionAssets = new CP_move_input();            // InputActionインスタンスを生成
    }

    // Start is called before the first frame update
    void Start() {
        tutorial = GameObject.Find("StageSelectMgr");
        if (tutorial) {
            isTutorial = true;
        } else {
            isTutorial = false;
        }

        //----- キャンバスが見つからなかったらキャンバスを作成する -----
        canvasObj = GameObject.Find("Canvas");
        if (canvasObj) {
            canvas = canvasObj.GetComponent<Canvas>();
        } else {
            canvasObj = new GameObject();
            canvasObj.name = "Canvas";
            canvasObj.AddComponent<Canvas>();

            canvas = canvasObj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }

        //----- デバッグ用 -----
        if (!gameUI) {
#if UNITY_EDITOR
            Debug.LogError("<color=red>gameUIのパネル入れ忘れてる！</color>");
#endif
        }else {
            gameUIEntity = Instantiate(gameUI);
            gameUIEntity.transform.SetParent(canvas.transform, false);
        }

        if (!hintUI) {
#if UNITY_EDITOR
            Debug.LogError("<color=red>hintUIのパネル入れ忘れてる！</color>");
#endif
        } else {
            hintUIEntity = Instantiate(hintUI);
            hintUIEntity.transform.SetParent(canvas.transform, false);
            hintUIEntity.SetActive(false);   // 始めは非表示
        }
    }

    private void OnEnable() {
        //---Actionイベントを登録
        UIActionAssets.UI.Switching.started += OnSwitchUI;

        //---InputActionの有効化
        UIActionAssets.UI.Enable();
    }

    private void OnDisable() {
        //---InputActionの無効化
        UIActionAssets.UI.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        if (CPData.isObjNameUI) {
            hintUIEntity.SetActive(true);
            gameUIEntity.SetActive(false);
        } else {
            hintUIEntity.SetActive(false);
            gameUIEntity.SetActive(true);
        }
    }

    private void OnSwitchUI(InputAction.CallbackContext obj) {
        if (isTutorial) {
            return;
        }
        CPData.isObjNameUI = !CPData.isObjNameUI;
    }       
}
