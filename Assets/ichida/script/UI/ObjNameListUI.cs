//=============================================================================
//
// �I�u�W�F�N�g�̖��O��UI
//
// �쐬��:2022/10/17
// �쐬��:�ɒn�c�^��
//
// <�J������>
// 2022/10/17 �쐬
// 2022/10/18 �ϐl�d�������ŕ\��
//            �A�C�R���\���ł���
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjNameListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject iconObj;     // �A�C�R���p�̃I�u�W�F�N�g��

    private List<string> nameList;  // �I�u�W�F�N�g�̕����������
    private List<Sprite> spriteList;// �I�u�W�F�N�g�̃A�C�R���Ƃ��ăX�v���C�g���擾���Ă���
    private GameObject[] objList;   // �I�u�W�F�N�g���̂�����

    private GameObject[] textObj = new GameObject[2];   // ���E�ɕ�����Ă���̂łQ��
    private Text rightText;
    private Text leftText;

    private Image[] iconImage;   // �A�C�R���\���p�X�v���C�g
    private GameObject[] iconObjEntity; // �A�C�R���\���I�u�W�F�N�g�̎���

    // Start is called before the first frame update
    void Start()
    {
        //----- ������\������e�L�X�g�{�b�N�X�����E�ɕ��������炻�ꂼ��擾 -----
        textObj[0] = transform.GetChild(0).gameObject;  // ��
        textObj[1] = transform.GetChild(1).gameObject;  // �E

        leftText = textObj[0].GetComponent<Text>();
        rightText = textObj[1].GetComponent<Text>();


        objList = GameObject.FindGameObjectsWithTag("SearchObject"); // �^�O���t�����S�ẴI�u�W�F�N�g���擾����

        nameList = new List<string>();
        spriteList = new List<Sprite>();

        //----- �ςƐl���̃I�u�W�F�N�g�̖��O���d�������Ŋi�[���� -----
        for (int i = 0; i < objList.Length; i++) {
            string work;   // �ꎞ�I�ɖ��O���i�[���邽�߂̕ϐ�

            //----- �l�̑��̃I�u�W�F�N�g�̊i�[ -----
            work = objList[i].GetComponent<HintObj>().GetObjName(); // �l���̖��O�̊i�[
            if (!nameList.Contains(work)) {  // ���X�g�ɗv�f��������Βǉ�
                nameList.Add(work);
                spriteList.Add(objList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite); // �A�C�R���p��sprite�𔲂����
            }
            //----- �ς̑��̃I�u�W�F�N�g�̊i�[ -----
            work = objList[i].GetComponent<HintObj>().GetUraObjName();  // �ϑ��̖��O�̊i�[
            if (!nameList.Contains(work)) {  // ���X�g�ɗv�f��������Βǉ�
                nameList.Add(work);
                spriteList.Add(objList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite);// �A�C�R���p��sprite�𔲂����
            }
        }

        //----- �A�C�R���\�� -----
        iconImage = new Image[nameList.Count];
        iconObjEntity = new GameObject[nameList.Count];

        for (int i = 0; i < nameList.Count; i++) {
            iconObjEntity[i] = Instantiate(iconObj);    // ���̉�
            iconObjEntity[i].transform.SetParent(this.transform, false);// ���̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
            iconImage[i] = iconObjEntity[i].GetComponent<Image>();
            iconImage[i].sprite = spriteList[i];    // ���炩���ߔ�������Ă�����sprite���擾����
            iconImage[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);  // ���傱���Ə��������ĕ\��

            if (i > 4) {    // �E��
                rightText.text = rightText.text + nameList[i] + "\n";
                iconObjEntity[i].transform.localPosition = new Vector3(rightText.transform.localPosition.x - 250, rightText.transform.localPosition.y - (i - 7) * 100, -5);
            } else {    // ����
                leftText.text = leftText.text + nameList[i] + "\n";
                iconObjEntity[i].transform.localPosition = new Vector3(leftText.transform.localPosition.x - 250, leftText.transform.localPosition.y - (i - 2) * 100, -5);
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
