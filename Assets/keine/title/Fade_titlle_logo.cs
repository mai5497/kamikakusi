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
    private Image img = null;
    private float timer = 0.0f;
    private int frameCount = 0;
    //  private bool fadeIn = false;
    public bool fadeIn = true;

    private string Scene_ikou;

    //public Start001 start;
    //public bool isFadeOk;

    ////�A�N�V�����擾�p
    //private InputAction _fadeAction;

    private bool fading = true;
    //// Scene _scene;
    //public bool fade_out;

    //   public Fade_in Fadein;
    // public bool fade_in;

    public Fade_title_haikei1 Titlle;
    bool Titlle_finish;
    public float titlle_alpha = 1.0f;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.fillAmount = 1;
        img.raycastTarget = true;
       // fadeIn = true;



        //  var pInput = GetComponent<PlayerInput>();

        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        //  var actionMap = pInput.currentActionMap;



        ////�A�N�V�����}�b�v����A�N�V�������擾
        //_fadeAction = actionMap["Fade"];



    }

    void Update()
    {

        Titlle_finish = Titlle.title_finish;
        // fade_in = Fadein.fadeIn;

        //  isFadeOk = start.isFade;

        //var current = Keyboard.current;
        if (frameCount > 2)
        {

            // bool Fade = _fadeAction.WasPerformedThisFrame();

            //if (Fade)
            //{
            //    fading = true;
            //}
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

                //if (isFadeOk)
                //{
                //    if (fadeIn)
                //    {
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
                    // fadeIn = true;
                    fading = false;
                    GetFadeIn_False();
                    // SceneManager.LoadScene(Scene_ikou);
                }
                timer += Time.deltaTime / 2;

            }

           // Debug.Log(Titlle_finish);
            if (Titlle_finish)
            {
                //if (timer < 1)
                //{
               // Debug.Log("sssssssssssssssssss");
              //  img.color = new Color(1, 1, 1, 1 - timer);
              //  img.fillAmount = 1 - timer;
                //   }
                //�t�F�[�h�A�E�g���� 
                //else
                //{
                //    img.color = new Color(1, 1, 1, 0);
                //    img.fillAmount = 0;
                //    // img.raycastTarget = false;
               // timer = 0.0f;
                //    //  fadeIn = false;
                //    // fadeIn = true;
                //    // fading = false;
                //    titlle_alpha = 0.0f;
                //    // Titlle_finish = false;
                //    // SceneManager.LoadScene(Scene_ikou);
                //}



                //    //}
                //    // }
                //}
                timer += Time.deltaTime*3;

            }
          
            //public void fade_in_use(string Scene, Image image)
            //{

            //    fadeIn = true;
            //    Scene_ikou = Scene;
            //    //  SceneManager.LoadScene(Scene_ikou);
            //    img = image;
            //}






        }


        ++frameCount;


    }

    public bool GetFadeIn_False()
    {
        return fadeIn;
    }


}











