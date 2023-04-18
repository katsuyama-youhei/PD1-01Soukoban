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
        // �ǉ��A������̐錾�Ə�����
        /* string debugText = "";
         // Debug.Log("Hello World");
         for (int i = 0; i < map.Length; i++)
         {
             // �ύX�A������Ɍ������Ă���
             debugText += map[i].ToString() + ",";
         }
         // ����������������o��
         Debug.Log(debugText);*/
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 1. ����������L�q
            // ������Ȃ��������̂��߂�-1�ŏ�����
            int playerIndex = GetPlayerIndex();
            // �v�f����map.Length�Ŏ擾
            /*   for (int i = 0; i < map.Length; i++)
               {
                   if (map[i] == 1)
                   {
                       playerIndex = i;
                       break;
                   }
               }*/
            // �s�z2. 3.���L�q���Ă���
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
            // 1. ����������L�q
            // ������Ȃ��������̂��߂�-1�ŏ�����
            int playerIndex = GetPlayerIndex();
            // �v�f����map.Length�Ŏ擾
            /*for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == 1)
                {
                    playerIndex = i;
                    break;
                }
            }*/
            // �s�z2. 3.���L�q���Ă���
            MoveNumber(1, playerIndex, playerIndex - 1);
            /* if (playerIndex > 0)
             {
                 map[playerIndex - 1] = 1;
                 map[playerIndex] = 0;
             }*/

            PrintArray();
        }
    }

    // �N���X�̒��A���\�b�h�̊O�ɒu�����Ƃɒ���
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
        //�@�ړ��悪 �͈͊O�Ȃ�ړ��s��
        if (moveTo < 0 || moveTo >= map.Length) { return false; }
        // �ړ����2�i���j��������
        if (map[moveTo] == 2)
        {
            // �ǂ̕����Ɉړ����邩���Z�o
            int velocity = moveTo - moveFrom;
            // �v���C���[�̈ړ��悩��A����ɐ��2(��)�����ǂ�������
            // ���̈ړ������AMoveNumber���\�b�h����MoveNumber���\�b�h��
            // �ĂсA�������ċA���Ă���B�ړ��s��bool�ŋL�^
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            // ���������ړ����s������A�v���C���[�̈ړ������s
            if (!success) { return false; }
        }

        /* if (moveTo < 0 || moveTo >= map.Length)
         {
             return false;
         }*/
        // �v���C���[�E���ւ�炸�̈ړ�����
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}