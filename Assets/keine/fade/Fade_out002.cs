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

public class Fade_out002 : MonoBehaviour
{
    //�t�F�[�h����摜
    private Image img = null;

    private float timer = 0.0f;

    private int frameCount = 0;
   // public bool fadeIn = false;
   //�t�F�[�h�A�E�g
    private bool fadeOut = false;
    //�t�F�[�h�A�E�g���I�����
    public bool fadeOut_finish = false;

    //�A�N�V�����擾�p
  //  private InputAction _fadeAction;

    private bool fading = true;
    // Scene _scene;
    public bool fade_out;



    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeOut = false;

      //  var pInput = GetComponent<PlayerInput>();

        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
       // var actionMap = pInput.currentActionMap;



        //�A�N�V�����}�b�v����A�N�V�������擾
       // _fadeAction = actionMap["Fade"];



    }

    void Update()
    {

        var current = Keyboard.current;
        if (frameCount > 2)
        {

         //   bool Fade = _fadeAction.WasPerformedThisFrame();

            fade_out_use(img, fadeOut);

            //if (fadeOut)
            //{

            //    fading = true;
            //�t�F�[�h�A�E�g�J�n
            if (fading)
            {

                //    //�t�F�[�h�� 
                if (timer < 1)
                {
                    // Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");

                    img.color = new Color(1, 1, 1, 1 - timer);
                    img.fillAmount = 1 - timer;
                }
                //�t�F�[�h�A�E�g���� 
                else
                {
                    img.color = new Color(1, 1, 1, 0);
                    img.fillAmount = 1;
                    img.raycastTarget = false;
                    timer = 0.0f;
                    fadeOut = false;
                    //  fadeOut = true;
                    fading = false;
                    // ChangeScene(_scene);
                    fadeOut_finish = true;
                }
                timer += Time.deltaTime;

            }
                //   }
         //   }

            //if (fading)
            //{
            //    if (fadeOut)
            //    {
            //        //�t�F�[�h�C���� 
            //        if (timer < 1)
            //        {
            //            img.color = new Color(1, 1, 1, 1 + timer);
            //            img.fillAmount = 0 + timer;
            //        }
            //        //�t�F�[�h�C������ 
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
            //}
        }
        ++frameCount;
    }

    public void fade_out_use(Image image, bool FadeIn)
    {
        img = image;
        fadeOut = FadeIn;
    }


    //�t�F�[�h�A�E�g���I�������
    public bool Fade_out_finish()
    {
        return fadeOut_finish;
    }



}
















