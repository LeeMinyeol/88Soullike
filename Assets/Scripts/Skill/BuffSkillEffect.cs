using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/SkillEft/BuffEft")]
public class BuffSkillEffect : SkillEffect
{
    //������ �ø� �÷��̾� ����, �нú�� �޸� ���� ���ݷ�, ��� ���� ���� �� ������ ȿ���� �ش�.
    [SerializeField] private string selectStat;
    public override bool ExcuteRole(int power, SkillType type)
    {
        switch (selectStat)
        {
            case "Attack":
                //GameManager.Instance.playerStats.NormalAttackDamage += power;
                break;
            case "Defense":
                //GameManager.Instance.playerStats.CharacterDefense += power;
                break;
        }


        //power�� ���� 
        return true;
    }
}
