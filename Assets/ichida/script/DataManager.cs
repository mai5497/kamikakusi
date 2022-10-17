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


    //-----------------------------------------------------
    // Efect(原則としてEffectData.csのenumの定義名と同じにし小文字で命名すること)
    //-----------------------------------------------------
    //---ギミック
    //[SerializeField] private ParticleSystem ef_gimick_fire;
    //[SerializeField] private ParticleSystem ef_gimick_healitem;
    //[SerializeField] private ParticleSystem ef_gimick_magiccircle_red;
    //[SerializeField] private ParticleSystem ef_gimick_magiccircle_blue;
    //[SerializeField] private ParticleSystem ef_gimick_guide_left;
    //[SerializeField] private ParticleSystem ef_gimick_guide_left_up;
    //[SerializeField] private ParticleSystem ef_gimick_guide_left_down;
    //[SerializeField] private ParticleSystem ef_gimick_guide_right;
    //[SerializeField] private ParticleSystem ef_gimick_guide_right_up;
    //[SerializeField] private ParticleSystem ef_gimick_guide_right_down;

    ////---プレイヤー
    //[SerializeField] private ParticleSystem ef_player_shield;
    //[SerializeField] private ParticleSystem ef_player_break;
    //[SerializeField] private ParticleSystem ef_player_hit;
    //[SerializeField] private ParticleSystem ef_player_heal;
    //[SerializeField] private ParticleSystem ef_player_damage;
    //[SerializeField] private ParticleSystem ef_player_death;

    ////---雑魚的
    //[SerializeField] private ParticleSystem ef_enemy_darkarea;
    //[SerializeField] private ParticleSystem ef_enemy_death;
    //[SerializeField] private ParticleSystem ef_enemy_tomatobomb;

    ////---ボス
    //[SerializeField] private ParticleSystem ef_boss_death;
    //[SerializeField] private ParticleSystem ef_boss_knifedamage;
    //[SerializeField] private ParticleSystem ef_boss_strawberry;
    //[SerializeField] private ParticleSystem ef_boss_strawberryaim;
    //[SerializeField] private ParticleSystem ef_boss_fork;
    //[SerializeField] private ParticleSystem ef_boss_rainzone;
    //[SerializeField] private ParticleSystem ef_boss_strawberry_land;
    //[SerializeField] private ParticleSystem ef_boss_fork_dust;

    ////ラスボス
    //[SerializeField] private ParticleSystem ef_lastboss_energyball;
    //[SerializeField] private ParticleSystem ef_lastboss_fastenergyball;
    //[SerializeField] private ParticleSystem ef_lastboss_charge;
    //[SerializeField] private ParticleSystem ef_lastboss_warp;


    //---その他(今のところ未使用)
    //[SerializeField] private ParticleSystem ef_shield2;

    // Start is called before the first frame update
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

        SoundData.SEVolume[(int)SoundData.eSE.SE_BACK] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_SELECT] = 1.0f;
        SoundData.SEVolume[(int)SoundData.eSE.SE_DICISION] = 1.0f;

        //-----------------------------------------------------
        // Efect
        //-----------------------------------------------------
        //---ギミック関連
        //EffectData.EFDataSet(ef_gimick_fire, (int)EffectData.eEFFECT.EF_GIMICK_FIRE);
        //EffectData.EFDataSet(ef_gimick_healitem, (int)EffectData.eEFFECT.EF_GIMICK_HEALITEM);
        //EffectData.EFDataSet(ef_gimick_magiccircle_red, (int)EffectData.eEFFECT.EF_GIMICK_MAGICCIRCLE_RED);
        //EffectData.EFDataSet(ef_gimick_magiccircle_blue, (int)EffectData.eEFFECT.EF_GIMICK_MAGICCIRCLE_BLUE);
        //EffectData.EFDataSet(ef_gimick_guide_left, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT);
        //EffectData.EFDataSet(ef_gimick_guide_left_up, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_UP);
        //EffectData.EFDataSet(ef_gimick_guide_left_down, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_LEFT_DOWN);
        //EffectData.EFDataSet(ef_gimick_guide_right, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT);
        //EffectData.EFDataSet(ef_gimick_guide_right_up, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_UP);
        //EffectData.EFDataSet(ef_gimick_guide_right_down, (int)EffectData.eEFFECT.EF_GIMICK_GUIDE_RIGHT_DOWN);

        ////---プレイヤー関連
        //EffectData.EFDataSet(ef_player_shield, (int)EffectData.eEFFECT.EF_PLAYER_SHIELD);
        //EffectData.EFDataSet(ef_player_break, (int)EffectData.eEFFECT.EF_PLAYER_BREAK);
        //EffectData.EFDataSet(ef_player_hit, (int)EffectData.eEFFECT.EF_PLAYER_HIT);
        //EffectData.EFDataSet(ef_player_heal, (int)EffectData.eEFFECT.EF_PLAYER_HEAL);
        //EffectData.EFDataSet(ef_player_damage, (int)EffectData.eEFFECT.EF_PLAYER_DAMAGE);
        //EffectData.EFDataSet(ef_player_death, (int)EffectData.eEFFECT.EF_PLAYER_DEATH);

        ////---雑魚的関連
        //EffectData.EFDataSet(ef_enemy_darkarea, (int)EffectData.eEFFECT.EF_ENEMY_DARKAREA);
        //EffectData.EFDataSet(ef_enemy_death, (int)EffectData.eEFFECT.EF_ENEMY_DEATH);
        //EffectData.EFDataSet(ef_enemy_tomatobomb, (int)EffectData.eEFFECT.EF_ENEMY_TOMATOBOMB);

        ////---ボス関連
        //EffectData.EFDataSet(ef_boss_death, (int)EffectData.eEFFECT.EF_BOSS_DEATH);
        //EffectData.EFDataSet(ef_boss_knifedamage, (int)EffectData.eEFFECT.EF_BOSS_KNIFEDAMAGE);
        //EffectData.EFDataSet(ef_boss_strawberry, (int)EffectData.eEFFECT.EF_BOSS_STRAWBERRY);
        //EffectData.EFDataSet(ef_boss_strawberryaim, (int)EffectData.eEFFECT.EF_BOSS_STRAWBERRYAIM);
        //EffectData.EFDataSet(ef_boss_fork, (int)EffectData.eEFFECT.EF_BOSS_FORK);
        //EffectData.EFDataSet(ef_boss_rainzone, (int)EffectData.eEFFECT.EF_BOSS_RAINZONE);
        //EffectData.EFDataSet(ef_boss_strawberry_land, (int)EffectData.eEFFECT.EF_BOSS_STRAWBERRY_LAND);
        //EffectData.EFDataSet(ef_boss_fork_dust, (int)EffectData.eEFFECT.EF_BOOS_FORK_DUST);

        ////---ラスボス関連
        //EffectData.EFDataSet(ef_lastboss_energyball, (int)EffectData.eEFFECT.EF_LASTBOSS_ENERGYBALL);
        //EffectData.EFDataSet(ef_lastboss_fastenergyball, (int)EffectData.eEFFECT.EF_LASTBOSS_FASTENERGYBALL);
        //EffectData.EFDataSet(ef_lastboss_charge, (int)EffectData.eEFFECT.EF_LASTBOSS_CHARGE);
        //EffectData.EFDataSet(ef_lastboss_warp, (int)EffectData.eEFFECT.EF_LASTBOSS_WARP);

        //EffectData.EFDataSet(ef_shield2,(int)EffectData.eEFFECT.EF_SHEILD2);

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