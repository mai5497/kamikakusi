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
        BGM_KITCHEN,
        BGM_BOSS1,
        BGM_EXSTAGE,
        BGM_LASTBOSS,

        MAX_BGM
    }

    public enum eSE {       // SE�ԍ�
        SE_JUMP = 0,
        SE_LAND,
        SE_SHIELD,
        SE_REFLECTION,
        SE_REFLECTION_STAR,
        SE_DAMEGE,
        SE_HEAL,
        SE_PLAYER_DEATH,
        SE_BOOS1_DASHU,
        SE_BOOS1_STRAWBERRY,
        SE_BOOS1_KNIFE,
        SE_BOOS1_DAMEGE,
        SE_LASTBOSS_ULT,
        SE_LASTBOSS_BULLET,
        SE_BUROKORI,
        SE_NINJIN,
        SE_TOMATO_BOMB,
        SE_TOMATO_BOUND,
        SE_KETTEI,
        SE_SELECT,
        SE_GAMEOVER,
        SE_GATEOPEN,
        SE_EXTINGUISH,
        SE_SWITCH,

        MAX_SE
    }

    //private static string path = "Assets/SoundData/";   // �p�X�̓r���܂ŁB���ƌ��ɖ��O�Ɗg���q����B
    //private static string[] SEpath = {path+ "�N���b�N.mp3",path+ "�h��.mp3", path + "�r���H��.mp3", path + "���ہ[��.mp3", }; // �����Ŋ��S�ȃp�X�ɂȂ�
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
