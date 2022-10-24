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

public class HintObj : MonoBehaviour {

    //[System.NonSerialized]
    public string objName;    // ���̃I�u�W�F�N�g�̖��O���C���X�y�N�^�[�Őݒ肵�Ă���
    //[System.NonSerialized]
    public bool _isHatenaHuman; // �H�ɕϊ����邩
    //[System.NonSerialized]
    public string hatenaObjName;

    //[System.NonSerialized]
    public string uraObjName;
    //[System.NonSerialized]
    public bool _isHatenaFox; // �H�ɕϊ����邩
    //[System.NonSerialized]
    public string hatenaUraName;


    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O

    private mado _mado;     // ���I�u�W�F�N�g�ɂ��Ă��鑋�X�N���v�g

    private GameObject objList;             // �I�u�W�F�N�g�̖��O��\������X�N���v�g�������Ă���
   
    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        _mado = GameObject.Find("Lens").GetComponent<mado>();
    }

    // Update is called once per frame
    void Update() {
        if (isWindowColl && CPData.isLook) {
            CPData.isRightAnswer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
            _mado.SetLookObjName(objName, uraObjName);
            
            //if (_isHatenaHuman) {
            //    _ObjNameListUI.SetText(hatenaObjName, objName);
            //}
            //if (_isHatenaFox) {
            //    _ObjNameListUI.SetText(hatenaUraName, uraObjName);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null, null);
        }
    }

    public string GetObjName() {
        return objName;
    }
    public string GetNormalHatenaStr() {
        return hatenaObjName;
    }

    public string GetUraObjName() {
        return uraObjName;
    }
    public string GetUraHatenaStr() {
        return hatenaUraName;
    }
}
