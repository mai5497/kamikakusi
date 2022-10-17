//=============================================================================
//
//�t�F�[�h
//
// �쐬��:2022/10/13
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/13 �쐬
//
//=============================================================================





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Fade_out : MonoBehaviour
{
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    private bool fadeIn = false;
    private bool fadeOut = true;

    private string Scene_ikou;

   

    //�A�N�V�����擾�p
    private InputAction _fadeAction;

    private bool fading = false;
    // Scene _scene;
    public bool fade_out;



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
        _fadeAction = actionMap["Fade"];



    }

    void Update()
    {

        var current = Keyboard.current;
        if (frameCount > 2)
        {

            bool Fade = _fadeAction.WasPerformedThisFrame();

            if (Fade)
            {
                fading = true;
            }
            //if (fading)
            //{
            //    if (fadeIn)
            //    {
            //        //�t�F�[�h�� 
            //        if (timer < 1)
            //        {
            //            img.color = new Color(1, 1, 1, 1 - timer);
            //            img.fillAmount = 1 - timer;
            //        }
            //        //�t�F�[�h�C������ 
            //        else
            //        {
            //            img.color = new Color(1, 1, 1, 0);
            //            img.fillAmount = 0;
            //            img.raycastTarget = false;
            //            timer = 0.0f;
            //            fadeIn = false;
            //            fadeOut = true;
            //            fading = false;
            //            // ChangeScene(_scene);
            //        }
            //        timer += Time.deltaTime;


            //    }
            //}



            if (fading)
            {
                if (fadeOut)
                {
                    //�t�F�[�h�C���� 
                    if (timer < 1)
                    {
                        img.color = new Color(1, 1, 1, 1 + timer);
                        img.fillAmount = 0 + timer;
                    }
                    //�t�F�[�h�A�E�g���� 
                    else
                    {
                        img.color = new Color(1, 1, 1, 1);
                        img.fillAmount = 1;
                        img.raycastTarget = false;
                        timer = 0.0f;
                        fadeOut = false;
                        fadeIn = true;
                        fading = false;

                        SceneManager.LoadScene(Scene_ikou);


                    }
                    timer += Time.deltaTime;

                }
            }
        }
        ++frameCount;
    }

    public void fade_out_use(string Scene,Image image)
    {

        fadeOut = true;
        Scene_ikou = Scene;
       // SceneManager.LoadScene(Scene);
        img = image;
    }






}

















