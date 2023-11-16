using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossRoomSpawner : MonoBehaviour
{
    public GameObject deathBringer;
    private int dBSpawnCount;
    public GameObject Door;

    private void Start()
    {
        deathBringer.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& dBSpawnCount == 0) //�÷��̾ �����ϸ� ���� Ȱ��ȭ
        {
            deathBringer.SetActive(true);
            dBSpawnCount++;
            

        }
    }
}
