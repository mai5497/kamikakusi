using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPData : MonoBehaviour
{
    public static  bool isLook = false;     // �������Ă���t���O
    public static bool isLens = false;      // �����Ă��邩�����Y�g���Ă邩
    
    public static bool isPose = false;      //�|�[�Y���Ă��邩�t���O

    public static Vector2 playerPos = Vector2.zero; // �v���C���[�̍��W    

    public static bool isRightAnswer = false;   // �`���Ă�Ƃ��낪�����Ă�̂ł��ꂠ��true

    public static bool isHint = false;      // �������肳���ʂ������Ă邩
}
