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
    public static string titleScene = "Title";
    [Header("ステージセレクトシーン名")]
    public static string stageSelectScene = "StageSelect";
    [Header("全クリアシーン名")]
    public static string allClearScene = "All_Clear";
    [Header("メインシーン名リスト")]  // string[ワールド番号,ステージ番号]
    public static string[,] mainSceneStrArray =
    {
        { "Tutorial_01","Tutorial_02","Tutorial_03" },
        { "Stage1_01","Stage1_02","Stage1_03" },
        { "Stage2_01","Stage2_02","Stage2_03" },
        { "Stage3_01","Stage3_02","Stage3_03" },
    };
    [Header("フェードアウト速度")]
    public static float speedFadeOut = 0.5f;
    [Header("フェードイン速度")]
    public static float speedFadeIn = 1.0f;

    // 現在のワールド番号
    public static int nowWorldNo = 0;
    // 現在のステージ番号
    public static int nowStageNo = 0;
}
