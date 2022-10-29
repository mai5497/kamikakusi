//=============================================================================
//
// ポストプロセス生成用
//
// 作成日:2022/10/29
// 作成者:泉優樹
//
// <開発履歴>
// 2022/10/29 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessGenerator : MonoBehaviour
{
    [Header("ワールド2のポストプロセス")]
    public GameObject postprocessStage2;
    [Header("ワールド3のポストプロセス")]
    public GameObject postprocessStage3;

    private static bool isPostprocessStage2 = false;
    private static bool isPostprocessStage3 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // フェードイン中に、フラグが立てばポストプロセス生成
        if (Fade_out003.GetFading())
        {
            if (isPostprocessStage2)
            {
                Instantiate(postprocessStage2);
                isPostprocessStage2 = false;
            }
            if (isPostprocessStage3)
            {
                Instantiate(postprocessStage3);
                isPostprocessStage3 = false;
            }
        }
    }

    // ポストプロセスのセット
    public static void SetPostProcess(int worldNo)
    {
        switch (worldNo) {
            case 2:
                isPostprocessStage2 = true;
                break;
            case 3:
                isPostprocessStage3 = true;
                break;
        }
    }
}
