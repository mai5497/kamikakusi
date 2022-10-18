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
using UnityEngine.SceneManagement;
public class Fade_in002 : MonoBehaviour
{
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    //  private bool fadeIn = false;
    private bool fadeIn = false;

    private string Scene_ikou;

    public Start001 start;
    public bool isFadeOk;

    //アクション取得用
    private InputAction _fadeAction;

    private bool fading = false;
    // Scene _scene;
    public bool fade_out;

    //   public Fade_in Fadein;
    // public bool fade_in;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeIn = true;



        var pInput = GetComponent<PlayerInput>();

        //現在のアクションマップを取得。
        //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
        var actionMap = pInput.currentActionMap;



        //アクションマップからアクションを取得
        _fadeAction = actionMap["Fade"];



    }

    void Update()
    {
        // fade_in = Fadein.fadeIn;

        isFadeOk = start.isFade;

        var current = Keyboard.current;
        if (frameCount > 2)
        {

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

            if (isFadeOk)
            {
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
                    timer += Time.deltaTime / 2;
                }
            }
            // }
        }
        ++frameCount;
    }

    public void fade_in_use(string Scene, Image image)
    {

        fadeIn = true;
        Scene_ikou = Scene;
        //  SceneManager.LoadScene(Scene_ikou);
        img = image;
    }






}

















