//=============================================================================
//
// サウンドマネージャー2
//
// 作成日:2022/03/16
// 作成者:伊地田真衣
// 編集者：泉優樹
//
// <開発履歴>
// 2022/03/16 作成
// 2022/03/26 SEの音量を30％に固定
// 2022/03/27 音のフェードを追加してない
// 2022/04/01 音ポーズ
//
// 2022/10/19 BGMフェード
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager2
{

    //BGMボリューム
    public static float bgmVolume = 1.0f;
    //SEボリューム
    public static float seVolume = 1.0f;
    //フェードアウト
    private static bool isFadeOut;
    private static float fadeOutValue = 0.0f;
    public static float fadeOutSpeed = 0.1f;
    //フェードイン
    private static bool isFadeIn;
    private static float fadeInValue = 0.0f;
    public static float fadeInSpeed = 0.1f;

    /// <summary>
    /// 未使用のAudioSourceを探す
    /// </summary>
    /// <param name="audioSourceList">そのシーンで使用しているオーディオリスト</param>
    /// <returns>未使用のオーディオリストを見つけて返す</returns>
    private static AudioSource GetUnusedSource(AudioSource[] audioSourceList) { 
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }
        return null;    // 未使用のAudioSourceはみつからなかった
    }

    //使用しているBGMのAudioSource取得
    public static AudioSource GetUsedSource(AudioSource[] audioSourceList)
    {
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            //再生中かどうか
            if (audioSourceList[i].isPlaying == true)
            {
                for(int j = 0; j < SoundData.BGMClip.Length; ++j)
                {
                    //BGMかどうか
                    if (audioSourceList[i].clip == SoundData.BGMClip[j])
                    {
                        return audioSourceList[i];
                    }
                }
            }
        }
        return null;    // 未使用のAudioSourceはみつからなかった
    }


    /// <summary>
    /// サウンド再生
    /// </summary>
    /// <remarks>SE専用</remarks>
    /// <param name="_seDataNumber">第一引数はSoundDataの列挙体に定義したやつ</param>
    /// <param name="_audioSourceList">ゲームオーディオリストかタイトルオーディオリストを渡す</param>
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("再生出来ませんでした");
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = seVolume * SoundData.SEVolume[(int)_seDataNumber];
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        //float endTime = audioSource.clip.length * Time.timeScale;

    }

    /// <summary>
    /// サウンド再生
    /// </summary>
    /// <remarks>BGM専用</remarks>
    /// <param name="_seDataNumber">第一引数はSoundDataの列挙体に定義したやつ</param>
    /// <param name="_audioSourceList">ゲームオーディオリストかタイトルオーディオリストを渡す</param>
    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("再生出来ませんでした");
            return; // 再生できませんでした
        }
        audioSource.clip = SoundData.BGMClip[(int)_bgmDataNumber];
        audioSource.volume = bgmVolume;
        audioSource.loop = true;
        audioSource.Play();
    }

    //BGMボリューム設定
    public static void setVolume(AudioSource[] _audioSourceList)
    {
        AudioSource audioSource = GetUsedSource(_audioSourceList);
        if (audioSource == null)
        {
            return;
        }
        audioSource.volume = bgmVolume;
    }

    /// <summary>
    /// サウンド再生
    /// </summary>
    /// <remarks>消えない音専用のはずだった</remarks>
    /// <param name="_seDataNumber">第一引数はSoundDataの列挙体に定義したやつ</param>
    /// <param name="_audioSourceList">ゲームオーディオリストかタイトルオーディオリストを渡す</param>
    public static void IgnorePlay(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("再生出来ませんでした");
            return; // 再生できませんでした
        }
        audioSource.ignoreListenerPause = true;
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = seVolume;
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        float endTime = audioSource.clip.length * Time.timeScale;

    }


    /// <summary>
    /// サウンドポーズ
    /// </summary>
    /// <param name="_audioSourceList">そのシーンで使用しているオーディオリスト</param>
    public static void SoundPause(AudioSource[] _audioSourceList) {
        for (int i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false)
            {
                break;
            }
            _audioSourceList[i].Pause();
        }
    }

    /// <summary>
    /// サウンドのポーズ解除
    /// </summary>
    /// <param name="_audioSourceList">そのシーンで使用しているオーディオリスト</param>
    public static void SoundUnPause(AudioSource[] _audioSourceList) {
        for (int i = 0; i < _audioSourceList.Length; ++i)
        {
            if (_audioSourceList[i].isPlaying == false && _audioSourceList[i].clip)
            {
                _audioSourceList[i].UnPause();
            }
        }
    }



    //----------------------------------
    //
    //  サウンドフェード
    //  作成：伊地田真衣
    //
    //----------------------------------
    //public static IEnumerator VolumeDown(AudioSource[] _audioSources) {
    //    while (_audioSources.volume > 0)
    //    {
    //        _audioSources.volume -= 0.01f;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    /// <summary>
    /// サウンドのフェードアウト再生
    /// </summary>
    /// <param name="_audioSourceList">そのシーンで使用しているオーディオリスト</param>
    public static void Play_FadeOut(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList)
    {
        isFadeOut = true;
        Play(_bgmDataNumber, _audioSourceList);
    }

    /// <summary>
    /// サウンドのフェードイン再生終了
    /// </summary>
    /// <param name="_audioSourceList">そのシーンで使用しているオーディオリスト</param>
    public static void Stop_FadeIn(AudioSource[] _audioSourceList)
    {
        isFadeIn = true;
    }

    /// <summary>
    /// サウンドのフェード更新
    /// </summary>
    /// <param name="_audioSourceList">そのシーンで使用しているオーディオリスト</param>
    public static void UpdateFade(AudioSource[] _audioSourceList)
    {
        // フェードアウト
        if (isFadeOut)
        {
            for (int i = 0; i < _audioSourceList.Length; ++i)
            {
                fadeOutValue += Time.deltaTime * fadeOutSpeed;
                _audioSourceList[i].volume = Mathf.Lerp(0, bgmVolume, fadeOutValue);
            }
            // フェードアウト終了
            if (fadeOutValue >= 1.0f)
            {
                isFadeOut = false;
                fadeOutValue = 0.0f;
            }
        }
        //フェードイン
        if (isFadeIn)
        {
            for (int i = 0; i < _audioSourceList.Length; ++i)
            {
                fadeInValue += Time.deltaTime * fadeInSpeed;
                _audioSourceList[i].volume = Mathf.Lerp(bgmVolume, 0, fadeInValue);
            }
            //フェードイン終了
            if (fadeInValue >= 1.0f)
            {
                for (int i = 0; i < _audioSourceList.Length; ++i)
                {
                    isFadeIn = false;
                    fadeInValue = 0.0f;
                    _audioSourceList[i].Stop();
                    _audioSourceList[i].clip = null;
                }
            }
        }
    }
}
