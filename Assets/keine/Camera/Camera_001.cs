using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_001 : MonoBehaviour
{
    GameObject playerObj;
    CP_move01 player;
    Transform playerTransform;


  // Transform CameraTransform;

  //�J�����������邩
    public bool canMove_camera=true;

    //private Camera camera=null;
   // float Old_Player_Pos =0.0f;
  // �J�����̍��ƉE
    float camera_right;
    float camera_left;
    //public GameObject[] wall;
    //  public Vector3 wallX;

    //GameObject wall;
    //Transform wallTransform;
    //float wallX;

    GameObject wallObj_left;
    GameObject wallObj_right;

    float rightTop = 0;
    float leftBottom = 0;
    float depth = 5.0f;
    void Start()
    {
        //�v���C���[�^�O�擾
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<CP_move01>();
        playerTransform = playerObj.transform;
      //  CameraTransform = this.transform;
        camera_right = Screen.width +( Screen.width / 2);
        camera_left = this.transform.position.x - (Screen.width / 2);


        wallObj_left = GameObject.FindGameObjectWithTag("TestWallLeft");
        wallObj_right = GameObject.FindGameObjectWithTag("TestWallRight");

        //  camera = GetComponent<Camera>();


       
        //wall = GameObject.FindGameObjectWithTag("wall");
        //wallTransform = wall.transform;
        //wallX = wallTransform.position.x;
        //wall = GameObject.FindGameObjectsWithTag("wall");

        //wall = GameObject.FindGameObjectWithTag("wall");
        //wallTransform = wall.transform;
        //wallX = wallTransform.position.x;
        //wall = GameObject.FindGameObjectsWithTag("wall");

    }

    void Update()
    {
        //camera_right = this.transform.position.x + (Screen.width / 2);
        //camera_left = this.transform.position.x - (Screen.width / 2);

      

        //�J�����̗��[���擾
        camera_right = this.transform.position.x +9.0f;
        camera_left = this.transform.position.x -9.0f;


        // Debug.Log(camera_left + ",,,,"+camera_right+",,,,"+"aaaaaaaaaa"+Screen.width/2);

     //   Debug.Log(this.transform.position.x);

        //// Screen.width;
        if (camera_left <= wallObj_left.transform.position.x) 
        {
            canMove_camera = false;
          //  Debug.Log("���~�܂�܂��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

           if(playerTransform.position.x>= this.transform.position.x)
            {
                canMove_camera = true;
            }

        }
        if (camera_right > wallObj_right.transform.position.x)
        {
            canMove_camera = false;
          //  Debug.Log("�E�~�܂�܂��I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");
            if (playerTransform.position.x <= this.transform.position.x)
            {
                canMove_camera = true;
            }


        }
       
        //if (camera_right < wallObj_right.transform.position.x && camera_left < wallObj_left.transform.position.x)
        //{
        //    Debug.Log("�J�����������I�I�I�I�I�I�I�I�I�I�I�I�I�I�I�I");

        //    canMove_camera = true;
        //}


        if (canMove_camera)
        {
            MoveCamera();
        }
        else
        {
           // cant_MoveCamera();
        }
    }


    void MoveCamera()
    {
        //�Ǐ]
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
    }


    public bool Get_camera_move()
    {
        return canMove_camera;
    }

    //void cant_MoveCamera()
    //{

    //    Old_Player_Pos = playerTransform.position.x;

    //    //�����������Ǐ]
    //    transform.position = new Vector3( Old_Player_Pos, transform.position.y, transform.position.z);

       
    //}
    //private void OnCollisionEnter2D()
    //{
    //    if(other.gameObject.CompareTag("wall"))
    //    {


    //    }


    //}

}