//=============================================================================
//
// �Q�[���̊Ǘ�
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/17 �쐬
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //----- �f�o�b�O�p�ϐ� -----
    [Header("�����������")]
    [SerializeField]
    private int paperCnt = 5;

    [Header("�����ł�������")]
    [SerializeField]
    private int lookCnt = 5;

    [Header("�ς̑��̐����̕���")]
    [SerializeField]
    private string _kituneClearStr;
    [Header("�ʏ펞�̐����̕���")]
    [SerializeField]
    private string _normalClearStr;



    // Start is called before the first frame update
    void Start()
    {
        //----- �T�E���h -----
        for (int i = 0; i < SoundData.GameAudioList.Length; ++i) {
            SoundData.GameAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager.Play(SoundData.eBGM.BGM_GAME01,SoundData.GameAudioList);

        //----- �f�o�b�O�p�ɒl�ύX -----
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
