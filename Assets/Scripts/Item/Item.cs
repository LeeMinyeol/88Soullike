using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public ItemSO curItem; // �̸�, �̹���, Ÿ��, �Ŀ�, ����

    public ItemType type;
    public string itemName;
    public Sprite sprite;
    public List<ItemEffect> efts;
    public int power;
    public List<String> description;
    public int amount;
    public float attackRange;
    public float attackSpeed;
    public PropertyType weaponProperty;
    public int weight;
    public int price;

    public bool Use() //��� ������ ���
    {
        bool isUsed = false;
        if (type == ItemType.Armor || type == ItemType.Weapon)
        {
            Equip();
            return isUsed;
        }
        

        foreach(ItemEffect eft in efts)
        {
            isUsed = eft.ExcuteRole(power);
        }
        amount--;
        isUsed = true;
        return isUsed; // ������ ��� ���� ����
    }

    public void Equip() // ��� ������ ����
    {
        Equipment.instance.ChangeEquipItem(this);
        
    }


}
