
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class All_Clear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  if()
        bool fin = ClearManager.GetClearStage(3,3);

        if(fin)
        {
            //SceneManager.LoadScene();
        }



    }
}
