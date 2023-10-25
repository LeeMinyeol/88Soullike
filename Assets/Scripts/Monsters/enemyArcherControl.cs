using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyArcherControl : MonoBehaviour
{
    public float moveSpeed = 0.5f; // ������ �̵��ӵ�
    public float attackLongRange = 4.0f; // ��� ���� ��Ÿ�
    public float attackDirectRange = 2.0f; // ���� ���� ��Ÿ�
    public Transform player;
    private Animator animator;
    private enum MonsterState { Idle, Run, AttackLongRange, AttackDirect };
    private MonsterState currentState = MonsterState.Idle;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;

            if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) < 10.0f && Mathf.Abs(direction.x) > 4.0f) //y������ 0.5����, x������ 4�̻� 8���� ��ŭ ������������ ���ʹ� �÷��̾ �ν��Ѵ�.
            {
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
            else if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) >= 2.0f && Mathf.Abs(direction.x) <= 4.0f) //y������ 0.5����, x������ 2�̻� 4�̸� ��ŭ ������������ ���ʹ� �÷��̾ ���� �����Ѵ�.
            {
                currentState = MonsterState.AttackLongRange;
            }
            else if (Mathf.Abs(direction.y) < 0.5f && Mathf.Abs(direction.x) < 2.0f) //y������ 0.5����, x������ 2�̸����� ������������ ���ʹ� �÷��̾ ����� �����Ѵ�.
            {
                currentState = MonsterState.AttackDirect;
            }
            else
            {
                currentState = MonsterState.Idle;
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