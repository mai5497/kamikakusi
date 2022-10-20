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

public class Fade_title_haikei : MonoBehaviour
{
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;

    //アクション取得用
    private InputAction _dicisionAction;

    private bool fading = false;
    public bool title_finish = false;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeIn = true;

        var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        _dicisionAction = actionMap["Disicion"];


    }

    void Update()
    {

        var current = Keyboard.current;
        if (frameCount > 2)
        {

            bool isDicision = _dicisionAction.WasPerformedThisFrame();

            if (isDicision)
            {
                fading = true;
            }
            if (fading)
            {
                if (fadeIn)
                {
                    //フェードイン中 
                    if (timer < 1)
                    {
                        img.color = new Color(1, 1, 1, 1 - timer);
                        img.fillAmount = 1 - timer;
                    }
                    //フェードイン完了 
                    else
                    {
                        img.color = new Color(1, 1, 1, 0);
                        img.fillAmount = 0;
                        img.raycastTarget = false;
                        timer = 0.0f;
                        fadeIn = false;
                        fadeOut = true;
                        fading = false;
                        title_finish = true;
                        GetTitlle_delete();

                       // Debug.Log("ボタン1");

                    }
                    timer += Time.deltaTime;


                }
            }
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
            //  }
        }



        ++frameCount;

    }

    /// <summary>
    /// タイトル削除
    /// </summary>
    /// <returns></returns>
   
    public bool GetTitlle_delete()
    {
        return title_finish;
    }


}