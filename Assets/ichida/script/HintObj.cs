//=============================================================================
//
// �q���g�̃I�u�W�F�N�g�ɂ���X�N���v�g
//
// �쐬��:2022/10/12
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/12 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObj : MonoBehaviour
{

    [SerializeField]
    private string objName;    // ���̃I�u�W�F�N�g�̖��O���C���X�y�N�^�[�Őݒ肵�Ă���
    [SerializeField]
    private string uraObjName;


    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O

    private mado _mado;     // ���I�u�W�F�N�g�ɂ��Ă��鑋�X�N���v�g

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    //private GameObject canvas;  // �V�[���̃L�����o�X�ɃI�u�W�F�N�g��UI�q���g�ԍ����r���ĕ\������̂�����Ă���̂�
    //                            // �L�����o�X���擾����
    //private ShowHintManager _ShowHint; // ��ŏ������I�u�W�F�N�g��UI�̃q���g�ԍ����r���ĕ\������ׂ̃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        //canvas = GameObject.Find("Canvas");
        //if (canvas != null) {
        //    _ShowHint = canvas.GetComponent<ShowHintManager>();
        //}

        _mado = GameObject.Find("Lens").GetComponent<mado>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWindowColl && CPData.isLook) {
            lookStopTimer -= Time.deltaTime;
            CPData.isRightAnswer = true;
            //if (lookStopTimer < 0) {
            //    if (canvas != null) {
            //        _ShowHint.ShowHintUI(hintNum);  // ���̃I�u�W�F�N�g�̃q���g�ԍ��������Ă���
            //    }
            //}
            /*
             * �����Ńq���g���o�Ȃ��Ȃ����̂ŃR�����g�A�E�g
             * ������x�����ŕ\�����邱�Ƃ�z�肵�Ă��Ȃ��̂�lookStopTimer�̏������͖���
             */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
            _mado.SetLookObjName(objName,uraObjName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null,null);
        }
    }

    public string GetObjName() {
        return objName;
    }
    public string GetUraObjName() {
        return uraObjName;
    }
}
