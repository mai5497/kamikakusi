//=============================================================================
//
//フェード
//
// 作成日:2022/10/13
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/13 作成
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
    private string Scene_ikou;

    public Start001 start;
    public bool isFadeOk;
    public bool fade_out;
    private bool fading = false;
    private bool fadeIn = false;

    //アクション取得用
    private InputAction _fadeAction;

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
        img = image;
    }






}

















