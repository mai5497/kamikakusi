//=============================================================================
//
// ���@��Œ��g�����z�������Ǝv���āA���O�K��
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

using UnityEngine.InputSystem;

public class mado : MonoBehaviour {
    private GameObject fox; // �ς̃I�u�W�F�N�g
    private CircleCollider2D foxCol;    // �ς̃R���C�_�[
    private Fox _Fox;   //�σI�u�W�F�N�g�ɂ��Ă���σN���X

    private CircleCollider2D windowCol;

    private string lookObjName; // �����Ă���I�u�W�F�N�g�̖��O
    private string lookUraObjName; // �����Ă���I�u�W�F�N�g�̖��O

    private Kokkurisan kokkurisan;

    // Start is called before the first frame update
    void Start() {
        fox = GameObject.FindWithTag("Fox");
        _Fox = fox.GetComponent<Fox>();
        foxCol = fox.GetComponent<CircleCollider2D>();

        windowCol = GetComponent<CircleCollider2D>();

        kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(lookObjName);

        kokkurisan.kituneAnswerStr = lookUraObjName;
        kokkurisan.normalAnswerStr = lookObjName;
        if (!CPData.isLens) {
            return; // ���g�p���łȂ���΂��̃X�N���v�g�ɂ�邱�Ƃ͂Ȃ��̂ŕԂ�
        }

        // �w�肵���͈͂Ƀ��m�����邩�̔���
        if (Physics2D.OverlapCircle(fox.transform.position, 0) == windowCol) {
            if (CPData.isLook) {
                _Fox.isWindowColl = true;
                Debug.Log("���ˁI");
            } else {
                _Fox.isWindowColl = false;
            }
        }



    }

    /// <summary>
    /// �����Ă���I�u�W�F�N�g�̖��O��ۑ�
    /// </summary>
    /// <param name="_lookObjName"></param>
    public void SetLookObjName(string _lookObjName,string _lookUraObjName) {
        lookObjName = _lookObjName;
        lookUraObjName = _lookUraObjName;
    }

    /// <summary>
    /// �����Ă���I�u�W�F�N�g�̖��O�̎擾
    /// </summary>
    /// <returns></returns>
    public string GetLookObjName() {
        return lookObjName;
    }
    public string GetLookUraObjName() {
        return lookUraObjName;
    }

}
