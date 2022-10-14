using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPData : MonoBehaviour
{
    public static  bool isLook = false;     // 注視しているフラグ
    public static bool isLens = false;      // 動いているかレンズ使ってるか
    
    public static bool isPose = false;      //ポーズしているかフラグ

    public static Vector2 playerPos = Vector2.zero; // プレイヤーの座標    

    public static bool isRightAnswer = false;   // 覗いてるところがあってるのであれあばtrue

    public static bool isHint = false;      // こっくりさん画面をだしてるか
}
