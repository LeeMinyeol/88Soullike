using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponColliderRange : MonoBehaviour
{
    public int damage = 10;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //������ ���� �߰�

        {
            Destroy(gameObject);
            Debug.Log("�÷��̾� ���ݹ���");
        }
    }
}
