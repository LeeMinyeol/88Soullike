using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour 
{
    public GameObject[] monster; // ��ȯ�� �Ϲ� ���� ������ �迭

    // Start is called before the first frame update
    void Start() //�ʿ� ��ȯ�� ���Ϳ� ��ǥ�� �Է��ؼ� �߰�
    {
        SpawnMonster(monster[0], new Vector3(1.0f, 1.0f, 1.0f));
        SpawnMonster(monster[1], new Vector3(5.0f, 3.0f, 1.0f));
        SpawnMonster(monster[2], new Vector3(9.0f, 5.0f, 1.0f));
    }

    public void SpawnMonster(GameObject curmonster, Vector3 spawnPosition)
    {
        GameObject curMonster = Instantiate(curmonster);
        curMonster.transform.position = spawnPosition;
    }
}