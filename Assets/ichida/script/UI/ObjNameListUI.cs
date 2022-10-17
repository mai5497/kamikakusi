using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjNameListUI : MonoBehaviour
{
    [Header("�ς�������true")]
    [SerializeField]
    private bool kituneORnormal;    // �ς��l��

    private string[] nameList;  // �I�u�W�F�N�g�̕����������
    private GameObject[] objList;   // �I�u�W�F�N�g���̂�����

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        objList = GameObject.FindGameObjectsWithTag("SearchObject"); // �^�O���t�����S�ẴI�u�W�F�N�g���擾����

        nameList = new string[objList.Length];

        for(int i = 0;i < objList.Length; i++) {
            if (kituneORnormal) {
                nameList[i] = objList[i].GetComponent<HintObj>().GetObjName();
            } else {
                nameList[i] = objList[i].GetComponent<HintObj>().GetUraObjName();
            }
        }

        for (int i = 0; i < nameList.Length; i++) {
            text.text = text.text + nameList[i]+ "\n" ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
