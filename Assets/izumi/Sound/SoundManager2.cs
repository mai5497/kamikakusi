//=============================================================================
//
// �쐬��:2022/03/16
// �쐬��:�ɒn�c�^��
// �ҏW�ҁF��D��
//
// <�J������>
// 2022/03/16 �쐬
// 2022/03/26 SE�̉��ʂ�30���ɌŒ�
// 2022/03/27 ���̃t�F�[�h��ǉ����ĂȂ�
// 2022/04/01 ���|�[�Y
//
// 2022/10/19 BGM�t�F�[�h
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager2
{

    //BGM�{�����[��
    public static float bgmVolume = 1.0f;
    //SE�{�����[��
    public static float seVolume = 1.0f;
    /// <summary>
    /// ���g�p��AudioSource��T��
    /// </summary>
    /// <param name="audioSourceList">���̃V�[���Ŏg�p���Ă���I�[�f�B�I���X�g</param>
    /// <returns>���g�p�̃I�[�f�B�I���X�g�������ĕԂ�</returns>
    private static AudioSource GetUnusedSource(AudioSource[] audioSourceList) { 
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }
        }
        return null;    // ���g�p��AudioSource�݂͂���Ȃ�����
    }

    //�g�p���Ă���BGM��AudioSource�擾
    public static AudioSource GetUsedSource(AudioSource[] audioSourceList)
    {
        for (int i = 0; i < audioSourceList.Length; ++i)
        {
            //�Đ������ǂ���
            if (audioSourceList[i].isPlaying == true)
            {
                for(int j = 0; j < SoundData.BGMClip.Length; ++j)
                {
                    //BGM���ǂ���
                    if (audioSourceList[i].clip == SoundData.BGMClip[j])
                    {
                        return audioSourceList[i];
                    }
                }
            }
        }
        return null;    // ���g�p��AudioSource�݂͂���Ȃ�����
    }


    /// <summary>
    /// �T�E���h�Đ�
    /// </summary>
    /// <remarks>SE��p</remarks>
    /// <param name="_seDataNumber">��������SoundData�̗񋓑̂ɒ�`�������</param>
    /// <param name="_audioSourceList">�Q�[���I�[�f�B�I���X�g���^�C�g���I�[�f�B�I���X�g��n��</param>
    public static void Play(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = seVolume * SoundData.SEVolume[(int)_seDataNumber];
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        //float endTime = audioSource.clip.length * Time.timeScale;

    }

    /// <summary>
    /// �T�E���h�Đ�
    /// </summary>
    /// <remarks>BGM��p</remarks>
    /// <param name="_seDataNumber">��������SoundData�̗񋓑̂ɒ�`�������</param>
    /// <param name="_audioSourceList">�Q�[���I�[�f�B�I���X�g���^�C�g���I�[�f�B�I���X�g��n��</param>
    public static void Play(SoundData.eBGM _bgmDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.clip = SoundData.BGMClip[(int)_bgmDataNumber];
        audioSource.volume = bgmVolume;
        audioSource.loop = true;
        audioSource.Play();
    }

    //BGM�{�����[���ݒ�
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
    /// �T�E���h�Đ�
    /// </summary>
    /// <remarks>�����Ȃ�����p�̂͂�������</remarks>
    /// <param name="_seDataNumber">��������SoundData�̗񋓑̂ɒ�`�������</param>
    /// <param name="_audioSourceList">�Q�[���I�[�f�B�I���X�g���^�C�g���I�[�f�B�I���X�g��n��</param>
    public static void IgnorePlay(SoundData.eSE _seDataNumber, AudioSource[] _audioSourceList) {
        AudioSource audioSource = GetUnusedSource(_audioSourceList);
        if (audioSource == null)
        {
            Debug.Log("�Đ��o���܂���ł���");
            return; // �Đ��ł��܂���ł���
        }
        audioSource.ignoreListenerPause = true;
        audioSource.clip = SoundData.SEClip[(int)_seDataNumber];
        audioSource.volume = seVolume;
        //audioSource.Play();
        audioSource.PlayOneShot(audioSource.clip);
        float endTime = audioSource.clip.length * Time.timeScale;

    }


    /// <summary>
    /// �T�E���h�|�[�Y
    /// </summary>
    /// <param name="_audioSourceList">���̃V�[���Ŏg�p���Ă���I�[�f�B�I���X�g</param>
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
    /// �T�E���h�̃|�[�Y����
    /// </summary>
    /// <param name="_audioSourceList">���̃V�[���Ŏg�p���Ă���I�[�f�B�I���X�g</param>
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
    //  �T�E���h�t�F�[�h
    //  �쐬�F�ɒn�c�^��
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
    /// �T�E���h�̃t�F�[�h�C��
    /// </summary>
    /// <param name="_audioSourceList">���̃V�[���Ŏg�p���Ă���I�[�f�B�I���X�g</param>
    public static void SoundFadeIn()
    {

    }
}
