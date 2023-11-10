using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject growthPopup;
    private bool isOpen = false;

    [Header("General ability")]
    [SerializeField] private TMP_Text levelTxt;
    //[SerializeField] private TMP_Text expTxt; // ����ġ %(���� ����ġ / ����ġ ��)
    [SerializeField] private TMP_Text healthTxt; // ����ü�� / �ִ�ü��
    [SerializeField] private TMP_Text steminaTxt; // ���� ���� / �ִ� ���� 
    [SerializeField] private TMP_Text weightTxt; // ���� ���� / ���� ����
    [SerializeField] private TMP_Text speedTxt; // �̵��ӵ�

    [Header("Attack ability")]
    [SerializeField] private TMP_Text weaponTxt; //�������� �̸�(���� ������ �̸�)
    [SerializeField] private TMP_Text attackTxt; // ���ݷ�
    [SerializeField] private TMP_Text skillTxt; // �ֹ���
    [SerializeField] private TMP_Text propertyTxt; // �Ӽ����ݷ�
    [SerializeField] private TMP_Text criticalTxt; // ġ��Ÿ��
    [SerializeField] private TMP_Text attackSpeedTxt; // ����

    [Header("Deffence / Special")]
    [SerializeField] private TMP_Text deffenceTxt; // ����
    [SerializeField] private TMP_Text parryTimeTxt; // �и����ɽð�
    [SerializeField] private TMP_Text addGoodTxt; // ������ ȹ�淮

    [Header("growStat")]
    [SerializeField] private TMP_Text growPoint;
    [SerializeField] private TMP_Text growHealthTxt;
    [SerializeField] private TMP_Text growStemenaTxt;
    [SerializeField] private TMP_Text growStrTxt;
    [SerializeField] private TMP_Text growDexTxt;
    [SerializeField] private TMP_Text growIntTxt;
    [SerializeField] private TMP_Text growLukTxt;



    private CharacterStats playerStat;

    private void Start()
    {
        playerStat = GameManager.Instance.playerStats;
    }
    private void Update()
    {
        UpdateStatus();

    }

    public void UpdateStatus()
    {
        levelTxt.text = $"LV.{playerStat.Level} ({(100*((float)playerStat.curExp / playerStat.maxExp)):F1}%)"; // ()�ȿ� {(���� ����ġ / ����ġ ��):F1}
        healthTxt.text = $"{playerStat.characterHp} / {playerStat.MaxHP}";
        steminaTxt.text = $"{(int)Math.Floor(playerStat.characterStamina)} / {playerStat.MaxStemina}";
        int equipWeight = 0;
        foreach(Item ew in Equipment.instance.equipItemList)
        {
            if(ew != null)
            equipWeight += ew.weight;
        }
        weightTxt.text = $"{equipWeight} / {playerStat.CharacterWeight}";
        speedTxt.text = $"{playerStat.CharacterSpeed:F1}";

        weaponTxt.text = $"[E] {Equipment.instance.equipItemList[0]?.itemName}";
        attackTxt.text = $"{playerStat.NormalAttackDamage}";
        skillTxt.text = $"{playerStat.NormalSkillDamage}";
        propertyTxt.text = $"{playerStat.PropertyDamage}";
        criticalTxt.text = $"{playerStat.critical:F1}%";
        attackSpeedTxt.text = $"{playerStat.AttackSpeed}";

        deffenceTxt.text = $"{playerStat.CharacterDefense}";
        parryTimeTxt.text = $"{playerStat.ParryTime:F2}";
        addGoodTxt.text = $"{playerStat.AddGoods}";

        growPoint.text = $"����Ʈ : {playerStat.Points}";
        growHealthTxt.text = $"ü�� {playerStat.MaxHP}({playerStat.GrowHP})";
        growStemenaTxt.text = $"���׹̳� {playerStat.MaxStemina}({playerStat.GrowStemina})";
        growStrTxt.text = $"�� ({playerStat.GrowStr})"; // ������ ���� ���� ���� �ʿ�
        growDexTxt.text = $"��ø ({playerStat.GrowDex})";
        growIntTxt.text = $"���� ({playerStat.GrowInt})";
        growLukTxt.text = $"�� ({playerStat.GrowLux})";
    }

    public void GrowStat(string statName)
    {
        playerStat.TryLevelUp(statName);
    }
    public void TogglePopup()
    {
        isOpen = !isOpen;
        growthPopup.SetActive(isOpen);
    }
}
