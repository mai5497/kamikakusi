using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPause = false;

    ObservedValue<bool> shouldPause;

    private void Start() {
        shouldPause = new ObservedValue<bool>(isPause);
        shouldPause.OnValueChange += () => { if (isPause /*|| GameData.isFadeIn || GameData.isFadeOut*/) { PauseStart(); } else { PauseFin(); } };
    }

    private void Update() {
        shouldPause.Value = isPause;
    }

    /// <summary>
    /// �|�[�Y�X�^�[�g
    /// </summary>
    public static void PauseStart() {
        //if (EffectData.isSetEffect && !GameOver.GameOverFlag) {
        //    EffectManager.EffectPause();
        //} else if (EffectData.isSetEffect && GameOver.GameOverFlag) {
        //    EffectManager.EffectSlowStart();
        //}

        //if (SoundData.isSetSound)  // �T�E���h���g�p�̃V�[���Ȃ�ȉ��̏������X�L�b�v
        //{
        //    if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE) {
        //        SoundManager.SoundPause(SoundData.TitleAudioList);
        //    } else {
        //        SoundManager.SoundPause(SoundData.GameAudioList);
        //    }
        //}
    }

    /// <summary>
    /// �|�[�Y�I���
    /// </summary>
    public static void PauseFin() {
        //if (EffectData.isSetEffect && !GameOver.GameOverFlag) {
        //    EffectManager.EffectUnPause();
        //} else if (EffectData.isSetEffect && GameOver.GameOverFlag) {
        //    EffectManager.EffectSlowFin();
        //}


        //if (!SoundData.isSetSound)  // �T�E���h���g�p�̃V�[���Ȃ�ȉ��̏������X�L�b�v
        //{
        //    return;
        //}
        //if (GameData.CurrentMapNumber == (int)GameData.eSceneState.TITLE_SCENE) {
        //    SoundManager.SoundUnPause(SoundData.TitleAudioList);
        //} else {
        //    SoundManager.SoundUnPause(SoundData.GameAudioList);
        //}
    }
}
