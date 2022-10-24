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
// 2022/10/20 �q���g�̕����ƈ�v�����Ƃ����F��ς��鏈���ǉ�
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjNameListUI : MonoBehaviour {
    [Header("�l�̖ڃq���g�F")]
    [SerializeField]
    private string humanTextColor = "f5e56b";
    [Header("�ς̖ڃq���g�F")]
    [SerializeField]
    private string foxTextColor = "b44c97";

    [SerializeField]
    private GameObject iconObj;     // �A�C�R���p�̃I�u�W�F�N�g��

    private List<string> nameList;  // �I�u�W�F�N�g�̕����������
    private string[] hatenaNameList;  // �I�u�W�F�N�g�̕����������
    private List<Sprite> spriteList;// �I�u�W�F�N�g�̃A�C�R���Ƃ��ăX�v���C�g���擾���Ă���
    private GameObject[] objList;   // �I�u�W�F�N�g���̂�����

    private GameObject[] textObj = new GameObject[2];   // ���E�ɕ�����Ă���̂łQ��
    private Text rightText;
    private Text leftText;

    private Image[] iconImage;   // �A�C�R���\���p�X�v���C�g
    private GameObject[] iconObjEntity; // �A�C�R���\���I�u�W�F�N�g�̎���

    private Kokkurisan _Kokkurisan; // ���Ō��Ă�I�u�W�F�N�g���������擾����

    private string hatenaStr;
    private string notHatenaStr;


    // Start is called before the first frame update
    void Start() {
        //----- ???�̕����̎��� -----
        if (GameObject.Find("CanvasKokkurisan")) {
            _Kokkurisan = GameObject.Find("CanvasKokkurisan").GetComponent<Kokkurisan>();
        }
        hatenaStr = "";
        notHatenaStr = "";

        //----- ������\������e�L�X�g�{�b�N�X�����E�ɕ��������炻�ꂼ��擾 -----
        textObj[0] = transform.GetChild(0).gameObject;  // ��
        textObj[1] = transform.GetChild(1).gameObject;  // �E

        leftText = textObj[0].GetComponent<Text>();
        rightText = textObj[1].GetComponent<Text>();




        //----- �ςƐl���̃I�u�W�F�N�g�̖��O���d�������Ŋi�[���� -----
        objList = GameObject.FindGameObjectsWithTag("SearchObject"); // �^�O���t�����S�ẴI�u�W�F�N�g���擾����

        nameList = new List<string>();
        hatenaNameList = new string[10];
        spriteList = new List<Sprite>();

        for (int i = 0; i < objList.Length; i++) {
            string work;   // �ꎞ�I�ɖ��O���i�[���邽�߂̕ϐ�

            //----- �l�̑��̃I�u�W�F�N�g�̊i�[ -----
            if (objList[i].GetComponent<HintObj>()._isHatenaHuman) {
                work = objList[i].GetComponent<HintObj>().hatenaObjName;
                if (!nameList.Contains(work)) {
                    nameList.Add(work);
                    hatenaNameList[nameList.Count-1] = objList[i].GetComponent<HintObj>().GetObjName();
                    spriteList.Add(objList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite); // �A�C�R���p��sprite�𔲂����
                }
            } else {
                work = objList[i].GetComponent<HintObj>().GetObjName(); // �l���̖��O�̊i�[
                if (!nameList.Contains(work)) {  // ���X�g�ɗv�f��������Βǉ�
                    nameList.Add(work);
                    spriteList.Add(objList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite); // �A�C�R���p��sprite�𔲂����
                }
            }
            //----- �ς̑��̃I�u�W�F�N�g�̊i�[ -----
            if (objList[i].GetComponent<HintObj>()._isHatenaFox) {
                work = objList[i].GetComponent<HintObj>().hatenaUraName;  // �ϑ��̖��O�̊i�[
                if (!nameList.Contains(work)) {
                    nameList.Add(work);
                    hatenaNameList[nameList.Count - 1] = objList[i].GetComponent<HintObj>().GetUraObjName();
                    spriteList.Add(objList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite);// �A�C�R���p��sprite�𔲂����
                }
            } else {
                work = objList[i].GetComponent<HintObj>().GetUraObjName();  // �ϑ��̖��O�̊i�[
                if (!nameList.Contains(work)) {  // ���X�g�ɗv�f��������Βǉ�
                    nameList.Add(work);
                    spriteList.Add(objList[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite);// �A�C�R���p��sprite�𔲂����
                }
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
                rightText.text = rightText.text + nameList[i] + "\n";   // �e�L�X�g�\���p�ɉ��s�̒ǉ�
                iconObjEntity[i].transform.localPosition = new Vector3(rightText.transform.localPosition.x - 300, rightText.transform.localPosition.y - (i - 7) * 110, -5);
            } else {    // ����
                leftText.text = leftText.text + nameList[i] + "\n"; // �e�L�X�g�\���p�ɉ��s�̒ǉ�
                iconObjEntity[i].transform.localPosition = new Vector3(leftText.transform.localPosition.x - 300, leftText.transform.localPosition.y - (i - 2) * 110, -5);
            }
        }



        //----- ���O�̃��X�g�ƈꕶ���̃q���g����v�����Ƃ����F��t���� -----
        // ��
        rightText.text = rightText.text.Replace(CPData.kituneHint, "<color=#" + foxTextColor + ">" + CPData.kituneHint + "</color>");
        leftText.text = leftText.text.Replace(CPData.kituneHint, "<color=#" + foxTextColor + ">" + CPData.kituneHint + "</color>");
        // �l
        rightText.text = rightText.text.Replace(CPData.normalHint, "<color=#" + humanTextColor + ">" + CPData.normalHint + "</color>");
        leftText.text = leftText.text.Replace(CPData.normalHint, "<color=#" + humanTextColor + ">" + CPData.normalHint + "</color>");
    }

    void Update() {
        bool _isCheckWord = false;
        for (int i = 0; i < 10; i++) {
            if (hatenaNameList[i] == null) {
                continue;
            }
            if (_Kokkurisan.normalAnswerStr == null || hatenaNameList[i] != _Kokkurisan.normalAnswerStr) {
                if (_Kokkurisan.kituneAnswerStr == null || hatenaNameList[i] != _Kokkurisan.kituneAnswerStr) {
                    continue;
                }
            }
            nameList[i] = hatenaNameList[i];
            _isCheckWord = true;
        }

        if (_isCheckWord) {
            rightText.text = "";
            leftText.text = "";

            for (int i = 0; i < nameList.Count; i++) {
                if (i > 4) {    // �E��
                    rightText.text = rightText.text + nameList[i] + "\n";   // �e�L�X�g�\���p�ɉ��s�̒ǉ�
                } else {    // ����
                    leftText.text = leftText.text + nameList[i] + "\n"; // �e�L�X�g�\���p�ɉ��s�̒ǉ�
                }
            }

            //----- ���O�̃��X�g�ƈꕶ���̃q���g����v�����Ƃ����F��t���� -----
            // ��
            rightText.text = rightText.text.Replace(CPData.kituneHint, "<color=#" + foxTextColor + ">" + CPData.kituneHint + "</color>");
            leftText.text = leftText.text.Replace(CPData.kituneHint, "<color=#" + foxTextColor + ">" + CPData.kituneHint + "</color>");
            // �l
            rightText.text = rightText.text.Replace(CPData.normalHint, "<color=#" + humanTextColor + ">" + CPData.normalHint + "</color>");
            leftText.text = leftText.text.Replace(CPData.normalHint, "<color=#" + humanTextColor + ">" + CPData.normalHint + "</color>");
        }

    }
}
