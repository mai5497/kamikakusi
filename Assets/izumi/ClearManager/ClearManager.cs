//=============================================================================
//
// �N���A�}�l�[�W���[
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

public static class ClearManager
{
    private static string clearStageName = "clearStage";
    private static string nowWorldName = "nowWorld";
    private static string nowStageName = "nowStage";
    private static string saveName = "save";

    // �N���A�����X�e�[�W��ۑ�
    public static void SaveClearStage()
    {
        PlayerPrefs.SetInt(clearStageName + SceneManagerData.nowWorldNo.ToString() + SceneManagerData.nowStageNo.ToString(), 1);
        PlayerPrefs.SetInt(saveName, 1);
        PlayerPrefs.Save();
    }
    // ���݂̃��[���h,�X�e�[�W��ۑ�
    public static void SaveNowStage()
    {
        PlayerPrefs.SetInt(nowWorldName, SceneManagerData.nowWorldNo);
        PlayerPrefs.SetInt(nowStageName, SceneManagerData.nowStageNo);
        PlayerPrefs.SetInt(saveName, 1);
        PlayerPrefs.Save();
    }
    // �����Ŏw�肵���X�e�[�W���N���A���Ă��邩���擾
    public static bool GetClearStage(int worldNo, int stageNo)
    {
        int clear = PlayerPrefs.GetInt(clearStageName + SceneManagerData.nowWorldNo.ToString() + SceneManagerData.nowStageNo.ToString(), 0);
        if (clear == 1){
            return true;
        }
        else
        {
            return false;
        }
    }
    // �����Ŏw�肵�����[���h���N���A���Ă��邩���擾
    public static bool GetClearWorld(int worldNo)
    {
        for (int j = 0; j < SceneManagerData.mainSceneStrArray.GetLength(1); j++)
        {
            if (SceneManagerData.mainSceneStrArray[worldNo, j] != null)
            {
                if (ClearManager.GetClearStage(worldNo, j) == false)
                {
                    return false;
                }
            }
        }
        return true;
    }
    // ���݂̃��[���h���擾
    public static int GetNowWorld()
    {
        return PlayerPrefs.GetInt(nowWorldName, 0);
    }
    // ���݂̃X�e�[�W���擾
    public static int GetNowStage()
    {
        return PlayerPrefs.GetInt(nowStageName, 0);
    }
    // �Z�[�u�̗L�����擾
    public static int GetSave()
    {
        return PlayerPrefs.GetInt(saveName, 0);
    }
    // �f�[�^�폜
    public static void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}