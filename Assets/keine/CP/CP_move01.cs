using UnityEngine;
using UnityEngine.InputSystem;

public class CP_move01 : MonoBehaviour
{
  public  bool isDash = false;

    public float fMove = 0.01f;
    public float fDash = 1.0f;


    void Start()
    {
        //  float fSpeed = 0.01f;
        
    }

    void Update()
    {

       // Transform transform = this.transform;
        //Vector3 pos = transform.position


        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            return;
        }

        // Aキーの入力状態取得
        var aKey = current.aKey;
        // Aキーが押された瞬間かどうか
        if (aKey.isPressed)
        {
            Debug.Log("Aキーが押された！");

            // pos.x -= 0.01f;

            transform.position -= transform.right * fMove;

        }
        else
        {
            isDash = false;
        }

        // Dキーの入力状態取得
        var dKey = current.dKey;
        // Dキーが押された瞬間かどうか
        if (dKey.isPressed)
        {
            Debug.Log("Dキーが押された！");

            // pos.x -= 0.01f;

            transform.position += transform.right * fMove ;

        }
        else
        {
           // isDash = false;
        }
        // shiftキーの入力状態取得
        var shiftKey = current.shiftKey;
        // shiftキーが押された瞬間かどうか
        if (shiftKey.isPressed)
        {
            Debug.Log("shiftキーが押された！");

         
        }
        //走ってるかどうか
        if(isDash)
        {
           
        }


        //transform.position = pos;
    }
}