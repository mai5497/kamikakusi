//=============================================================================
//
// データマネージャー
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
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    /*
     * セーブデータの管理・音のデータの管理など素材のデータの管理をする(予定)
     * タイトルで呼び出しをする。ぜったい。
     * 
     * 
     * 
     */

    static int TargetFrame = 60;

    //-----------------------------------------------------
    // BGM・SE
    //-----------------------------------------------------
    [SerializeField]
    private AudioClip bgm_title;
    [SerializeField]
    private AudioClip bgm_select;
    [SerializeField]
    private AudioClip bgm_game01;
    [SerializeField]
    private AudioClip bgm_game02;
    [SerializeField]
    private AudioClip bgm_game03;


    [SerializeField]
    private AudioClip se_back;
    [SerializeField]
    private AudioClip se_decision;
    [SerializeField]
    private AudioClip se_select;
    [SerializeField]
    private AudioClip se_clear;
    [SerializeField]
    private AudioClip se_gameover;
    [SerializeField]
    private AudioClip se_get;
    [SerializeField]
    private AudioClip se_hit;
    [SerializeField]
    private AudioClip se_kokkurisan;
    [SerializeField]
    private AudioClip se_miss;



    //-----------------------------------------------------
    // Efect(原則としてEffectData.csのenumの定義名と同じにし小文字で命名すること)
    //-----------------------------------------------------
    //---ギミック
    //[SerializeField] private ParticleSystem ef_gimick_fire;


    void Awake() {
        Application.targetFrameRate = TargetFrame;
        //-----------------------------------------------------
        // BGM・SE
        //-----------------------------------------------------
        SoundData.BGMDataSet(bgm_title, (int)SoundData.eBGM.BGM_TITLE);
        SoundData.BGMDataSet(bgm_select, (int)SoundData.eBGM.BGM_SELECT);
        SoundData.BGMDataSet(bgm_game01, (int)SoundData.eBGM.BGM_GAME01);
        SoundData.BGMDataSet(bgm_game02, (int)SoundData.eBGM.BGM_GAME02);
        SoundData.BGMDataSet(bgm_game03, (int)SoundData.eBGM.BGM_GAME03);

        SoundData.SEDataSet(se_back, (int)SoundData.eSE.SE_BACK);
        SoundData.SEDataSet(se_select, (int)SoundData.eSE.SE_SELECT);
        SoundData.SEDataSet(se_decision, (int)SoundData.eSE.SE_DICISION);
        SoundData.SEDataSet(se_clear, (int)SoundData.eSE.SE_CLEAR);
        SoundData.SEDataSet(se_gameover, (int)SoundData.eSE.SE_GAMEOVER);
        SoundData.SEDataSet(se_get, (int)SoundData.eSE.SE_GET);
        SoundData.SEDataSet(se_hit, (int)SoundData.eSE.SE_HIT);
        SoundData.SEDataSet(se_kokkurisan, (int)SoundData.eSE.SE_KOKKURISAN);
        SoundData.SEDataSet(se_miss, (int)SoundData.eSE.SE_MISS);

        SoundData.SEVolume[(int)SoundData.eSE.SE_BACK] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_SELECT] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_DICISION] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_CLEAR] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_GAMEOVER] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_GET] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_HIT] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_KOKKURISAN] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_MISS] = 1.0f;

        //-----------------------------------------------------
        // Efect
        //-----------------------------------------------------
        //---ギミック関連
        //EffectData.EFDataSet(ef_gimick_fire, (int)EffectData.eEFFECT.EF_GIMICK_FIRE);

        SoundData.isSetSound = true;                 // デバッグ時サウンド初期化してない場合にエラーが出るからけす
        //EffectData.isSetEffect = true;                  // デバッグ時エフェクト初期化していない場合にエラーを出さない

        // 絶対に消されない音のいれる場所
        for (int i = 0; i < 10; i++)
        {
            SoundData.IndelibleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        // ゲームパッド初期化
        //if (Gamepad.current != null)
        //{
        //    GameData.gamepad = Gamepad.current;
        //}
    }
}