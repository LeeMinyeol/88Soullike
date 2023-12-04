using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Skill : MonoBehaviour
{
    [SerializeField] private SkillSO curSkill;

    [SerializeField] private SkillType type;
    [SerializeField] private bool activeType;
    [SerializeField] private string skillName;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private List<SkillEffect> efts;
    [SerializeField] private int power; // ������ ����(ex: ��ų���ݷ� * power) or �����ð� ��
    [SerializeField] private int cost; // ���� �ڽ�Ʈ
    [SerializeField] private List<String> descriptiion;
    [SerializeField] private PropertyType skillProperty;
    [SerializeField] private int price; // ����(�������� �춧�� ������, �����ǸŰ� �Ұ����� ��� 0)

    CharacterStats characterStats;
    Vector3 dir;
    private void Start()
    {
        Init();
        characterStats = GameManager.Instance.playerStats;
        dir = new Vector3(GameManager.Instance.lastPlayerController.facingDirection, 0, 0);

    }
    private void Update()
    {
        StartCoroutine(move());
    }

    public void Init()
    {
        if (curSkill == null) return;
        type = curSkill.Type;
        activeType = curSkill.ActiveType;
        skillName = curSkill.SkillName;
        skillIcon = curSkill.SkillIcon;
        efts = curSkill.SkillEffects;
        power = curSkill.Power;
        cost = curSkill.Cost;
        descriptiion = curSkill.Description;
        skillProperty = curSkill.SkillProperty;
        price = curSkill.Price;
    }

    public bool Use()
    {
        if (characterStats.characterMana <= 0) return false;

        bool isUsed = false;

        foreach (SkillEffect eft in efts)
        {

            isUsed = eft.ExcuteRole(power, type);
        }

        if (isUsed)
        {
            CostDecrease();
        }


        return isUsed; // ��ų ��� ���� ����
    }

    void CostDecrease()
    {
        if (characterStats.characterMana >= cost)
        {
            characterStats.characterMana -= cost;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<skeletonEnemy>().TakeDamage(power + characterStats.NormalSkillDamage);
            //power�� �÷��̾� �ֹ����� ������� �������� �� collision.getComponent<Enemy>().TakeDamage??
            if (activeType)
            {
                Destroy(gameObject);
            }
        }

    }

    IEnumerator move()
    {
        if (dir.x > 0)
        {
            transform.GetComponent<SpriteRenderer>().flipX = activeType;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().flipX = !activeType;

        }

        if (activeType)
        {
            transform.Translate(new Vector3(dir.x
                * 20 * Time.deltaTime, 0, 0)); // 1f�� �÷��̾ �ٶ󺸴� ���⿡ ���� -1f or 1f
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            // ����ǰ� 1�� �� �����
        }
        Destroy(gameObject);

        yield return null;
    }

    #region ������Ƽ
    public SkillType Type { get { return type; } }
    public bool ActiveType { get { return activeType; } set { activeType = value; } }
    public string SkillName { get { return skillName; } set { skillName = value; } }
    public Sprite SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    public List<SkillEffect> Efts { get { return efts; } set { efts = value; } }
    public int Power { get { return power; } set { power = value; } }
    public int Cost { get { return cost; } set { cost = value; } }
    public List<String> description { get { return descriptiion; } set { descriptiion = value; } }
    public PropertyType SkillProperty { get { return skillProperty; } }
    public int Price { get { return price; } set { price = value; } }
    #endregion
}
