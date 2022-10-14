//=============================================================================
//
// ��
//
// �쐬��:2022/10/11
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/11 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bifor_fox001 : MonoBehaviour
{
    //private GameObject foxWindow;
    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float alpha;    // �ς̃A���t�@�l

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    public bool isByakko_delete = true;
    public bool isByakko_flag = false;
    private bool isByakko_return = true;
    // Start is called before the first frame update
    void Start()
    {
        //foxWindow = GameObject.FindWithTag("FoxWindow");
        isWindowColl = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);
    }

    // Update is called once per frame
    void Update()
    {

        //----- �������� -----
        if (isWindowColl)
        {
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;

            if (lookStopTimer < 0 && alpha < 1.0f && isByakko_delete)
            {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
                if (alpha >= 1.0f)
                {
                    isByakko_delete = false;
                    //  Debug.Log("���Ղ�����您����������������������");
                    // isByakko_flag = true;
                }
                //  Debug.Log("���ՂłĂ�您�������[�[�[�[�[�[");

            }

        }

        if (!isByakko_delete)
        {
            alpha -= Time.deltaTime / appearTime;
            sr.color = new Color(1, 1, 1, alpha);
            isByakko_flag = true;
            //  Debug.Log("���Տ�����");


        }
        if (isByakko_return)
        {
            if (isByakko_flag)
            {
                isByakko_return = false;
                GetByakko_delete();
            }
        }
    }
    public bool GetByakko_delete()
    {

        //  Debug.Log("���Տ�����");

        return isByakko_flag;

    }

}
