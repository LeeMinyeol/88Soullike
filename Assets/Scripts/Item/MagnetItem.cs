using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool magnetTime;
    [SerializeField] private float bounceForce;
    private Rigidbody2D rb;

    private void Start()
    {
        ApplyRandomForce();
    }
    private void Update()
    {
        if (magnetTime && target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.1f); // ���� �������Ե� �Ƿ���..*/
    }

    private void OnEnable()
    {
        target = null;
        magnetTime = false;
        bounceForce = 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MagnetField"))
        {
            target = collision.gameObject;
            magnetTime = true;
        }
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void ApplyRandomForce()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized; // ������ ���� ���� ����
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(randomDirection * bounceForce, ForceMode2D.Impulse);
        }
    }
}
