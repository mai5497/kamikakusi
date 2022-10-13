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
            // スティックの入力を受け取る
            var v = context.ReadValue<Vector2>();

            // 移動量を計算
            var mov = new Vector3(v.x * speed * Time.deltaTime, 0, v.y * speed * Time.deltaTime);

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    // 入力開始

                    // アニメーション設定
                    FarmerAnimator.SetBool(ANIM_BOOL_MOVE, true);

                    // 移動方向を向く
                    transform.forward = mov;
                    break;
                case InputActionPhase.Canceled:
                    // 入力終了

                    // アニメーション設定
                    FarmerAnimator.SetBool(ANIM_BOOL_MOVE, false);
                    break;
                default:
                    // 移動方向を向く
                    transform.forward = mov;
                    break;
            }

            // 移動させる
            transform.position = transform.position + mov;
        }


    
}
