//=============================================================================
//
//�t�F�[�h
//
// �쐬��:2022/10/20
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/20�쐬
///�Q�[���I�[�o�[�p�̃t�F�[�h
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Fade_in_gemeover : MonoBehaviour
{
    private SpriteRenderer img = null;
    //�A�N�V�����擾�p
    private InputAction _fadeAction;

    private float timer = 0.0f;
    private int frameCount = 0;
    public int isFadeOk;
    public bool fadeIn = false;
    public bool isOver_fade = false;
    public bool fade_out;

    private bool isDelayFade = false;

    private CP_move01 player;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        img.color = new Color(1, 1, 1, 0);
        fadeIn = true;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CP_move01>();
    }

    void Update()
    {
         isFadeOk = CPData.lookCnt;
        var current = Keyboard.current;
        if (frameCount > 2)
        {
            if (isFadeOk <= 0)
            {
                player.isGameOver = true;
                Invoke("DelayFade", 4.0f);
            }
        }
        ++frameCount;

        if (isDelayFade)
        {
            if (fadeIn)
            {
                //�t�F�[�h�� 
                if (timer < 1)
                {
                    img.color = new Color(0, 0, 0, 0 + timer);
                }
                //�t�F�[�h�A�E�g���� 
                else
                {
                    img.color = new Color(0, 0, 0, 0 + (timer / 4) * 3);
                    timer = 0.0f;
                    fadeIn = false;
                    isOver_fade = true;
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

















