using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxByakko : MonoBehaviour
{
    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O
    private SpriteRenderer sr;  // �ς̃X�v���C�g�����_���[
    private float alpha;    // �ς̃A���t�@�l

    private const float lookStopTime = 3.0f;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ�����
    private const float appearTime = 1.5f;  // �A���t�@�l���オ�肫��܂ł̎���

    private float lookStopTimer;    // �������Ď~�܂�Ȃ��Ƃ����Ȃ����Ԃ̃J�E���g�p�^�C�}�[

    private bool isByakko_delete = false;
    private bool isDeleting;

    private Kokkurisan kokkurisan;
    private bool isClear;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        isDeleting = false;

        lookStopTimer = lookStopTime;

        sr = GetComponent<SpriteRenderer>();
        alpha = 0.0f;
        sr.color = new Color(1, 1, 1, alpha);

        kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
        if (kokkurisan.MissNum == 0) {
            isClear = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (kokkurisan.MissNum == 0) {
            isClear = true;
        }

        //----- �������� -----
        if (isClear && isWindowColl && CPData.isLook && !isDeleting) {
            CPData.isRightAnswer = true;
            lookStopTimer -= Time.deltaTime;

            if (lookStopTimer < 0 && alpha <= 1.0f) {
                alpha += Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
            } else if(alpha > 1.0){
                isDeleting = true;
            }
        } else if(isDeleting) {
                alpha -= Time.deltaTime / appearTime;
                sr.color = new Color(1, 1, 1, alpha);
                if (alpha < 0.0f) {
                    isByakko_delete = true;
                }
        }
    }
    public bool GetByakko_delete() {
        return isByakko_delete;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
        }
    }
}
