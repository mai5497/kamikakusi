using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testgetobj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        //�󂯎�����z��̂��ꂼ��̖��O���o��
        foreach (var item in gameObjects) {
            Debug.Log(item.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
