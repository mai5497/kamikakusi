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
