//=============================================================================
//
// シーンマネージャーデータ
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

public class SceneManagerData
{
    [Header("タイトルシーン名")]
    public static string titleScene = "title";
    [Header("ステージセレクトシーン名")]
    public static string stageSelectScene = "StageSelect";
    [Header("メインシーン名リスト")]
    //public List<string> mainSceneStrList;
    public static string[,] mainSceneStrArray =
    {
        { "Tutorial","Tutorial","Tutorial" },
        { "Stage1_01","Stage1_02","Stage1_03" },
        { "Stage2_01","Stage2_02","Stage2_03" },
        { "Stage3_01","Stage3_02","Stage3_03" },
    };

}
