//=============================================================================
//
// �q���g�̃I�u�W�F�N�g�ɂ���X�N���v�g
//
// �쐬��:2022/10/12
// �쐬��:�ɒn�c�^��
// �ҏW��:��D��
//
// <�J������>
// 2022/10/12 �쐬
// 2022/10/26 ���d�X�N���[���p�ɓ����蔻��̓��I�ύX
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintObj : MonoBehaviour {

    //[System.NonSerialized]
    public string objName;    // ���̃I�u�W�F�N�g�̖��O���C���X�y�N�^�[�Őݒ肵�Ă���
    //[System.NonSerialized]
    public bool _isHatenaHuman; // �H�ɕϊ����邩
    //[System.NonSerialized]
    public string hatenaObjName;

    //[System.NonSerialized]
    public string uraObjName;
    //[System.NonSerialized]
    public bool _isHatenaFox; // �H�ɕϊ����邩
    //[System.NonSerialized]
    public string hatenaUraName;


    [System.NonSerialized]
    public bool isWindowColl;   // ���Ɠ����������t���O

    private mado _mado;     // ���I�u�W�F�N�g�ɂ��Ă��鑋�X�N���v�g

    [System.NonSerialized]
    public bool isCheckThis;    // ���̃I�u�W�F�N�g�����Ŕ`���ꂽ��

    // �����蔻�菉�����W
    private Vector2 hitInitPos;
    // �����蔻��̕␳
    private float hitCameraHosei = 1.0f;
    private float hitPlayerHosei = 0.25f;
    // �J�����ƃv���C���[�̋���
    private float cameraToPlayer;

    // �g�p�J����
    private Camera cameraUse;

    // �v���C���[�I�u�W�F�N�g
    private GameObject cpObj;

    // Start is called before the first frame update
    void Start() {
        isWindowColl = false;

        _mado = GameObject.Find("Lens").GetComponent<mado>();

        isCheckThis = false;
        
        switch (this.gameObject.layer)
        {
            // Middle1
            case 7:
                cameraUse = GameObject.Find("CameraMiddle1").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle1").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle2
            case 9:
                cameraUse = GameObject.Find("CameraMiddle2").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle2").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
            // Middle3
            case 10:
                cameraUse = GameObject.Find("CameraMiddle3").GetComponent<Camera>();
                hitCameraHosei = GameObject.Find("CameraMiddle3").GetComponent<CameraMiddle>().rate * hitCameraHosei;
                break;
        }

        hitInitPos = this.GetComponent<CapsuleCollider2D>().offset;

        cpObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        // �����蔻��̍��W�ύX
        if (this.gameObject.layer == 7 || this.gameObject.layer == 9 || this.gameObject.layer == 10)
        {
            Vector2 hitPos;
            cameraToPlayer = Camera.main.transform.position.x - cpObj.transform.position.x;
            hitPos.x = hitInitPos.x + (cameraToPlayer * hitCameraHosei + Camera.main.transform.position.x * hitPlayerHosei);
            hitPos.y = hitInitPos.y;
            this.GetComponent<CapsuleCollider2D>().offset = hitPos;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "FoxWindow")
        {
            isWindowColl = true;
            _mado.SetLookObjName(objName, uraObjName);
            //----- �������肳��̎��̃J�E���g -----
            if (isCheckThis == false && CPData.paperCnt > 0)
            {
                if (CPData.isLens && CPData.isKokkurisan)
                {
                    isCheckThis = true;
                    CPData.paperCnt--;
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null, null);
        }
    }

    public string GetObjName() {
        return objName;
    }
    public string GetNormalHatenaStr() {
        return hatenaObjName;
    }

    public string GetUraObjName() {
        return uraObjName;
    }
    public string GetUraHatenaStr() {
        return hatenaUraName;
    }
}
