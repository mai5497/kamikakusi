//=============================================================================
//
// サウンドマネージャー2テスト用
//
// 作成日:2022/10/19
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/19 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSound : MonoBehaviour
{
    [Header("曲数")]
    public int soundNum = 10;
    [Header("音量")]
    public float soundVolume = 1.0f;
    [Header("フェードアウト速度")]
    public float speedFadeOut;
    [Header("フェードイン速度")]
    public float speedFadeIn;
    [Header("BGM再生")]
    public bool isPlay;
    [Header("BGMフェードアウト再生")]
    public bool isPlayFadeOut;
    [Header("BGMフェードイン再生終了")]
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
        // 普通に再生
        if (isPlay)
        {
            SoundManager2.Play(SoundData.eBGM.BGM_TITLE, audioSourceArray);
            SoundManager2.bgmVolume = soundVolume;
            SoundManager2.setVolume(audioSourceArray);
            isPlay = false;
        }
        // フェードアウト再生
        if (isPlayFadeOut)
        {
            SoundManager2.fadeOutSpeed = speedFadeOut;
            SoundManager2.Play_FadeOut(SoundData.eBGM.BGM_TITLE, audioSourceArray);
            SoundManager2.bgmVolume = soundVolume;
            isPlayFadeOut = false;
        }
        // フェードイン再生終了
        if (isStopFadeIn)
        {
            SoundManager2.fadeInSpeed = speedFadeIn;
            SoundManager2.Stop_FadeIn(audioSourceArray);
            isStopFadeIn = false;
        }
        // フェード用更新
        SoundManager2.UpdateFade(audioSourceArray);
    }
}
