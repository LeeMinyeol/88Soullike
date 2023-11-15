using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/SkillEft/ActiveEft")]
public class ActiveSkillEffect : SkillEffect
{
    [SerializeField] private bool type; // false �ٰŸ�, true ���Ÿ�
    public override bool ExcuteRole(int power, SkillType type) // power�� ������� ���ظ� ��
    {
        // power��ŭ ���ظ� ������.
        // type�� ���� �Ӽ����ظ� ������.
        return true;
    }
}
