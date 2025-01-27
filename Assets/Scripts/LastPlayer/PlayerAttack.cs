using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStatusHandler playerStatusHandler;
    private PlayerStat max;
    private Animator anim;
    public LastPlayerController player;
    private SoundManager soundManager;
    private float comboResetTime = 2f;
    private float lastClickTime;
    [SerializeField] public int attackStaminaCost = 5; // 민열님과 얘기
    double nextAttackTime = 0f;

    public bool isParrying = false;
    public bool isGuarding = false;
    private float parryWindowEndTime = 0f;
    public bool canAttack = true;

    private int comboAttackClickCount = 0;
    private int manaRegainClickCount = 0;
    [SerializeField] public bool monsterToPlayerDamage;
    public Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    public bool comboAttack;
    private Test test;
    private int comboCount=1;
    public GameObject parryingObject;
    public GameObject shieldObject;
    // Start is called before the first frame update

    void Start()
    {
        canAttack = true;
        max = playerStatusHandler.GetStat();
        soundManager = SoundManager.instance;
        parryingObject.SetActive(false);
        shieldObject.SetActive(false);
    }
    private void Awake()
    {

        anim = GetComponent<Animator>();
        playerStatusHandler = GetComponent<PlayerStatusHandler>();
        test = GetComponent<Test>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDefense();
        CheckAttackTime();
        ResetClickCount();
    }


    public void CheckDefense()
    {
        if (isGuarding)
        {
            monsterToPlayerDamage = true;
        }

        // 패링 체크
        if (Input.GetMouseButtonDown(1) && !isParrying)
        {
            isParrying = true;
            parryingObject.SetActive(true);
            parryWindowEndTime = Time.time + 0.5f;
        }
        else if (Input.GetMouseButtonUp(1) && isParrying)
        {
            isParrying = false;
            parryingObject.SetActive(false);
        }

        // 가드 체크
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            canAttack = false;
            isGuarding = true;
            shieldObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            canAttack = true;
            isGuarding = false;
            shieldObject.SetActive(false);
        }

        // 패링 윈도우 만료 체크
        if (Time.time > parryWindowEndTime)
        {
            isParrying = false;
            parryingObject.SetActive(false);
        }
    }


private void ManaPlus()
    {
        //Debug.Log("comboCount:" + comboCount);
        if (comboCount %3 ==0)
        {
            if (max.mana < playerStatusHandler.currentMana)//(현재마나  < 맥스마나 )
            {
                max.mana += 1;
            }
        }
    }

    private void ClickCount()
    {
        comboAttackClickCount += 1;
        lastClickTime = Time.time;
        //Debug.Log(comboAttackClickCount);
    }
    private void ResetClickCount()
    {
        // 클릭 카운터 초기화
        if (Time.time - lastClickTime > comboResetTime)
        {
            comboAttackClickCount = 0;
        }
    }
    private void CheckAttackTime()
    {

        if (Time.time >= nextAttackTime)//다음 공격 가능 시간 
        {
            if (canAttack == true)
            {
                if (Input.GetMouseButtonDown(0) && player.isGrounded) //&& PopupUIManager.instance.activePopupLList.Count <= 0)
                {
                    //double sp = gameManager.playerStats.AttackSpeed + 1f; // AttackSpeed= 1 // 아이템 공속 감소?
                    nextAttackTime = Time.time + 1f; // / sp  <= 삭제함( 수정 필요 )
                    if (playerStatusHandler.currentStemina >= attackStaminaCost)
                    {
                        playerStatusHandler.currentStemina -= attackStaminaCost;
                        anim.SetTrigger("attack");
                        soundManager.PlayClip(test.attackSound);
                        ApplyDamage();
                    }
                }
            }
        }
    }

    private void RegainHp(int damage)
    {
        int heal = damage;
        if (playerStatusHandler.currentHp < playerStatusHandler.currentRegainHp)
        {
            playerStatusHandler.currentHp += heal / 4;
        }
    }

    private int DamageCalculator()
    {
        int modifiedAttackDamage = playerStatusHandler.currentDamage;
        if (comboAttackClickCount != 3)
        {
//            soundManager.PlayClip(test.attackSound);
            comboAttack = false;
        }
        else
        {
            anim.SetTrigger("combo");
            soundManager.PlayClip(test.comboAttackSound);
            playerStatusHandler.currentStemina -= attackStaminaCost * 2;
            modifiedAttackDamage *= 2;
            comboAttackClickCount = 0;
            comboAttack = true;
            comboCount += 1;
            ManaPlus();
        }
        modifiedAttackDamage = playerStatusHandler.CriticalCheck(modifiedAttackDamage);
        return modifiedAttackDamage;
    }

    private void ApplyDamage() // Add damage To Monster
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, playerStatusHandler.currentAttackRange, enemyLayer);
        //Debug.Log("enemyLayer : " + enemyLayer);
        //Debug.Log("hitEnemy : " + hitEnemies.Length);
        if (hitEnemies.Length != 0)
        {
            ClickCount();
            Debug.Log(comboAttackClickCount);
            int damage = DamageCalculator();
            foreach (Collider2D enemyCollider in hitEnemies)
            {
                EnemyStatusHandler enemyhandler = enemyCollider.GetComponent<EnemyStatusHandler>();
                enemyhandler.TakeDamage(damage);
                RegainHp(damage);

            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(attackPoint.position, stat.attackRange);

    //}
}