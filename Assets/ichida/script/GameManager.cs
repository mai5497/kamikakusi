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

    [Header("�ʏ펞�̐����̕���")]
    [SerializeField]
    private string _normalClearStr;
    [Header("�ς̑��̐����̕���")]
    [SerializeField]
    private string _kituneClearStr;
    [Header("�ψꕶ���q���g")]
    [SerializeField]
    private string _foxHint;
    [Header("�l�ꕶ���q���g")]
    [SerializeField]
    private string _humanHint;


    //[SerializeField]
    //private GameObject sceneManager;        // �V�[���}�l�[�W���[���������p
    //private GameObject sceneManagerEntity;  // �V�[���}�l�[�W���[�̎���

    // Start is called before the first frame update
    void Start()
    {
        //----- �T�E���h -----
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

        //----- �f�o�b�O�p�ɒl�ύX -----
        // ���J�����
        if (paperCnt != CPData.paperCnt) {
            CPData.paperCnt = paperCnt;
        }
        // ������
        if (lookCnt != CPData.lookCnt) {
            CPData.lookCnt = lookCnt;
        }
        // �N���A����
        if (_kituneClearStr != CPData.kituneClearStr) {
            CPData.kituneClearStr = _kituneClearStr;
        }
        if(_normalClearStr != CPData.normalClearStr) {
            CPData.normalClearStr = _normalClearStr;
        }
        // �ꕶ���Ђ��
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
