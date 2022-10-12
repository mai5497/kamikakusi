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
    private int hintNum;    // ���Ԗڂ̃q���g�̃I�u�W�F�N�g���i�[
                            // ���̔ԍ��ƑΉ�����q���g���o�邽��
                            // �q���g���ł��ݒ肪�K�v
    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    //private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    private GameObject canvas;  // �V�[���̃L�����o�X�ɃI�u�W�F�N�g��UI�q���g�ԍ����r���ĕ\������̂�����Ă���̂�
                                // �L�����o�X���擾����
    private ShowHintManager _ShowHint; // ��ŏ������I�u�W�F�N�g��UI�̃q���g�ԍ����r���ĕ\������ׂ̃X�N���v�g


    // Start is called before the first frame update
    void Start()
    {
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        canvas = GameObject.Find("Canvas");
        _ShowHint = canvas.GetComponent<ShowHintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWindowColl) {
            lookStopTimer -= Time.deltaTime;    
            if (lookStopTimer < 0) {
                _ShowHint.ShowHintUI(hintNum);  // ���̃I�u�W�F�N�g�̃q���g�ԍ��������Ă���
            }
            /*
             * ������x�����ŕ\�����邱�Ƃ�z�肵�Ă��Ȃ��̂�lookStopTimer�̏������͖���
             */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "FoxWindow") {
            isWindowColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }
}
