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
    private static string Scene_ikou;
    private float timer = 0.0f;
    private static bool fadeIn = false;
    private bool fading = false;

    [System.NonSerialized]
    public bool fade_out;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;
    }

    void Update()
    {
        var current = Keyboard.current;

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
        img = image;
    }

    public static void fade_in_use(string Scene)
    {
        fadeIn = true;
        Scene_ikou = Scene;
    }




}