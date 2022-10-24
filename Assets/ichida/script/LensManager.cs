//=============================================================================
//
// �ς̑��̐���
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
// �ҏW��:��D��
//
// <�J������>
// 2022/10/17 �쐬
// 2022/10/24 �ҏW(��������)
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{
    [Header("�ڂ���")]
    [SerializeField]
    private BlurIn blurIn;
    [Header("�����Y�I�u�W�F�N�g")]
    [SerializeField]
    private GameObject lensObj;
    [Header("�Y�[��")]
    [SerializeField]
    private ZoomLens zoomLens;
    [Header("�����Y���x�{��")]
    [SerializeField]
    private float lensSpeed = 0.05f;


    private CP_move01 _Player;

    // �����Y��
    private EnableLens enableLens;

    private bool oldIsLens;         // islens��true�ɂȂ����u�Ԃ݂̂��鏈���p
    private Vector2 oldPlayerPos;   // �ړ����Ă��Ȃ���ΐ�قǂ܂ŕ\�����Ă����ꏊ�ɕ\������ׂ̕ϐ�

    private bool isLensInit;    // �����Y�̈ʒu�����������邩

    private RectTransform lensRT;

    private RectTransform lensCanvas;

    // �����x
    public float moveAccel = 0.0f;
    [Header("�ړ�����_���x")]
    public float moveAccelSpeed;
    [Header("�ړ�����_���x")]
    public float moveDecelSpeed;
    // ���݂̃t���[��
    private int nowFrame = 0;
    // �ۑ�����t���[����
    private const int moveFrameMax = 10;
    // ���t���[���O���g�p���邩
    private const int useFrame = 5;
    // �v���C���[���x�𖈃t���[���ۑ����Ă��郊�X�g
    private Vector2[] moveBeforeList = new Vector2[moveFrameMax];

    // �J�������W
    private Transform cameraTrans;
    [Header("�J�����␳�l")]
    public Vector2 cameraHosei = new Vector2(0.5f, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<CP_move01>();
        // �����Y���̎擾
        enableLens = lensObj.GetComponent<EnableLens>();
        lensRT = lensObj.GetComponent<RectTransform>();

        lensCanvas = GameObject.Find("CanvasLens").GetComponent<RectTransform>();

        oldIsLens = CPData.isLens;
        oldPlayerPos = CPData.playerPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldPlayerPos != CPData.playerPos)
        {
            isLensInit = true;

            oldPlayerPos = CPData.playerPos;
        }

        if (oldIsLens != CPData.isLens)
        {
            if (blurIn.isBlur == false)
            {
                // �ڂ���,�����Y�̗L��
                blurIn.isBlur = true;
                enableLens.EnableImage(true);
            }
            else
            {
                // �����Y�̖���
                blurIn.isBlur = false;
                enableLens.EnableImage(false);
            }

            oldIsLens = CPData.isLens;
        }

        // �����Y�ړ�(�ʏ펞�̂�)
        if (!CPData.isKokkurisan && !CPData.isObjNameUI && CPData.isLens && blurIn.blurMode == BlurIn.BlurMode.Normal)
        {
            if (isLensInit)
            {
                lensRT.position = CPData.playerPos;
                isLensInit = false;
            }

            if (Mathf.Abs(_Player.GetMoveValue().x) > 0.1f || Mathf.Abs(_Player.GetMoveValue().y) > 0.1f)
            {
                UpdateMove();
            }
            else
            {
                UpdateIdle();
            }
        }
        else
        {
            UpdateIdle();
        }


        // �����Y�̒���(�ڂ������[�h�ύX)
        if (CPData.isLook && CPData.isLens)
        {
            if (blurIn.blurMode == BlurIn.BlurMode.Normal)
            {
                blurIn.blurMode = BlurIn.BlurMode.PressInit;
                // �Y�[������
                zoomLens.isZoom = true;
            }
        }
        else
        {
            blurIn.blurMode = BlurIn.BlurMode.Normal;
            // �Y�[����������
            zoomLens.isZoom = false;
        }
    }

    // ����(����)
    private void UpdateMove()
    {
        Vector2 moveVal;
        Vector3 newLensPos = lensObj.transform.position;
        moveVal.x = _Player.GetMoveValue().x * lensSpeed * moveAccel * Time.deltaTime;
        moveVal.y = _Player.GetMoveValue().y * lensSpeed * moveAccel * Time.deltaTime;

        cameraTrans = Camera.main.transform;

        newLensPos.x += moveVal.x;
        newLensPos.y += moveVal.y;

        lensObj.transform.position = new Vector2(
                     //�G���A�w�肵�Ĉړ�����
                     Mathf.Clamp(newLensPos.x, -(9.0f - cameraHosei.x) + cameraTrans.position.x, (9.0f - cameraHosei.x) + cameraTrans.position.x),
                     Mathf.Clamp(newLensPos.y, -(5.0f - cameraHosei.y) + cameraTrans.position.y, (5.0f - cameraHosei.y) + cameraTrans.position.y)
                     );

        // ��������
        moveAccel += moveAccelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);

        // ���݃t���[���Ƀv���C���[�̑��x��ۑ�
        moveBeforeList[nowFrame] = _Player.GetMoveValue();
        nowFrame++;
        if (nowFrame >= moveFrameMax)
        {
            nowFrame = 0;
        }
    }
    // �ҋ@(����)
    private void UpdateIdle()
    {
        Vector2 moveVal;
        Vector3 newLensPos = lensObj.transform.position;
        // �w�肵���t���[���̃v���C���[�̈ړ����x�����o��
        int frame = nowFrame - useFrame;
        if (frame < 0)
        {
            frame = moveFrameMax + frame;
        }
        moveVal.x = moveBeforeList[frame].x * lensSpeed * moveAccel * Time.deltaTime;
        moveVal.y = moveBeforeList[frame].y * lensSpeed * moveAccel * Time.deltaTime;

        newLensPos.x += moveVal.x;
        newLensPos.y += moveVal.y;

        lensObj.transform.position = new Vector2(
                     //�G���A�w�肵�Ĉړ�����
                     Mathf.Clamp(newLensPos.x, -8.5f, 8.5f),
                     Mathf.Clamp(newLensPos.y, -4.5f, 4.5f)
                     );

        // ��������
        moveAccel -= moveDecelSpeed * Time.deltaTime;
        moveAccel = Mathf.Lerp(0, 1, moveAccel);
    }
}
