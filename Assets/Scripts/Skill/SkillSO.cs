using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/Skill", order = 0)]
public class SkillSO : ScriptableObject
{
    [SerializeField] private SkillType type;
    [SerializeField] private bool activeType; //false�ٰŸ� true���Ÿ�
    [SerializeField] private string skillName;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private List<SkillEffect> efts;
    [SerializeField] private int power; // ������ ����(ex: ��ų���ݷ� * power) or �����ð� ��
    [SerializeField] private int cost; // ���� �ڽ�Ʈ
    [SerializeField] private List<String> descriptiion;
    [SerializeField] private PropertyType skillProperty; // Ÿ���� ������ ��츸 ���
    [SerializeField] private int price; // ����(�������� �춧�� ������, �����ǸŰ� �Ұ����� ��� 0)
    public bool Buyable() // true�� ��ų�� ������ ǥ��, ex: Ư�������� ��� �رݵǴ� ��ų�̸� Buyable�� false���� true�� ����
    {
        return true;
    }

    public SkillType Type { get { return type; }}
    public bool ActiveType { get { return activeType; }}
    public string SkillName { get {  return skillName; }}
    public Sprite SkillIcon { get {  return skillIcon; }}
    public List<SkillEffect> SkillEffects { get { return efts; }}
    public int Power { get { return power; }}
    public int Cost { get { return cost; }}
    public List<String> Description { get { return descriptiion; }}
    public PropertyType SkillProperty { get { return skillProperty; }}
    public int Price { get { return price; }}
}

public enum SkillType
{
    Active,
    Passive,
    Buff
}


