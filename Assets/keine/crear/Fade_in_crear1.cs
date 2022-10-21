//=============================================================================
//
//�t�F�[�h
//
// �쐬��:2022/10/19
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/19 �쐬
//
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Fade_in_crear1 : MonoBehaviour
{
    private Image img = null;
    public FoxByakko start;
    //�A�N�V�����擾�p
    private InputAction _fadeAction;
    private float timer = 0.0f;
    private int frameCount = 0;
    public bool fadeIn = false;
    public bool isFadeOk;
    private bool fading = false;
    public bool fade_out;




    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        fadeIn = true;
    }

    void Update()
    {
        if (frameCount > 2)
        {
            if (isFadeOk)
            {
                if (fadeIn)
                {
                    //�t�F�[�h�� 
                    if (timer < 1)
                    {
                        img.color = new Color(1, 1, 1, 0 + timer);
                    }
                    //�t�F�[�h�A�E�g���� 
                    else
                    {
                        img.color = new Color(1, 1, 1, 1);
                        timer = 0.0f;
                        fadeIn = false;
                        fading = false;
                    }
                    timer += Time.deltaTime / 2;
                }
            }
        }
        ++frameCount;
    }

    public void fade_in_use(Image image)
    {
        img = image;
    }






}

















