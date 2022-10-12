//=============================================================================
//
// �q���g�\����UI�ƃI�u�W�F�N�g�̉˂���������
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
using UnityEngine.UI;


public class ShowHintManager : MonoBehaviour {
    private Image[] hintsimage = new Image[5];          // �ő�q���g��5���낤���Ďv���Ă邩��T��
    private GameObject[] hintsobj = new GameObject[5];  // �^�O�����A�X�N���v�g�擾���y���邽�߈��UI���Q�[���I�u�W�F�N�g�Ŏ擾
    private HintUI[] _hintUIs = new HintUI[5];          // ������UI�̃q���g�ԍ����Ǘ����Ă���
    private UIOnOff[] _UIOnOff = new UIOnOff[5];        // UI�̕\����\�����Ǘ�����׎擾

    private GameObject hintbox;
    private UIOnOff hintOnOff;

    // Start is called before the first frame update
    void Start() {
        hintsobj = GameObject.FindGameObjectsWithTag("UIHint"); // �q���g�^�O���t�����S�ẴI�u�W�F�N�g���擾����

        //----- ��������UI�̃I�u�W�F�N�g���q���g�ԍ����ɕ��בւ� -----
        for (int i = 0; i < hintsobj.Length; i++) {    // �q���g�ԍ���HintUi�N���X�������Ă���׎擾
            _hintUIs[i] = hintsobj[i].GetComponent<HintUI>();
        }
        GameObject _work;    // ���בւ��p�Ɏg�����[�N�̈� 
        HintUI _hintUI;
        // �q���g�ԍ����ɕ��בւ�
        for (int j = 0; j < hintsobj.Length; j++) {
            for (int i = 0; i < hintsobj.Length; i++) {
                if (_hintUIs[i].HintNum != i) {
                    // �O�_����
                    _work = hintsobj[_hintUIs[i].HintNum];      // �ꎞ�ޔ�
                    _hintUI = _hintUIs[_hintUIs[i].HintNum];

                    hintsobj[_hintUIs[i].HintNum] = hintsobj[i];
                    _hintUIs[_hintUIs[i].HintNum] = _hintUIs[i];

                    hintsobj[i] = _work;
                    _hintUIs[i] = _hintUI;
                }
            }
        }
        // ���בւ����UI��Image���擾����
        for (int i = 0; i < hintsobj.Length; i++) {
            hintsimage[i] = hintsobj[i].GetComponent<Image>();
            _UIOnOff[i] = hintsobj[i].GetComponent<UIOnOff>();
        }

        // �q���g�̃{�b�N�X�̎擾
        hintbox = GameObject.Find("hintbar");
        hintOnOff = hintbox.GetComponent<UIOnOff>();
    }

    /// <summary>
    /// �n���ꂽ�q���g�ԍ��ɑΉ�����UI��\������
    /// </summary>
    /// <param name="_hintNum"></param>
    public void ShowHintUI(int _hintNum) {
        hintOnOff.UIOn();
        _UIOnOff[_hintNum].UIOn();
    }


    /// <summary>
    /// �n���ꂽ�q���g�ԍ��ɑΉ�����UI���\���ɂ���
    /// </summary>
    /// <param name="_hintNum"></param>
    public void HideHintUI(int _hintNum) {
        hintOnOff.UIOff();
        _UIOnOff[_hintNum].UIOff();
    }
}
