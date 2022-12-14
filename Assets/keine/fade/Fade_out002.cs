//=============================================================================
//
//フェード
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/18 作成
//
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Fade_out002 : MonoBehaviour
{
    //フェードする画像
    private Image img = null;

    private float timer = 0.0f;

    private int frameCount = 0;
   // public bool fadeIn = false;
   //フェードアウト
    private bool fadeOut = false;
    //フェードアウトが終わった
    public bool fadeOut_finish = false;

    //アクション取得用
  //  private InputAction _fadeAction;

    private bool fading = true;
    // Scene _scene;
    public bool fade_out;



    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeOut = false;

      //  var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
       // var actionMap = pInput.currentActionMap;



        //アクションマップからアクションを取得
       // _fadeAction = actionMap["Fade"];



    }

    void Update()
    {

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
                timer += Time.deltaTime;

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



}

















