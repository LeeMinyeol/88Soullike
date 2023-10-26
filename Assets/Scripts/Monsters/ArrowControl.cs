using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public int damage = 10; // ȭ���� ���� ������ ���Դϴ�.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.right = GetComponent<Rigidbody2D>().velocity;
    }

    // ȭ���� �ٸ� ������Ʈ�� �浹���� �� ȣ��Ǵ� �޼����Դϴ�.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±װ� �ִ��� Ȯ���մϴ�.
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ � ������ ü�� �ý����� ������ �ִ��� �����մϴ�.
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            //if (playerHealth != null)
            {
                // �÷��̾�� �������� �����ϴ�.
                //playerHealth.TakeDamage(damage);
            }

            // ȭ���� �÷��̾�� ������ ��, ȭ���� �ı��մϴ�.
            Destroy(gameObject);
        }
        else
        {
            // "Player" �̿��� �ٸ� ������Ʈ�� ȭ���� �����ϸ� ȭ���� �ı��մϴ�.
            Destroy(gameObject);
        }
    }
}