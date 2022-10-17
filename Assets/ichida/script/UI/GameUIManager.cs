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

public class GameUIManager : MonoBehaviour
{
    private GameObject canvasObj;   // UI表示のためのキャンバスがあるオブジェクトの格納
    private Canvas canvas;  // UI表示のためのキャンバス

    [SerializeField]
    private GameObject gameUI;          // UIのあるパネルをインスペクターで格納
    private GameObject gameUIEntity;    // Instatiateで実体化させる用の変数

    // Start is called before the first frame update
    void Start() {
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
            Debug.LogError("<color=red>UIのパネル入れ忘れてる！</color>");
#endif
        }else {
            gameUIEntity = Instantiate(gameUI);
            gameUIEntity.transform.SetParent(canvas.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
