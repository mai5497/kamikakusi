//=============================================================================
//
// シーンマネージャーフェード用
//
// 作成日:2022/10/20
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/20 作成
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManagerFade
{
    // サブシーン
    public enum SubScene {
        Title,
        StageSelect
    }
    // サブシーン読み込み
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
    // メインシーン読み込み
    public static void LoadSceneMain(int worldNo,int stageNo)
    {
        SceneManagerData.nowWorldNo = worldNo;
        SceneManagerData.nowStageNo = stageNo;
        ClearManager.SaveNowStage();
        Fade_in003.fade_in_use(SceneManagerData.mainSceneStrArray[worldNo, stageNo]);
    }
    // 次のステージシーンの読み込み
    public static void LoadSceneNextStage()
    {
        bool isNextWorld = false;
        // ステージ番号が最大は無ければ、
        if (SceneManagerData.nowStageNo < SceneManagerData.mainSceneStrArray.GetLength(1) - 1)
        {
            // 次のステージがnullで無ければ
            if (SceneManagerData.mainSceneStrArray[SceneManagerData.nowWorldNo, SceneManagerData.nowStageNo + 1] != null)
            {
                if (Fade_in003.GetFadeIn() == false)
                {
                    LoadSceneMain(SceneManagerData.nowWorldNo, SceneManagerData.nowStageNo + 1);
                }
            }
            else
            {
                isNextWorld = true;
            }
        }
        else
        {
            isNextWorld = true;
        }

        if (isNextWorld)
        {
            // ワールド番号が最大で無ければ次のワールドシーンへ
            if (SceneManagerData.nowWorldNo < SceneManagerData.mainSceneStrArray.GetLength(0) - 1)
            {
                if (Fade_in003.GetFadeIn() == false)
                {
                    LoadSceneMain(SceneManagerData.nowWorldNo + 1, 0);
                }
            }
        }
    }
}
