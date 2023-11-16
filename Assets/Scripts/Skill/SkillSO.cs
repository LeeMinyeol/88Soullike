using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/Skill", order = 0)]
public class SkillSO : ScriptableObject
{
    [SerializeField] private SkillType type;
    [SerializeField] private string skillName;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private List<SkillEffect> efts;
    [SerializeField] private int power; // ������ ����(ex: ��ų���ݷ� * power) or �����ð� ��
    [SerializeField] private List<String> descriptiion;
    [SerializeField] private PropertyType skillProperty; // Ÿ���� ������ ��츸 ���
    [SerializeField] private int price; // ����(�������� �춧�� ������, �����ǸŰ� �Ұ����� ��� 0)
    public bool Buyable() // true�� ��ų�� ������ ǥ��, ex: Ư�������� ��� �رݵǴ� ��ų�̸� Buyable�� false���� true�� ����
    {
        return true;
    }
}

public enum SkillType
{
    Active,
    Passive,
    Buff
}
