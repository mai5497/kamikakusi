//=============================================================================
//
// フェードイン
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
// 編集者:泉優樹
//
// <開発履歴>
// 2022/10/13 作成
// 2022/10/20 編集
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Fade_out003 : MonoBehaviour
{
    //フェードする画像
    private Image img = null;

    private float timer = 0.0f;

    private int frameCount = 0;
   // public bool fadeIn = false;
   //フェードアウト
    private bool fadeOut = false;
    //フェードアウトが終わった
    public bool fadeOut_finish;

    //アクション取得用
  //  private InputAction _fadeAction;

    private static bool fading = true;
    // Scene _scene;
    public bool fade_out;



    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeOut = false;
        fadeOut_finish = false;


      //  var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        // var actionMap = pInput.currentActionMap;



        //アクションマップからアクションを取得
        // _fadeAction = actionMap["Fade"];


        fading = true;
    }

    void Update()
    {
        Keyboard keyboard;
        keyboard = Keyboard.current;
        // シーン遷移
        if (keyboard.f1Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(0, 0);
        }
        if (keyboard.f2Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(0, 1);
        }
        if (keyboard.f3Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(0, 2);
        }
        if (keyboard.f4Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(1, 0);
        }
        if (keyboard.f5Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(1, 1);
        }
        if (keyboard.f6Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(1, 2);
        }
        if (keyboard.f7Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(2, 0);
        }
        if (keyboard.f8Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(2, 1);
        }
        if (keyboard.f9Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(2, 2);
        }
        if (keyboard.f10Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(3, 0);
        }
        if (keyboard.f11Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(3, 1);
        }
        if (keyboard.f12Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneMain(3, 2);
        }
        if (keyboard.digit1Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.Title);
        }
        if (keyboard.digit2Key.wasReleasedThisFrame)
        {
            SceneManagerFade.LoadSceneSub(SceneManagerFade.SubScene.StageSelect);
        }
        // クリアデータ削除
        if (keyboard.digit0Key.wasPressedThisFrame)
        {
            ClearManager.Delete();
        }
        // 全てクリア
        if (keyboard.digit9Key.wasPressedThisFrame)
        {
            ClearManager.SaveClearStageAll();
        }


        var current = Keyboard.current;
        if (frameCount > 2)
        {

         //   bool Fade = _fadeAction.WasPerformedThisFrame();

            fade_out_use(img, fadeOut);

            //if (fadeOut)
            //{

            //    fading = true;
            //フェードアウト開始
            if (fading)
            {

                //    //フェード中 
                if (timer < 1)
                {
                    // Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");

                    img.color = new Color(1, 1, 1, 1 - timer);
                    img.fillAmount = 1 - timer;
                }
                //フェードアウト完了 
                else
                {
                    img.color = new Color(1, 1, 1, 0);
                    img.fillAmount = 1;
                    img.raycastTarget = false;
                    timer = 0.0f;
                    fadeOut = false;
                    //  fadeOut = true;
                    fading = false;
                    // ChangeScene(_scene);
                    fadeOut_finish = true;
                }
                timer += Time.deltaTime * SceneManagerData.speedFadeIn;

            }
                //   }
         //   }

            //if (fading)
            //{
            //    if (fadeOut)
            //    {
            //        //フェードイン中 
            //        if (timer < 1)
            //        {
            //            img.color = new Color(1, 1, 1, 1 + timer);
            //            img.fillAmount = 0 + timer;
            //        }
            //        //フェードイン完了 
            //        else
            //        {
            //            img.color = new Color(1, 1, 1, 1);
            //            img.fillAmount = 1;
            //            img.raycastTarget = false;
            //            timer = 0.0f;
            //            fadeOut = false;
            //            fadeIn = true;
            //            fading = false;
            //        }
            //        timer += Time.deltaTime;

            //    }
            //}
        }
        ++frameCount;
    }

    public void fade_out_use(Image image, bool FadeIn)
    {
        img = image;
        fadeOut = FadeIn;
    }


    //フェードアウトが終わったか
    public bool Fade_out_finish()
    {
        return fadeOut_finish;
    }

    // フェード
    public static bool GetFading()
    {
        return fading;
    }

}

















