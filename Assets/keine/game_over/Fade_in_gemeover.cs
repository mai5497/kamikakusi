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
    private float timer = 0.0f;
    private int frameCount = 0;
    //  private bool fadeIn = false;
    public bool fadeIn = false;

    //  private string Scene_ikou;

    // public FoxByakko gameover;
    public int isFadeOk;

    //�A�N�V�����擾�p
    private InputAction _fadeAction;

    private bool fading = false;
    // Scene _scene;
    public bool fade_out;

    //   public Fade_in Fadein;
    // public bool fade_in;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        img.color = new Color(1, 1, 1, 0);
        // img.fillAmount = 1;
        // img.raycastTarget = true;
        fadeIn = true;



        // var pInput = GetComponent<PlayerInput>();

        //���݂̃A�N�V�����}�b�v���擾�B
        //������Ԃ�PlayerInput�R���|�[�l���g��inspector��DefaultMap
        //   var actionMap = pInput.currentActionMap;



        //�A�N�V�����}�b�v����A�N�V�������擾
        //  _fadeAction = actionMap["Fade"];



    }

    void Update()
    {
        // fade_in = Fadein.fadeIn;

         isFadeOk = CPData.lookCnt;
       // isFadeOk = 0;
        var current = Keyboard.current;
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



            //if (fading)
            //{
            //  Debug.Log(isFadeOk);
            //  Debug.Log("�N���A�I�I�I");
            if (isFadeOk <= 0)
            {
                if (fadeIn)
                {
                    //�t�F�[�h�� 
                    if (timer < 1)
                    {

                        img.color = new Color(0, 0, 0, 0 + timer);
                        // img.fillAmount = 0 + timer;
                    }
                    //�t�F�[�h�A�E�g���� 
                    else
                    {
                        img.color = new Color(0, 0, 0, 0 + (timer / 4) * 3);
                        //img.fillAmount = 0;
                        //  img.raycastTarget = false;
                        timer = 0.0f;
                        fadeIn = false;
                        // fadeIn = true;
                        fading = false;

                        // SceneManager.LoadScene(Scene_ikou);
                    }
                    timer += Time.deltaTime / 2;
                }
            }
            // }
        }
        ++frameCount;
    }

    public void fade_in_use(SpriteRenderer image)
    {

        // fadeIn = true;
        //  Scene_ikou = Scene;
        //  SceneManager.LoadScene(Scene_ikou);
        img = image;
    }






}
















