using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start001 : MonoBehaviour
{

    private InputAction _fadeAction;

    //  public Fade_title titlle;

    // public  bool title_finish;


    //GameObject titlle; 


    public bool titlle;

    public Fade_title Titlle;

    public select sel;

    public bool isSelect;

    public Fade_out fade;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _fadeAction = actionMap["Fade"];

        //  titlle = GameObject.Find("Image"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
        //  Titlle = titlle.GetComponent<Fade_title>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        //  bool Titlle_finish = Titlle.title_finish;


    }

    // Update is called once per frame
    void Update()
    {
        titlle = Titlle.GetTitlle_delete();
        isSelect = sel.isStartOK;


        Debug.Log(titlle);

        var current = Keyboard.current;
        bool Fade = _fadeAction.WasPerformedThisFrame();

        if (Fade && titlle && isSelect)
        {
            Debug.Log("ボタン");
           // SceneManager.LoadScene("Alpha 1");
           fade.fade_out_use("Alpha 1",image);
        }


    }
}
