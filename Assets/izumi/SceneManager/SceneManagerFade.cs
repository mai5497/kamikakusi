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
        // ステージ番号が最大では無ければ、
        if (SceneManagerData.nowStageNo < SceneManagerData.mainSceneStrArray.GetLength(1) - 1)
        {
            if (SceneManagerData.mainSceneStrArray[SceneManagerData.nowWorldNo, SceneManagerData.nowStageNo + 1] == null)
            {
                
            }
        }
    }
}
