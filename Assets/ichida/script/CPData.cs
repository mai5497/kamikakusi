//=============================================================================
//
// ゲーム上のデータを管理するクラス
//
// 作成日:2022/10/12
// 作成者:伊地田真衣
//
// <開発履歴>
// 2022/10/12 作成
//
//=============================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPData : MonoBehaviour
{
    /*
     * プレイヤーのデータだけを保存するクラスにするつもりだったけど
     * いろいろ保持するクラスになっちゃった
     * 
     * ※印が付いてるものはデバッグ用にゲームマネージャーで値が変更できるようになっているため
     * このクラスの初期化子付き宣言を書き換えても上限は変わらないので注意
     */

    public static  bool isLook = false;     // 注視しているフラグ
    public static bool isLens = false;      // 動いているかレンズ使ってるか
    
    public static bool isPose = false;      //ポーズしているかフラグ

    public static Vector2 playerPos = Vector2.zero; // プレイヤーの座標    

    public static bool isRightAnswer = false;   // 覗いてるところがあってるのであれあばtrue

    public static bool isKokkurisan = false;      // こっくりさん画面をだしてるか
    public static bool kokkurisanButton = false;    // こっくりさんの画面のぼたんがおされてるかどうか

    public static bool isObjNameUI = false;      // ヒントとオブジェクトの名前を表示する 

    public static int paperCnt = 5;         // 紙を見ることができる回数（※）
    public static int lookCnt = 5;          // 注視できる回数（※）

    public static string kituneClearStr;    // 狐の目で見た時の正解（※）
    public static string normalClearStr;    // 人の目で見た時の正解（※）

    public static string kituneHint;        // 狐の目のヒント(※)
    public static string normalHint;        // 人の目のヒント(※)
}
