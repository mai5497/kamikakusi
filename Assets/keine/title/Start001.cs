


//=============================================================================
//�X�^�[�g�{�^��
//
//
// �쐬��:2022/10/13
// �쐬��:���؋��d��
//
// <�J������>
// 2022/10/18 �쐬
//
//=============================================================================






using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Start001 : MonoBehaviour
{

    private InputAction _fadeAction;

    //  public Fade_title titlle;

    // public  bool title_finish;


    //GameObject titlle; 


    public bool titlle_delete;

    public Fade_title_haikei Titlle;

    public select sel;

    public bool isSelect;

    public Fade_out002 fadeout;

    public Fade_in002 fadein;

    public Image image;

    public Image img;

    public bool finish;

    public bool isFade;

    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        _fadeAction = actionMap["Fade"];

        //  titlle = GameObject.Find("Image"); //Unity�������I�u�W�F�N�g�̖��O����擾���ĕϐ��Ɋi�[����
        //  Titlle = titlle.GetComponent<Fade_title>(); //unitychan�̒��ɂ���UnityChanScript���擾���ĕϐ��Ɋi�[����
        //  bool Titlle_finish = Titlle.title_finish;


    }

    // Update is called once per frame
    void Update()
    {
        titlle_delete = Titlle.GetTitlle_delete();
        isSelect = sel.isStartOK;
        finish = fadeout.fadeOut_finish;

        //  Debug.Log(titlle);

        var current = Keyboard.current;
        bool Fade = _fadeAction.WasPerformedThisFrame();

        if (Fade&& titlle_delete)
        {
            isFade = true;
        }
        if (isSelect&&isFade)
        {
            fadein.fade_in_use("Alpha 1", image);
            //Debug.Log("�{�^��");
            //// SceneManager.LoadScene("Alpha 1");

            //fadeout.fade_out_use(img, true);
            //if (finish)
            //{
            //    fadein.fade_in_use("Alpha 1", image);
            //}

        }


    }
}