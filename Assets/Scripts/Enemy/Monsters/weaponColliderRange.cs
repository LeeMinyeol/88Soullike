using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponColliderRange : MonoBehaviour
{
    public int damage = 10;

    private GameManager gameManager;


    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))

        {
            if (gameManager != null && gameManager.playerStats != null)
            {
                gameManager.playerStats.TakeDamage(damage);
                gameManager.playerStats.ApplyPoisonStatus(5, 3, 50);
                Debug.Log("�÷��̾� ���ݹ���");
                
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("GameManager �Ǵ� playerStats�� ã�� �� �����ϴ�.");
            }
        }
    }
    
}

