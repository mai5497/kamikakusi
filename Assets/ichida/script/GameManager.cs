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

    [Header("通常時の正解の文字")]
    [SerializeField]
    private string _normalClearStr;
    [Header("狐の窓の正解の文字")]
    [SerializeField]
    private string _kituneClearStr;
    [Header("狐一文字ヒント")]
    [SerializeField]
    private string _foxHint;
    [Header("人一文字ヒント")]
    [SerializeField]
    private string _humanHint;


    //[SerializeField]
    //private GameObject sceneManager;        // シーンマネージャー自動生成用
    //private GameObject sceneManagerEntity;  // シーンマネージャーの実体

    // Start is called before the first frame update
    void Start()
    {
        //----- サウンド -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i) {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        if (SceneManagerData.nowWorldNo == 0) {
            SoundManager2.Play(SoundData.eBGM.BGM_GAME01, SoundData.GameAudioList);
        } else if (SceneManagerData.nowWorldNo == 1) {
            SoundManager2.Play(SoundData.eBGM.BGM_GAME01, SoundData.GameAudioList);
        } else if (SceneManagerData.nowWorldNo == 2) {
            SoundManager2.Play(SoundData.eBGM.BGM_GAME02, SoundData.GameAudioList);
        } else if (SceneManagerData.nowWorldNo == 3) {
            SoundManager2.Play(SoundData.eBGM.BGM_GAME03, SoundData.GameAudioList);
        }
        Debug.Log(SceneManagerData.nowWorldNo);

        //----- デバッグ用に値変更 -----
        // 紙開ける回数
        if (paperCnt != CPData.paperCnt) {
            CPData.paperCnt = paperCnt;
        }
        // 注視回数
        if (lookCnt != CPData.lookCnt) {
            CPData.lookCnt = lookCnt;
        }
        // クリア文字
        if (_kituneClearStr != CPData.kituneClearStr) {
            CPData.kituneClearStr = _kituneClearStr;
        }
        if(_normalClearStr != CPData.normalClearStr) {
            CPData.normalClearStr = _normalClearStr;
        }
        // 一文字ひんと
        if(_foxHint != CPData.kituneHint) {
            CPData.kituneHint = _foxHint;
        }
        if(_humanHint != CPData.normalHint) {
            CPData.normalHint = _humanHint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
