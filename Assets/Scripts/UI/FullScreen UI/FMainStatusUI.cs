using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FMainStatusUI : FStatus
{
    [Header("�⺻�ɷ�")]
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text levelValue;
    [SerializeField] private TMP_Text haveSoulValue;
    [SerializeField] private TMP_Text needSoulValue;
    [SerializeField] private TMP_Text healthStatValue;
    [SerializeField] private TMP_Text steminaStatValue;
    [SerializeField] private TMP_Text strStatValue;
    [SerializeField] private TMP_Text dexStatValue;
    [SerializeField] private TMP_Text intStatValue;
    [SerializeField] private TMP_Text luxStatValue;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        playerName.text = "���";
        levelValue.text = $"{playerMaxStat.level}";
        haveSoulValue.text = $"{inven.SoulCount}";
        //needSoulValue.text = $"{}"; => ���� growStat�� �󸶳� �÷ȳĿ� ���� �޶���, ���� ����(100�� * ���?) �ѹ� �����Ҷ����� 10�ۼ�Ʈ�� ���?
        healthStatValue.text = $"{playerBaseStat.healthStat + playerGrowStat.healthStat}";
        steminaStatValue.text = $"{playerBaseStat.steminaStat + playerGrowStat.steminaStat}";
        strStatValue.text = $"{playerBaseStat.strStat + playerGrowStat.strStat}";
        dexStatValue.text = $"{playerBaseStat.dexStat + playerGrowStat.dexStat}";
        intStatValue.text = $"{playerBaseStat.intStat + playerGrowStat.intStat}";
        luxStatValue.text = $"{playerBaseStat.luxStat + playerGrowStat.luxStat}";
    }
}
