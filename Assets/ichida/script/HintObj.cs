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

    // �Y�[�������Y
    private ZoomLens zoomLens;

    // �q�I�u�W�F�N�g
    List<SpriteRenderer> childrenList = new List<SpriteRenderer>();

    // �q�I�u�W�F�N�g�������W
    Vector3 localInitPos;
    // �q�I�u�W�F�N�g�̏����I�t�Z�b�g
    Vector2 localInitOffset;

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

        zoomLens = GameObject.Find("CanvasLens").GetComponent<ZoomLens>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            childrenList.Add(this.transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        foreach (SpriteRenderer child in childrenList)
        {
            if (child.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
            {
                localInitPos = child.transform.localPosition;
                localInitOffset = this.GetComponent<CapsuleCollider2D>().offset;
            }
        }
    }


    void Update()
    {
        if (zoomLens.isZoom == false)
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

        if (CPData.isLens)
        {
            foreach (SpriteRenderer child in childrenList)
            {
                switch (child.maskInteraction)
                {
                    // ���I�u�W�F�N�g
                    case SpriteMaskInteraction.VisibleInsideMask:
                        child.gameObject.layer = 0;
                        child.transform.localPosition = new Vector3(localInitPos.x + this.GetComponent<CapsuleCollider2D>().offset.x - localInitOffset.x, localInitPos.y + this.transform.GetComponent<CapsuleCollider2D>().offset.y - localInitOffset.y, localInitPos.z);
                        break;
                }
            }
        }
        else
        {
            foreach (SpriteRenderer child in childrenList)
            {
                switch (child.maskInteraction)
                {
                    // �O�I�u�W�F�N�g
                    case SpriteMaskInteraction.VisibleOutsideMask:
                        child.enabled = true;
                        break;
                }
            }
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

            if (CPData.isLens)
            {
                foreach (SpriteRenderer child in childrenList)
                {
                    switch (child.maskInteraction)
                    {
                        // �O�I�u�W�F�N�g
                        case SpriteMaskInteraction.VisibleOutsideMask:
                            child.enabled = false;
                            break;
                    }
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == "FoxWindow") {
            isWindowColl = false;
            _mado.SetLookObjName(null, null);

            if (CPData.isLens)
            {
                foreach (SpriteRenderer child in childrenList)
                {
                    switch (child.maskInteraction)
                    {
                        // �O�I�u�W�F�N�g
                        case SpriteMaskInteraction.VisibleOutsideMask:
                            child.enabled = true;
                            break;
                    }
                }
            }
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
