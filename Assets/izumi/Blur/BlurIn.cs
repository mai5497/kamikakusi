//=============================================================================
//
// �ڂ�������
//
// �쐬��:2022/10/12
// �쐬��:��D��
//
// <�J������>
// 2022/10/12 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurIn : MonoBehaviour
{
    [Header("�ڂ����������邩")]
    public bool isBlur;
    public enum BlurMode{ 
        Normal,
        PressInit,
        Press
    }
    [Header("�ڂ����̎��")]
    public BlurMode blurMode = BlurMode.Normal;
    [Header("�ڂ����̍ő�l")]
    public float valueBlurMax;
    [Header("�ڂ������x(�ʏ�)")]
    public float speedNormal = 0.01f;
    [Header("�ڂ������x(������)")]
    public float speedPress = 0.025f;
    [Header("�ڂ����񕜑��x")]
    public float speedHeal = 0.1f;
    [Header("�������������b(������)")]
    public float pressInitSecond = 1.0f;
    // ����������
    private float pressSecond;

    // �ڂ�����������}�e���A��
    private Material mBlur;
    // �ڂ����̒l
    private float valueBlur = 0;

    [Header("�ڂ����̌��݂̕�Ԓl�@���m�F�p�ŕ\�����Ă܂�")]
    public float valueLerpNowBlur;

    // Start is called before the first frame update
    void Start()
    {
        // �}�e���A���̎擾
        mBlur = this.GetComponent<Image>().material;

        // �ڂ������\��
        isBlur = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �ڂ����������邩
        if (isBlur)
        {
            // �ڂ������x
            float speedBlur = 0;
            // �ڂ����̎��
            switch (blurMode)
            {
                // �ʏ�
                case BlurMode.Normal:
                    speedBlur = speedNormal;
                    pressSecond = pressInitSecond;
                    break;
                // ����������
                case BlurMode.PressInit:
                    speedBlur = speedNormal;
                    pressSecond -= Time.deltaTime;
                    if (pressSecond < 0)
                    {
                        blurMode = BlurMode.Press;
                    }
                    break;
                // ������
                case BlurMode.Press:
                    speedBlur = speedPress;
                    break;
            }
            // �ڂ����̕�Ԓl�̌���
            valueLerpNowBlur += speedBlur * Time.deltaTime;
            if (valueLerpNowBlur > 1.0f)
            {
                valueLerpNowBlur = 1.0f;
            }
            // �ڂ����̒l������
            valueBlur = Mathf.Lerp(0, valueBlurMax, valueLerpNowBlur);
            // �ڂ����̒l���Z�b�g
            mBlur.SetFloat("_Blur", valueBlur);
        }
        else
        {
            // �ڂ����̒l�����Z�b�g
            mBlur.SetFloat("_Blur", 0);

            // �ڂ����̕�Ԓl�̉�
            valueLerpNowBlur -= speedHeal * Time.deltaTime;
            if (valueLerpNowBlur < 0)
            {
                valueLerpNowBlur = 0;
            }
        }
    }
}
