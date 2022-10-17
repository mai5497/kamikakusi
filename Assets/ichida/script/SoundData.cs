//=============================================================================
//
// サウンドデータ
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/03/16 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class SoundData
{
    public enum eBGM {      // BGM番号
        BGM_TITLE = 0,
        BGM_SELECT,
        BGM_GAME01,
        BGM_GAME02,
        BGM_GAME03,

        MAX_BGM
    }

    public enum eSE {       // SE番号
        SE_BACK = 0,
        SE_DICISION,
        SE_SELECT,

        MAX_SE
    }

    public static AudioClip[] BGMClip = new AudioClip[(int)eBGM.MAX_BGM];   // データをまとめて入れる
    public static AudioClip[] SEClip = new AudioClip[(int)eSE.MAX_SE];
    public static float[] SEVolume = new float[(int)eSE.MAX_SE];

    public static AudioSource[] TitleAudioList = new AudioSource[20];    // 一回に同時にならせる数
    public static AudioSource[] IndelibleAudioList = new AudioSource[10];    // 一回に同時にならせる数
    public static AudioSource[] GameAudioList = new AudioSource[30];    // 一回に同時にならせる数

    public static bool isSetSound = false;


    public static void SEDataSet(AudioClip _SE, int i)    // SEのデータを読み込む
    {
        SEClip[i] = _SE;
    }
    public static void BGMDataSet(AudioClip _BGM, int i)    // SEのデータを読み込む
    {
        BGMClip[i] = _BGM;
    }

}
