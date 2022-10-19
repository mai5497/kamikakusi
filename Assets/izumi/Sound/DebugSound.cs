using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSound : MonoBehaviour
{
    [Header("ã»êî")]
    public int soundNum = 10;
    [Header("âπó ")]
    public float soundVolume = 1.0f;
    [Header("ã»í«â¡")]
    public bool isAdd;
    private AudioSource[] audioSourceArray;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceArray = new AudioSource[soundNum];
        for (int i = 0; i < soundNum; i++) {
            audioSourceArray[i] = this.gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAdd == true)
        {
            SoundManager2.Play(SoundData.eBGM.BGM_TITLE, audioSourceArray);
            SoundManager2.bgmVolume = soundVolume;
            SoundManager2.setVolume(audioSourceArray);
            isAdd = false;
        }
    }
}
