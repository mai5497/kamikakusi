//=============================================================================
//
// �G�t�F�N�g�f�[�^
//
// �쐬��:2022/03/18
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/28 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EffectData
{
    /// <summary>
    /// �G�t�F�N�g�̊��蓖��
    /// �ǉ�����ꍇ�͖����K���Ƃ���
    /// EF_�I�u�W�F�N�g��_���e�@�Ƃ��܂��B
    /// ��) EF_PLAYER_SHIELD
    /// </summary>
    public enum eEFFECT
    {
        EF_SMOKE = 0,

        MAX_EF
    }

    //---�G�t�F�N�g�f�[�^�̐������܂Ƃ߂�
    public static ParticleSystem[] EF = new ParticleSystem[(int)eEFFECT.MAX_EF];

    public static bool isSetEffect = false;
    public static bool onceSearchEffect = true;
    public static GameObject[] activeEffect = new GameObject[300];
    //public static ParticleSystem[] activeEffect = new ParticleSystem[(int)eEFFECT.MAX_EF * 5];

    //---�G�t�F�N�g�f�[�^��ǂݍ���
    public static void EFDataSet(ParticleSystem _EF,int i)
    {
        EF[i] = _EF;
    }

}
