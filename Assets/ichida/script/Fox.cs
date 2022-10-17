//=============================================================================
//
// ��
//
// �쐬��:2022/10/11
// �쐬��:�ɒn�c�^��
// �ҏW�ҁF���؋��d��
//
// <�J������>
// 2022/10/11 �쐬
// 2022/10/17 ���ς̉��o�ǉ�
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour {
    //private GameObject foxWindow;
    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float alpha;    // �ς̃A���t�@�l

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    private bool isByakko_delete = true;
    private bool isByakko_flag = false;
    private bool isByakko_return = true;


    // Start is called before the first frame update
    void Start() {
        //foxWindow = GameObject.FindWithTag("FoxWindow");
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);
    }

    // Update is called once per frame
    void Update() {
        //----- �������� -----
        if (isWindowColl) {
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;
            if (lookStopTimer < 0 && alpha < 1.0f) {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
            } else if (alpha >= 1.0f) {
                //    isByakko_delete = false;
                //}
            }

            //----- ���ω��o -----
            //if (!isByakko_delete) {
            //    alpha -= Time.deltaTime / appearTime;
            //    sr.color = new Color(1, 1, 1, alpha);
            //    isByakko_flag = true;

            //}
            //if (isByakko_return) {
            //    if (isByakko_flag) {
            //        isByakko_return = false;
            //        //GetByakko_delete();   
            //    }
            //}

        }

        //public bool GetByakko_delete() {
        //    return isByakko_flag;
        //}

    }
}
