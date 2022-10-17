//=============================================================================
//
// �T�E���h�f�[�^
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/03/16 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SoundData
{
    public enum eBGM {      // BGM�ԍ�
        BGM_TITLE = 0,
        BGM_SELECT,
        BGM_GAME01,
        BGM_GAME02,
        BGM_GAME03,

        MAX_BGM
    }

    public enum eSE {       // SE�ԍ�
        SE_BACK = 0,
        SE_DICISION,
        SE_SELECT,

        MAX_SE
    }

    public static AudioClip[] BGMClip = new AudioClip[(int)eBGM.MAX_BGM];   // �f�[�^���܂Ƃ߂ē����
    public static AudioClip[] SEClip = new AudioClip[(int)eSE.MAX_SE];
    public static float[] SEVolume = new float[(int)eSE.MAX_SE];

    public static AudioSource[] TitleAudioList = new AudioSource[20];    // ���ɓ����ɂȂ点�鐔
    public static AudioSource[] IndelibleAudioList = new AudioSource[10];    // ���ɓ����ɂȂ点�鐔
    public static AudioSource[] GameAudioList = new AudioSource[30];    // ���ɓ����ɂȂ点�鐔

    public static bool isSetSound = false;


    public static void SEDataSet(AudioClip _SE, int i)    // SE�̃f�[�^��ǂݍ���
    {
        SEClip[i] = _SE;
    }
    public static void BGMDataSet(AudioClip _BGM, int i)    // SE�̃f�[�^��ǂݍ���
    {
        BGMClip[i] = _BGM;
    }

}
