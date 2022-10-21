//=============================================================================
//
// クリアマネージャー
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

public static class ClearManager
{
    private static string clearStageName = "clearStage";
    private static string nowWorldName = "nowWorld";
    private static string nowStageName = "nowStage";
    private static string saveName = "save";

    // クリアしたステージを保存
    public static void SaveClearStage()
    {
        PlayerPrefs.SetInt(clearStageName + SceneManagerData.nowWorldNo.ToString() + SceneManagerData.nowStageNo.ToString(), 1);
        PlayerPrefs.SetInt(saveName, 1);
        PlayerPrefs.Save();
    }
    // 現在のワールド,ステージを保存
    public static void SaveNowStage()
    {
        PlayerPrefs.SetInt(nowWorldName, SceneManagerData.nowWorldNo);
        PlayerPrefs.SetInt(nowStageName, SceneManagerData.nowStageNo);
        PlayerPrefs.SetInt(saveName, 1);
        PlayerPrefs.Save();
    }
    // 引数で指定したステージがクリアしているかを取得
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
    // 引数で指定したワールドがクリアしているかを取得
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
    // 現在のワールドを取得
    public static int GetNowWorld()
    {
        return PlayerPrefs.GetInt(nowWorldName, 0);
    }
    // 現在のステージを取得
    public static int GetNowStage()
    {
        return PlayerPrefs.GetInt(nowStageName, 0);
    }
    // セーブの有無を取得
    public static int GetSave()
    {
        return PlayerPrefs.GetInt(saveName, 0);
    }
    // データ削除
    public static void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}
