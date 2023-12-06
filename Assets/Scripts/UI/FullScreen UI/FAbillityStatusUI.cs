using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FAbillityStatusUI : FStatus
{
    [Header("���ɷ�")]
    [SerializeField] private TMP_Text deffenseValue;
    [SerializeField] private TMP_Text propertyDeffenseValue;
    [SerializeField] private TMP_Text parryTimeValue;

    [Header("Ư���ɷ�")]
    [SerializeField] private TMP_Text soulDropValue;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        deffenseValue.text = $"{playerStatusHandler.currentHp} / {playerMaxStat.hp}";
        propertyDeffenseValue.text = $"{playerStatusHandler.currentHp} / {playerMaxStat.hp}";
        parryTimeValue.text = $"{playerStatusHandler.currentHp} / {playerMaxStat.hp}";


    }
}
