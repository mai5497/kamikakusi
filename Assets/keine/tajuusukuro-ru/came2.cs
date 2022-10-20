using UnityEngine;
using System.Collections;

public class came2 : MonoBehaviour
{

    private GameObject player;
    private Vector3 startPlayerOffset;
    private Vector3 startCameraPos;
    private static readonly float RATE = 0.12f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPlayerOffset = player.transform.position;
        startCameraPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = (player.transform.position - startPlayerOffset) * RATE;
        this.transform.position = startCameraPos + v;
    }
}