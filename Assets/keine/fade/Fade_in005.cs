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

public class Fade_in005 : MonoBehaviour
{
    private static Image img = null;
    private static string Scene_ikou;
    private float timer = 0.0f;
    private static bool fadeIn = false;
    private bool fading = false;
    public Fade_out002 fade;
    private float time ;

    [System.NonSerialized]
    public bool fade_out;
    public clear_animation Clear;
    private bool is_Clear;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;


        time = 0.0f;
    }

    void Update()
    {
        //var current = Keyboard.current;

        //if (fadeIn)
        //{
        //フェード中 
      //  Debug.Log(is_Clear);
        is_Clear = Clear.isClear;

        if (is_Clear)
        {
         //   Debug.Log("afdafdsfss");
            SceneManager.LoadScene("All_Clear");
            // time += Time.deltaTime;

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
                    fading = false;
                    fade.fadeOut_finish = false;
                    // SceneManager.LoadScene(Scene_ikou);
                    SceneManager.LoadScene("All_Clear");
                }
                timer += Time.deltaTime * SceneManagerData.speedFadeOut;
         
        }
        //   }}
    }

    public static void fade_in_use(string Scene, Image image)
    {

        fadeIn = true;
        Scene_ikou = Scene;
        img = image;
    }

    public static void fade_in_use(string Scene)
    {
        fadeIn = true;
        Scene_ikou = Scene;
    }

    public static bool GetFadeIn()
    {
        return fadeIn;
    }


}