using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject clearPrefab;
    public GameObject clearText;
    public GameObject particlePrehub;
    public GameObject wallPrefab;

    int[,] map;
    GameObject[,] field;

    // Start is called before the first frame update
    void Start()
    {
        /* 確認したので削除
         GameObject instance=Instantiate(
            playerPrefab,
            new Vector3(0,0,0),
            Quaternion.identity
            );
        */

        Screen.SetResolution(1920, 1080, false);

        map = new int[,] {
            { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 4, 0, 0, 3, 0, 0, 0, 0, 4 },
            { 4, 0, 3, 2, 0, 0, 0, 0, 4 },
            { 4, 0, 2, 1, 3, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 2, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        };
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];
        // string debugText = "";
        // 変更。二重for分で二次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    //GameObject instance = Instantiate(
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                else if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                     boxPrefab,
                     new Vector3(x, map.GetLength(0) - y, 0),
                     Quaternion.identity
                     );
                }
                else if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                    clearPrefab,
                    new Vector3(x, map.GetLength(0) - y, 0.01f),
                    Quaternion.identity
                    );
                }
                else if (map[y, x] == 4)
                {
                    field[y, x] = Instantiate(
                   wallPrefab,
                   new Vector3(x, map.GetLength(0) - y, 0),
                   Quaternion.identity
                   );
                }
                //   debugText += map[y, x].ToString() + ",";
            }
            //debugText += "\n";
        }
        //  Debug.Log(debugText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int asa = new Vector2Int(1, 0);
            Vector2Int tototo = playerIndex + asa;
            MoveNumber("Player", playerIndex, tototo);
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(playerIndex.x, map.GetLength(0) - playerIndex.y, 0);
                Instantiate(particlePrehub, pos, Quaternion.identity);
            }
            //PrintArray();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int asa = new Vector2Int(1, 0);
            Vector2Int tototo = playerIndex - asa;
            MoveNumber("Player", playerIndex, tototo);
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(playerIndex.x, map.GetLength(0) - playerIndex.y, 0);
                Instantiate(particlePrehub, pos, Quaternion.identity);
            }
            //PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int asa = new Vector2Int(0, 1);
            Vector2Int tototo = playerIndex - asa;
            MoveNumber("Player", playerIndex, tototo);
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(playerIndex.x, map.GetLength(0) - playerIndex.y, 0);
                Instantiate(particlePrehub, pos, Quaternion.identity);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            Vector2Int asa = new Vector2Int(0, 1);
            Vector2Int tototo = playerIndex + asa;
            MoveNumber("Player", playerIndex, tototo);
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = new Vector3(playerIndex.x, map.GetLength(0) - playerIndex.y, 0);
                Instantiate(particlePrehub, pos, Quaternion.identity);
            }
        }

        if (IsCleard())
        {
            clearText.SetActive(true);
        }

        if (clearText)
        {
            if (!IsCleard())
            {
                clearText.SetActive(false);
            }
        }
        Particle.particle.Update();
    }

    // クラスの中、メソッドの外に置くことに注意
    void PrintArray()
    {
        // 追加、文字列の宣言と初期化
        /* string debugText = "";
         for (int i = 0; i < map.Length; i++)
         {
             // 変更、文字列に結合していく
             debugText += map[i].ToString() + ",";
         }
         // 結合した文字列を出力
         Debug.Log(debugText);*/
    }

    Vector2Int GetPlayerIndex()
    {
        // 要素数はmap.Lengthで取得

        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        // 見つからなかった時のために-1で初期化
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {
        //　移動先が 範囲外なら移動不可
        //if (moveTo < 0 || moveTo >= map.Length || map[moveTo] == 3) { return false; }
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Wall")
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(tag, moveTo, moveTo + velocity);
            if (!success) { return false; }
        }

        // 移動先に2（箱）が居たら
        /* if (map[moveTo] == 2)
         {
             // どの方向に移動するかを算出
             int velocity = moveTo - moveFrom;
             // プレイヤーの移動先から、さらに先へ2(箱)をいどうさせる
             // 箱の移動処理、MoveNumberメソッド内でMoveNumberメソッドを
             // 呼び、処理が再帰している。移動不可をboolで記録
             bool success = MoveNumber(2, moveTo, moveTo + velocity);
             // もし箱が移動失敗したら、プレイヤーの移動も失敗
             if (!success) { return false; }
         }*/
        // プレイヤー・箱関わらずの移動処理
        /*  map[moveTo] = number;
          map[moveFrom] = 0;*/

        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;

        return true;
    }

    bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }
        return true;
    }

}

