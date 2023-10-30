using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {

    }

    void Update()
    {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //������ ���� �߰�
            
        {
            Destroy(gameObject);
            Debug.Log("�÷��̾� Ȱ ����");
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}