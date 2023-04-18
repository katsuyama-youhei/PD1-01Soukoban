using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0, 0 };
        // 追加、文字列の宣言と初期化
        /* string debugText = "";
         // Debug.Log("Hello World");
         for (int i = 0; i < map.Length; i++)
         {
             // 変更、文字列に結合していく
             debugText += map[i].ToString() + ",";
         }
         // 結合した文字列を出力
         Debug.Log(debugText);*/
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 1. をここから記述
            // 見つからなかった時のために-1で初期化
            int playerIndex = GetPlayerIndex();
            // 要素数はmap.Lengthで取得
            /*   for (int i = 0; i < map.Length; i++)
               {
                   if (map[i] == 1)
                   {
                       playerIndex = i;
                       break;
                   }
               }*/
            // 都築2. 3.を記述していく
            MoveNumber(1, playerIndex, playerIndex + 1);
            /* if (playerIndex < map.Length - 1)
             {
                 map[playerIndex + 1] = 1;
                 map[playerIndex] = 0;
             }*/

            PrintArray();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 1. をここから記述
            // 見つからなかった時のために-1で初期化
            int playerIndex = GetPlayerIndex();
            // 要素数はmap.Lengthで取得
            /*for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }*/
            // 都築2. 3.を記述していく
            MoveNumber(1, playerIndex, playerIndex - 1);
            /* if (playerIndex > 0)
             {
                 map[playerIndex - 1] = 1;
                 map[playerIndex] = 0;
             }*/

            PrintArray();
        }
    }

    // クラスの中、メソッドの外に置くことに注意
    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        //　移動先が 範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.Length) { return false; }
        // 移動先に2（箱）が居たら
        if (map[moveTo] == 2)
        {
            // どの方向に移動するかを算出
            int velocity = moveTo - moveFrom;
            // プレイヤーの移動先から、さらに先へ2(箱)をいどうさせる
            // 箱の移動処理、MoveNumberメソッド内でMoveNumberメソッドを
            // 呼び、処理が再帰している。移動不可をboolで記録
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            // もし箱が移動失敗したら、プレイヤーの移動も失敗
            if (!success) { return false; }
        }

        /* if (moveTo < 0 || moveTo >= map.Length)
         {
             return false;
         }*/
        // プレイヤー・箱関わらずの移動処理
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}