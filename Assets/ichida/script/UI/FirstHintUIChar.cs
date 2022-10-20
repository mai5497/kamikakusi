//=============================================================================
//
// �n�߂̃q���g�P������\������X�N���v�g
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/17 �쐬(�����_���ňꕶ���\��)
// 2022/10/20 �����_���ŕ\���ł͂Ȃ��A����ґ��Őݒ�ł���悤��
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHintUIChar : MonoBehaviour
{
    [Header("�ς��l��(true����)")]
    [SerializeField]
    private bool kituneORnormal = true;

    private Text hintCharText;      // �\���p�e�L�X�g�^�ϐ�
    private string firstHintKitune; // �ςƐl�̈ꕶ���q���g�̗��������˂Ă���̂ł��ꂼ��p�ӂ��Ă���
    private string firstHintNormal;


    // Start is called before the first frame update
    void Start() {
        hintCharText = GetComponent<Text>();

        firstHintKitune = CPData.kituneHint;
        firstHintNormal = CPData.normalHint;

        //----- �����̕������烉���_���ňꕶ���擾���� -----
        // ����������Ă��炸���������Ă��Ȃ������ꍇ�̓����_���ŕ\��
        int random;
        if (kituneORnormal) {
            if (firstHintKitune == "") {
                random = UnityEngine.Random.Range(0, CPData.kituneClearStr.Length);
                firstHintKitune = CPData.kituneClearStr.Substring(random, 1);
                CPData.kituneHint = firstHintKitune;
            }
        } else {
            if (firstHintNormal == "") {
                random = UnityEngine.Random.Range(0, CPData.normalClearStr.Length);
                firstHintNormal = CPData.normalClearStr.Substring(random, 1);
                CPData.normalHint = firstHintNormal;
            }
        }

        //----- �����̕\�� -----
        if (kituneORnormal) {
            hintCharText.text = firstHintKitune;
        } else {
            hintCharText.text = firstHintNormal;
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
