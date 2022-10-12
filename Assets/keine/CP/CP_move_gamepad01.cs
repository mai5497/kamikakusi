using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CP_move_gamepad01 : MonoBehaviour
{
      [SerializeField] Animator FarmerAnimator;

        float speed = 2f;

        const string ANIM_BOOL_MOVE = "Move";

        public void Move(InputAction.CallbackContext context)
        {
            // �X�e�B�b�N�̓��͂��󂯎��
            var v = context.ReadValue<Vector2>();

            // �ړ��ʂ��v�Z
            var mov = new Vector3(v.x * speed * Time.deltaTime, 0, v.y * speed * Time.deltaTime);

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    // ���͊J�n

                    // �A�j���[�V�����ݒ�
                    FarmerAnimator.SetBool(ANIM_BOOL_MOVE, true);

                    // �ړ�����������
                    transform.forward = mov;
                    break;
                case InputActionPhase.Canceled:
                    // ���͏I��

                    // �A�j���[�V�����ݒ�
                    FarmerAnimator.SetBool(ANIM_BOOL_MOVE, false);
                    break;
                default:
                    // �ړ�����������
                    transform.forward = mov;
                    break;
            }

            // �ړ�������
            transform.position = transform.position + mov;
        }


    
}
