//=============================================================================
//
// ゲームの管理
//
// 作成日:2022/10/17
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/17 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //----- デバッグ用変数 -----
    [Header("紙見れる上限数")]
    [SerializeField]
    private int paperCnt = 5;

    [Header("注視できる上限数")]
    [SerializeField]
    private int lookCnt = 5;

    [Header("狐の窓の正解の文字")]
    [SerializeField]
    private string _kituneClearStr;
    [Header("通常時の正解の文字")]
    [SerializeField]
    private string _normalClearStr;



    // Start is called before the first frame update
    void Start()
    {
        //----- サウンド -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i) {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_GAME01,SoundData.GameAudioList);

        //----- デバッグ用に値変更 -----
        if(paperCnt != CPData.paperCnt) {
            CPData.paperCnt = paperCnt;
        }
        if (lookCnt != CPData.lookCnt) {
            CPData.lookCnt = lookCnt;
        }
        if (_kituneClearStr != CPData.kituneClearStr) {
            CPData.kituneClearStr = _kituneClearStr;
        }
        if(_normalClearStr != CPData.normalClearStr) {
            CPData.normalClearStr = _normalClearStr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
