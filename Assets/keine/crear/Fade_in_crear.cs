//=============================================================================
//
//フェード
//
// 作成日:2022/10/19
// 作成者:八木橋慧音
//
// <開発履歴>
// 2022/10/19 作成
//
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Fade_in_crear : MonoBehaviour
{
    private SpriteRenderer img = null;
    private FoxByakko start;
    private InputAction _fadeAction;
    private float timer = 0.0f;
    private int frameCount = 0;

    public bool isFadeOk;
    public bool fadeIn = false;
    private bool fading = false;
    public bool fade_out;

    private bool isDelayFade = false;

    private CP_move01 player;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        img.color = new Color(1, 1, 1, 0);
        fadeIn = true;

        start = GameObject.Find("FoxByakko").GetComponent<FoxByakko>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CP_move01>();
    }

    void Update()
    {
        isFadeOk = start.isClear;

        if (frameCount > 2)
        {
            if (isFadeOk)
            {
                player.isClear = true;
                Invoke("DelayFade", 2.0f);
            }
        }
        ++frameCount;

        if (isDelayFade)
        {
            if (fadeIn)
            {
                //フェード中 
                if (timer < 1)
                {
                    img.color = new Color(0, 0, 0, 0 + timer);
                }
                //フェードアウト完了 
                else
                {
                    img.color = new Color(0, 0, 0, 0 + (timer / 4) * 3);
                    timer = 0.0f;
                    fadeIn = false;
                    fading = false;
                }
                timer += Time.deltaTime / 2;
            }
        }
    }

    public void fade_in_use(SpriteRenderer image)
    {
        img = image;
    }

    private void DelayFade()
    {
        isDelayFade = true;
    }




}

















