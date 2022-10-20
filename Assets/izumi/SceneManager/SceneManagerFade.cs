//=============================================================================
//
// �V�[���}�l�[�W���[�t�F�[�h�p
//
// �쐬��:2022/10/20
// �쐬��:��D��
//
// <�J������>
// 2022/10/20 �쐬
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManagerFade
{
    public enum Scene {
        Title,
        StageSelect,
        Main
    }

    public static void LoadScene(Scene scene)
    {
        switch (scene) {
            case Scene.Title:
                Fade_in003.fade_in_use(SceneManagerData.titleScene);
                break;
        }
    }
}
