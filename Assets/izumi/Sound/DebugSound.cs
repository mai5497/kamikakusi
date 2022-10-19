//=============================================================================
//
// �T�E���h�}�l�[�W���[2�e�X�g�p
//
// �쐬��:2022/10/19
// �쐬��:��D��
//
// <�J������>
// 2022/10/19 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSound : MonoBehaviour
{
    [Header("�Ȑ�")]
    public int soundNum = 10;
    [Header("����")]
    public float soundVolume = 1.0f;
    [Header("�t�F�[�h�A�E�g���x")]
    public float speedFadeOut;
    [Header("�t�F�[�h�C�����x")]
    public float speedFadeIn;
    [Header("BGM�Đ�")]
    public bool isPlay;
    [Header("BGM�t�F�[�h�A�E�g�Đ�")]
    public bool isPlayFadeOut;
    [Header("BGM�t�F�[�h�C���Đ��I��")]
    public bool isStopFadeIn;
    private AudioSource[] audioSourceArray;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceArray = new AudioSource[soundNum];
        for (int i = 0; i < soundNum; i++) {
            audioSourceArray[i] = this.gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���ʂɍĐ�
        if (isPlay)
        {
            SoundManager2.Play(SoundData.eBGM.BGM_TITLE, audioSourceArray);
            SoundManager2.bgmVolume = soundVolume;
            SoundManager2.setVolume(audioSourceArray);
            isPlay = false;
        }
        // �t�F�[�h�A�E�g�Đ�
        if (isPlayFadeOut)
        {
            SoundManager2.fadeOutSpeed = speedFadeOut;
            SoundManager2.Play_FadeOut(SoundData.eBGM.BGM_TITLE, audioSourceArray);
            SoundManager2.bgmVolume = soundVolume;
            isPlayFadeOut = false;
        }
        // �t�F�[�h�C���Đ��I��
        if (isStopFadeIn)
        {
            SoundManager2.fadeInSpeed = speedFadeIn;
            SoundManager2.Stop_FadeIn(audioSourceArray);
            isStopFadeIn = false;
        }
        // �t�F�[�h�p�X�V
        SoundManager2.UpdateFade(audioSourceArray);
    }
}
