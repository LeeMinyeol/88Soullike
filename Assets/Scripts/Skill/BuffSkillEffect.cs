using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/SkillEft/BuffEft")]
public class BuffSkillEffect : SkillEffect
{
    //������ �ø� �÷��̾� ����, �нú�� �޸� ���� ���ݷ�, ��� ���� ���� �� ������ ȿ���� �ش�.
    [SerializeField] private Transform buffPrefab;

    private PlayerBuff pb;

    private void Awake()
    {
        //ps = GameManager.Instance.playerStats;
    }
    public override bool ExcuteRole(int power, SkillType type) //�������� ȿ��, �̴�θ� power�� ������� �ѵ�..
    {
        // PlayerBuff.Instance�� ���߿� PlayerStat���� ������
        // PlayerBuff.Instance.AddBuff(curBuff); // Buff - BuffSO - StatName�� ���� ȿ���� �޶���
        var buff = Instantiate(buffPrefab);
        pb = GameManager.Instance.player.GetComponent<PlayerBuff>();
        pb.AddBuff(buff.GetComponent<Buff>());
        return true;
    }
}




