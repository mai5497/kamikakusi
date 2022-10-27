using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxByakko : MonoBehaviour {
    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float alpha;    // �ς̃A���t�@�l

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    private bool isByakko_delete = false;   // ���ς���������true
    private bool isDeleting;    // �����Ă�Œ��̃t���O

    private Kokkurisan _Kokkurisan;  // �������肳��̃X�N���v�g�擾
    [System.NonSerialized]
    public bool isClear;    // �Q�[���N���A�̃t���O

    private bool oldIsLook;

    private ZoomLens _ZoomLens; // �Y�[���̕�Ԓl�̎擾�p

    private bool isSave = false;

    // �ς����������ɃG�t�F�N�g���o��
    public List<ParticleSystem> efFindList;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        isDeleting = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);

        _Kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
        isClear = false;

        oldIsLook = CPData.isLook;

        // �V�[���J�n�����݂̃X�e�[�W��ۑ�
        ClearManager.SaveNowStage();

        if (GameObject.Find("CanvasLens")) {
            _ZoomLens = GameObject.Find("CanvasLens").GetComponent<ZoomLens>();
        }
    }

    // Update is called once per frame
    void Update() {
        this.gameObject.layer = 0;

        //----- �������� -----
        //if (!isWindowColl && !oldIsLook && CPData.isLook) {    // �����������Ă��Ȃ��Ƃ��ɒ������ꂽ�Ƃ�
        //    if (_ZoomLens.valueZoomLerp > 0.9) {
        //        CPData.lookCnt--;
        //    }
        //}
        if (!isWindowColl && oldIsLook && !CPData.isLook) {    // �����������Ă��Ȃ��Ƃ��ɒ������ꂽ�Ƃ�
            if (_ZoomLens.valueZoomLerp > 0.9) {
                CPData.lookCnt--;
                SoundManager2.Play(SoundData.eSE.SE_MISS, SoundData.GameAudioList);
            }
        }

        if (_Kokkurisan.isClear && isWindowColl && CPData.isLook && !isDeleting) {
            // ������I
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;

            // ������̎��ɒ������I��������̓Y�[�������������Ȃ�
            if (_ZoomLens.valueZoomLerp > 0.9)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CP_move01>().foxFind = true;
            }
            if (lookStopTimer < 0 && alpha <= 1.0f) {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
            } else if (alpha > 1.0) {
                isDeleting = true;
                foreach (ParticleSystem ef in efFindList) {
                    ef.Play();
                    SoundManager2.Play(SoundData.eSE.SE_GET, SoundData.GameAudioList);
                }
            }
        } else if (isDeleting) {
            alpha -= Time.deltaTime / appearTime;
            sr.color = new Color(1, 1, 1, alpha);
            if (alpha < 0.0f) {
                isByakko_delete = true;
            }
        }
        // ���������烊�Z�b�g
        else
        {
            lookStopTimer = lookStopTime;
            alpha = 0;
            sr.color = new Color(1, 1, 1, alpha);
        }

        oldIsLook = CPData.isLook;

        // �N���A������N���A�X�e�[�W��ۑ�
        if (isClear && isSave == false) {
            ClearManager.SaveClearStage();
            isSave = true;
        }
    }
    public bool GetByakko_delete() {
        return isByakko_delete;
    }


    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.tag == "FoxWindow") {
    //        isWindowColl = true;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isWindowColl = true;
        }
    }
}
