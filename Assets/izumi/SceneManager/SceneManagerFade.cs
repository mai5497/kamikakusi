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
    // �T�u�V�[��
    public enum SubScene {
        Title,
        StageSelect
    }
    // �T�u�V�[���ǂݍ���
    public static void LoadSceneSub(SubScene scene)
    {
        switch (scene) {
            case SubScene.Title:
                Fade_in003.fade_in_use(SceneManagerData.titleScene);
                break;
            case SubScene.StageSelect:
                Fade_in003.fade_in_use(SceneManagerData.stageSelectScene);
                break;
        }
    }
    // ���C���V�[���ǂݍ���
    public static void LoadSceneMain(int worldNo,int stageNo)
    {
        SceneManagerData.nowWorldNo = worldNo;
        SceneManagerData.nowStageNo = stageNo;
        ClearManager.SaveNowStage();
        Fade_in003.fade_in_use(SceneManagerData.mainSceneStrArray[worldNo, stageNo]);
    }
    // ���̃X�e�[�W�V�[���̓ǂݍ���
    public static void LoadSceneNextStage()
    {
        // �X�e�[�W�ԍ����ő�ł͖�����΁A
        if (SceneManagerData.nowStageNo < SceneManagerData.mainSceneStrArray.GetLength(1) - 1)
        {
            if (SceneManagerData.mainSceneStrArray[SceneManagerData.nowWorldNo, SceneManagerData.nowStageNo + 1] == null)
            {
                
            }
        }
    }
}
