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

    private void Start()
    {
        Init();

    }
    private void Update()
    {
        move();
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
        //�÷��̾� ���� cost -= this.cost;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //power�� �÷��̾� �ֹ����� ������� �������� �� collision.getComponent<Enemy>().TakeDamage??
            if (activeType) {
                Destroy(gameObject);
            }


        }
    }

    IEnumerator move()
    {
        if (activeType)
        {
            transform.Translate(new Vector3(1f * 10 * Time.deltaTime, 0, 0)); // 1f�� �÷��̾ �ٶ󺸴� ���⿡ ���� -1f or 1f
        }
        else
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
            // ����ǰ� 1�� �� �����
        }

        yield return null;
    }

    #region ������Ƽ
    public SkillType Type { get { return type; } }
    public bool ActiveType { get { return activeType; } set{ activeType = value; } }
    public string SkillName { get {  return skillName; } set { skillName = value; } }
    public Sprite SkillIcon { get { return skillIcon; } set { skillIcon = value; } }
    public List<SkillEffect> Efts { get {  return efts; } set {  efts = value; } }
    public int Power { get { return power; } set { power = value; } }
    public int Cost { get { return cost; } set { cost = value; } }
    public List<String> description { get { return descriptiion; } set {  descriptiion = value; } }
    public PropertyType SkillProperty { get { return skillProperty; }}
    public int Price { get { return price; } set { price = value; } }
    #endregion
}
