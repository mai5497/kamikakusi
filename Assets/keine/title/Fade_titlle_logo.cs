//=============================================================================
//
//�t�F�[�h
//
// �쐬��:2022/10/13
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/18 �쐬
//
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Fade_titlle_logo : MonoBehaviour
{
    public Fade_title_haikei1 Titlle;
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    public bool fadeIn = true;
    private bool fading = true;
    bool Titlle_finish;
    private string Scene_ikou;
    public float titlle_alpha = 1.0f;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;
    }

    void Update()
    {

        Titlle_finish = Titlle.title_finish;
        if (fading)
        {
            //�t�F�[�h�� 
            if (timer < 1)
            {
                img.color = new Color(1, 1, 1, 0 + timer);
                img.fillAmount = 0 + timer;
            }
            //�t�F�[�h�A�E�g���� 
            else
            {
                img.color = new Color(1, 1, 1, 1);
                img.fillAmount = 0;
                img.raycastTarget = false;
                timer = 0.0f;
                fadeIn = false;
                fading = false;
                GetFadeIn_False();
            }
            timer += Time.deltaTime / 2;

        }

        if (Titlle_finish)
        {
            timer += Time.deltaTime * 3;
        }
    }



    public bool GetFadeIn_False()
    {
        return fadeIn;
    }


}











