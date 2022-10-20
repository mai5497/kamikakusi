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

public class Fade_title_haikei : MonoBehaviour
{
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;

    //�A�N�V�����擾�p
    private InputAction _dicisionAction;

    private bool fading = false;
    public bool title_finish = false;
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        img.fillAmount = 1;
        img.raycastTarget = true;
        fadeIn = true;

        var pInput = GetComponent<PlayerInput>();

        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _dicisionAction = actionMap["Disicion"];


    }

    void Update()
    {

        var current = Keyboard.current;
        if (frameCount > 2)
        {

            bool isDicision = _dicisionAction.WasPerformedThisFrame();

            if (isDicision)
            {
                fading = true;
            }
            if (fading)
            {
                if (fadeIn)
                {
                    //�t�F�[�h�C���� 
                    if (timer < 1)
                    {
                        img.color = new Color(1, 1, 1, 1 - timer);
                        img.fillAmount = 1 - timer;
                    }
                    //�t�F�[�h�C������ 
                    else
                    {
                        img.color = new Color(1, 1, 1, 0);
                        img.fillAmount = 0;
                        img.raycastTarget = false;
                        timer = 0.0f;
                        fadeIn = false;
                        fadeOut = true;
                        fading = false;
                        title_finish = true;
                        GetTitlle_delete();

                       // Debug.Log("�{�^��1");

                    }
                    timer += Time.deltaTime;


                }
            }
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
            //  }
        }



        ++frameCount;

    }

    /// <summary>
    /// �^�C�g���폜
    /// </summary>
    /// <returns></returns>
   
    public bool GetTitlle_delete()
    {
        return title_finish;
    }


}