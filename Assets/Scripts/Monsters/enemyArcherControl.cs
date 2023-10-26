using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyArcherControl : MonoBehaviour
{
    public float moveSpeed = 0.5f; // ������ �̵��ӵ�
    public float attackLongRange = 4.0f; // ��� ���� ��Ÿ�
    public float attackDirectRange = 2.0f; // ���� ���� ��Ÿ�
    public Transform player;
    private Animator animator;

    public GameObject arrowPrefab;
    public float arrowSpeed = 5.0f;

    private MonsterState currentState;
    private bool isShooting = false;
    private bool isAttackCooldown = false;

    private enum MonsterState
    {
        Idle,
        Run,
        AttackLongRange,
        AttackDirect
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;

            if (isShooting) //Ȱ �� ����
            {
                moveSpeed = 0;
                UpdateAnimationState();
            }

            else if (!isShooting && !isAttackCooldown)
            {
                if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) < 10.0f && Mathf.Abs(direction.x) > 4.0f) //�÷��̾� �ν�
                {
                    moveSpeed = 0.5f;
                    currentState = MonsterState.Run;
                    Vector2 moveDirection = direction.normalized;

                    if (moveDirection.x < 0) //���� ��ȯ ���
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                }

                else if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) >= 2.0f && Mathf.Abs(direction.x) <= 4.0f) //��� ����
                {
                    if (!isAttackCooldown) //��ٿ��� ���� �ٽ� ���ݰ���
                    {
                        currentState = MonsterState.AttackLongRange;
                        Vector2 moveDirection = direction.normalized;

                        if (moveDirection.x < 0) //���� ��ȯ ���
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        isShooting = true;

                        Invoke("FireArrow",2.0f);
                    }

                }
             
                else if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) < 2.0f) //���� ����
                {
                    if (!isAttackCooldown) //��ٿ��� ���� �ٽ� ���ݰ���
                    {
                        currentState = MonsterState.AttackDirect;
                        Vector2 moveDirection = direction.normalized;

                        if (moveDirection.x < 0) //���� ��ȯ ���
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        UpdateAnimationState();
                        isShooting = true;

                        Invoke("FireArrow", 2.0f);
                    }

                    else
                    {
                        currentState = MonsterState.Idle;
                    }
                }
                UpdateAnimationState();
            }            
        }

        void UpdateAnimationState()
        {
            switch (currentState)
            {
                case MonsterState.Idle:
                    animator.SetBool("Idle", true);
                    animator.SetBool("Run", false);
                    animator.SetBool("AttackLongRange", false);
                    animator.SetBool("AttackDirect", false);
                    break;

                case MonsterState.Run:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Run", true);
                    animator.SetBool("AttackLongRange", false);
                    animator.SetBool("AttackDirect", false);
                    break;

                case MonsterState.AttackLongRange:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("AttackLongRange", true);
                    animator.SetBool("AttackDirect", false);
                    break;

                case MonsterState.AttackDirect:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Run", false);
                    animator.SetBool("AttackLongRange", false);
                    animator.SetBool("AttackDirect", true);
                    break;
            }
        }
    }

    public void FireArrow()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 0.5f, 0);
        GameObject arrow = Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);

        Vector2 direction = player.position - transform.position;

        if (Mathf.Abs(direction.x) >= 4.0f)
        {
            // ȭ���� ���������� �߻��մϴ�
            // �߷� �� �ʱ� �ӵ� �� ������ �����մϴ�

            float gravity = 9.81f; // �߷� ���ӵ� (9.81 m/s^2)
            float timeToReachPlayer = Mathf.Sqrt((2 * direction.magnitude) / gravity);

            // �ʱ� �ӵ��� ����Ͽ� ������ �������� �и��մϴ�
            float initialVelocityX = direction.x / timeToReachPlayer;
            float initialVelocityY = direction.y / timeToReachPlayer - 0.5f * gravity * timeToReachPlayer;

            // �ձ۰� �׸����� �߷��� ������ ���ϰ� �մϴ�
            initialVelocityY *= 0.1f;

            // ȭ���� Rigidbody2D �ӵ��� �����մϴ�
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(initialVelocityX, initialVelocityY);
        }
        else
        {
            // ȭ���� �������� �߻��մϴ�
            // ȭ���� Rigidbody2D �ӵ��� �÷��̾ ���� ���� �����մϴ�
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * arrowSpeed;
        }
    }
}