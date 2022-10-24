


//=============================================================================
//スタートボタン
//
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/18 作成
//
//=============================================================================






using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start001 : MonoBehaviour
{

    private InputAction _dicisionAction;

    //  public Fade_title titlle;

    // public  bool title_finish;


    //GameObject titlle; 


    public bool titlle_delete;

    public Fade_title_haikei1 Titlle;

    public select sel;

    public bool isSelect;

    public Fade_out002 fadeout;

    public Fade_in002 fadein;

    public Image image;

    public Image img;

    public bool finish;

    public bool isFade;

    

    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _dicisionAction = actionMap["Dicision"];

        //  titlle = GameObject.Find("Image"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
        //  Titlle = titlle.GetComponent<Fade_title>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        //  bool Titlle_finish = Titlle.title_finish;


    }

    // Update is called once per frame
    void Update()
    {
        titlle_delete = Titlle.GetTitlle_delete();
        isSelect = sel.NO1;
        finish = fadeout.fadeOut_finish;

        //  Debug.Log(titlle);

        var current = Keyboard.current;
        bool isdicision = _dicisionAction.WasPerformedThisFrame();

        if (isdicision && titlle_delete)
        {
            isFade = true;
        }
        if (isFade) {
            if (isSelect)
            {
                //fadein.fade_in_use("Alpha 1", image);

                // はじめから
                SceneManagerFade.LoadSceneMain(0, 0);
                isFade = false;

                //Debug.Log("ボタン");
                //// SceneManager.LoadScene("Alpha 1");

                //fadeout.fade_out_use(img, true);
                //if (finish)
                //{
                //    fadein.fade_in_use("Alpha 1", image);
                //}

            }
            else
            {
                // つづきから
                SceneManagerFade.LoadSceneMain(ClearManager.GetNowWorld(), ClearManager.GetNowStage());
                isFade = false;
            }
        }


    }
}
