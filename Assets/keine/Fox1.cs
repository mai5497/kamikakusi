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

public class Fox1 : MonoBehaviour {
    //private GameObject foxWindow;
    [System.NonSerialized]
    //public bool isWindowColl;   // ���Ɠ����������t���O
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float Alpha;    // �ς̃A���t�@�l

   // private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

  //  private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    public bool isByakko;

    public bool byakko;

    public Fox fox;


    // Start is called before the first frame update
    void Start() {
        //foxWindow = GameObject.FindWithTag("FoxWindow");
        //isWindowColl = false;

        //lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        Alpha = 0.0f;
        sr.color = new Color(1, 1, 1, Alpha);
        //���Ղ��o�ė��Ă������������
       
    }

    // Update is called once per frame
    void Update() {
        //----- �������� -----
        byakko = fox.GetByakko_delete();

        if (byakko)
        {
          //  Debug.Log("�ςł�您��������������");

            // CPData.isRightAnswer = true;
            //lookStopTimer -= Time.deltaTime;

            // if (lookStopTimer < 0 && alpha < 1.0f)
            // {
                 Alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, Alpha);
         //   Debug.Log("alpha" + Alpha);

            //}



        }
    }
}
