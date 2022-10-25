using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //----- ƒTƒEƒ“ƒh -----
        for (int i = 0; i < SoundData.TitleAudioList.Length; ++i) {
            SoundData.TitleAudioList[i] = gameObject.AddComponent<AudioSource>();
        }
        SoundManager2.Play(SoundData.eBGM.BGM_TITLE, SoundData.TitleAudioList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
