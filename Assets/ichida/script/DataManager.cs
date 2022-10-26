//=============================================================================
//
// �f�[�^�}�l�[�W���[
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
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    /*
     * �Z�[�u�f�[�^�̊Ǘ��E���̃f�[�^�̊Ǘ��ȂǑf�ނ̃f�[�^�̊Ǘ�������(�\��)
     * �^�C�g���ŌĂяo��������B���������B
     * 
     * 
     * 
     */

    static int TargetFrame = 60;

    //-----------------------------------------------------
    // BGM�ESE
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
    // Efect(�����Ƃ���EffectData.cs��enum�̒�`���Ɠ����ɂ��������Ŗ������邱��)
    //-----------------------------------------------------
    //---�M�~�b�N
    //[SerializeField] private ParticleSystem ef_gimick_fire;


    void Awake() {
        Application.targetFrameRate = TargetFrame;
        //-----------------------------------------------------
        // BGM�ESE
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
        //---�M�~�b�N�֘A
        //EffectData.EFDataSet(ef_gimick_fire, (int)EffectData.eEFFECT.EF_GIMICK_FIRE);

        SoundData.isSetSound = true;                 // �f�o�b�O���T�E���h���������ĂȂ��ꍇ�ɃG���[���o�邩�炯��
        //EffectData.isSetEffect = true;                  // �f�o�b�O���G�t�F�N�g���������Ă��Ȃ��ꍇ�ɃG���[���o���Ȃ�

        // ��΂ɏ�����Ȃ����̂����ꏊ
        for (int i = 0; i < 10; i++)
        {
            SoundData.IndelibleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }

        // �Q�[���p�b�h������
        //if (Gamepad.current != null)
        //{
        //    GameData.gamepad = Gamepad.current;
        //}
    }
}