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
    private GameObject fox; // �ς̃I�u�W�F�N�g(�e)

    private string lookObjName; // �����Ă���I�u�W�F�N�g�̖��O
    private string lookUraObjName; // �����Ă���I�u�W�F�N�g�̖��O

    private Kokkurisan kokkurisan;

    [SerializeField]
    private List<ParticleSystem> efSmokeList;


    [Header("�ς̏o�����W")]
    public Vector3 posFox;

    private int oldLookCount;

    // Start is called before the first frame update
    void Start() {
        kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();

        fox = GameObject.Find("Fox");

        oldLookCount = CPData.lookCnt;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(lookObjName);



        kokkurisan.kituneAnswerStr = lookUraObjName;
        kokkurisan.normalAnswerStr = lookObjName;

        if (kokkurisan.isClear) {
            fox.transform.position = this.transform.position + posFox;
        }

        if(oldLookCount != CPData.lookCnt) {
            PlayEffect();
            oldLookCount = CPData.lookCnt;
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

    public void PlayEffect() {
        foreach (ParticleSystem ef in efSmokeList) {
            ef.Play();
        }
    }

}
