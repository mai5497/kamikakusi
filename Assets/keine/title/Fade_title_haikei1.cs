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
        fadeIn = true;
    }

    void Update()
    {
        var current = Keyboard.current;
        if (frameCount > 2)
        {
            var anyKey = current.anyKey;
            if (GamePadManager.PressAnyButton(0))
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
                    }
                    ////フェードイン完了 
                    else
                    {
                        title_finish = true;
                    }
                    timer += Time.deltaTime;


                }
            }
        }
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