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

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (current == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        // A�L�[�̓��͏�Ԏ擾
        var aKey = current.aKey;
        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.isPressed)
        {
            Debug.Log("A�L�[�������ꂽ�I");

            // pos.x -= 0.01f;

            transform.position -= transform.right * fMove;

        }
        else
        {
            isDash = false;
        }

        // D�L�[�̓��͏�Ԏ擾
        var dKey = current.dKey;
        // D�L�[�������ꂽ�u�Ԃ��ǂ���
        if (dKey.isPressed)
        {
            Debug.Log("D�L�[�������ꂽ�I");

            // pos.x -= 0.01f;

            transform.position += transform.right * fMove ;

        }
        else
        {
           // isDash = false;
        }
        // shift�L�[�̓��͏�Ԏ擾
        var shiftKey = current.shiftKey;
        // shift�L�[�������ꂽ�u�Ԃ��ǂ���
        if (shiftKey.isPressed)
        {
            Debug.Log("shift�L�[�������ꂽ�I");

         
        }
        //�����Ă邩�ǂ���
        if(isDash)
        {
           
        }


        //transform.position = pos;
    }
}