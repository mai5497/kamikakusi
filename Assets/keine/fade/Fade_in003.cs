//=============================================================================
//
// フェードアウト
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
// 編集者:泉優樹
//
// <開発履歴>
// 2022/10/13 作成
// 2022/10/19 編集
//
//=============================================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Fade_in003 : MonoBehaviour
{
    private static Image img = null;
    private float timer = 0.0f;
    //  private bool fadeIn = false;
    private static bool fadeIn = false;

    private static string Scene_ikou;

    //アクション取得用
    private InputAction _fadeAction;

    private bool fading = false;
    // Scene _scene;

    [System.NonSerialized]
    public bool fade_out;

    //   public Fade_in Fadein;
    // public bool fade_in;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;



        var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        //var actionMap = pInput.currentActionMap;



        //アクションマップからアクションを取得
        //_fadeAction = actionMap["Fade"];



    }

    void Update()
    {
        // fade_in = Fadein.fadeIn;

        var current = Keyboard.current;

        // bool Fade = _fadeAction.WasPerformedThisFrame();

        //if (Fade)
        //{
        //    fading = true;
        //}
        //if (fading)
        //{
        //    if (fadeIn)
        //    {
        //        //フェード中 
        //        if (timer < 1)
        //        {
        //            img.color = new Color(1, 1, 1, 1 - timer);
        //            img.fillAmount = 1 - timer;
        //        }
        //        //フェードイン完了 
        //        else
        //        {
        //            img.color = new Color(1, 1, 1, 0);
        //            img.fillAmount = 0;
        //            img.raycastTarget = false;
        //            timer = 0.0f;
        //            fadeIn = false;
        //            fadeOut = true;
        //            fading = false;
        //            // ChangeScene(_scene);
        //        }
        //        timer += Time.deltaTime;


        //    }
        //}



        //if (fading)
        //{

        if (fadeIn)
        {
            //フェード中 
            if (timer < 1)
            {
                img.color = new Color(1, 1, 1, 0 + timer);
                img.fillAmount = 0 + timer;
            }
            //フェードアウト完了 
            else
            {
                img.color = new Color(1, 1, 1, 1);
                img.fillAmount = 0;
                img.raycastTarget = false;
                timer = 0.0f;
                fadeIn = false;
                // fadeIn = true;
                fading = false;

                SceneManager.LoadScene(Scene_ikou);
            }
            timer += Time.deltaTime * SceneManagerData.speedFadeOut;
        }
    }

    public static void fade_in_use(string Scene, Image image)
    {

        fadeIn = true;
        Scene_ikou = Scene;
        //  SceneManager.LoadScene(Scene_ikou);
        img = image;
    }

    public static void fade_in_use(string Scene)
    {

        fadeIn = true;
        Scene_ikou = Scene;
        //  SceneManager.LoadScene(Scene_ikou);
    }




}