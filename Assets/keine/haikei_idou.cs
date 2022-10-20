using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haikei_idou : MonoBehaviour
{
    public CP_move01 Player;

    public Transform transform;

    public float reate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x*reate, 0.0f, 0.0f);
    }
}
