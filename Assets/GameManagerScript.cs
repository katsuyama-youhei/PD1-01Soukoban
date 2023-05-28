using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        Screen.SetResolution(1920, 1080, false);

        map = new int[,] {

            { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 4, 0, 2, 3, 0, 0, 0, 0, 4 },
            { 4, 0, 3, 2, 0, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 3, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 2, 0, 0, 0, 4 },
            { 4, 0, 1, 0, 0, 0, 0, 0, 4 },
            { 4, 0, 0, 0, 0, 0, 0, 0, 4 },
            { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
        };
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];

        // 変更。二重for分で二次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {

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

            }

        }

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

   Vector2 CubicOut(float t, float totaltime, Vector2 min, Vector2 max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }

    float SineInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

}

