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

public class Fade_title_haikei1 : MonoBehaviour
{
    private SpriteRenderer img = null;
    private SpriteRenderer oder = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;

    //アクション取得用
    private InputAction _disicionAction;

    private bool fading = false;
    public bool title_finish = false;
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        img.color = new Color(1, 1, 1, 1);

        oder = GetComponent<SpriteRenderer>();
        oder.sortingOrder = 1;

        //img.fillAmount = 1;
        //img.raycastTarget = true;
        fadeIn = true;

        //  var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        //   var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        //  _disicionAction = actionMap["AnyKey"];


    }

    void Update()
    {

        var current = Keyboard.current;
        // var current = Gamepad.current;
        if (frameCount > 2)
        {
            //bool fade = false;

            //Gamepad gamepad = Gamepad.current;
            //if (gamepad != null) {
            //    if (gamepad.buttonSouth.wasReleasedThisFrame ||
            //        gamepad.buttonNorth.wasReleasedThisFrame ||
            //        gamepad.buttonWest.wasReleasedThisFrame ||
            //        gamepad.buttonEast.wasReleasedThisFrame) {
            //        fade = !fade;
            //    }
            //}
            //Keyboard keyboard = Keyboard.current;
            //if (keyboard.enterKey.wasReleasedThisFrame) {
            //    fade = !fade;
            //}


            var anyKey = current.anyKey;
            if (  GamePadManager.PressAnyButton(0) )
            {
                fading = true;
            }
                if (anyKey.wasPressedThisFrame)
                {
                    fading = true;
                }
            if (fading)
            {
                if (fadeIn)
                {
                    oder.sortingOrder = -3;

                    ////フェードイン中 
                    if (timer < 1)
                    {
                        //    img.color = new Color(1, 1, 1, 1 - timer);
                        //   // img.fillAmount = 1 - timer;
                    }
                    ////フェードイン完了 
                    else
                    {
                        title_finish = true;
                        //    img.color = new Color(1, 1, 1, 0);
                        //   // img.fillAmount = 0;
                        //   // img.raycastTarget = false;
                        //    timer = 0.0f;
                        //    fadeIn = false;
                        //    fadeOut = true;
                        //    fading = false;

                        //    GetTitlle_delete();

                        //   // Debug.Log("ボタン1");

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