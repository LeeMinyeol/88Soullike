using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/SkillEft/ActiveEft")]
public class ActiveSkillEffect : SkillEffect
{
    [SerializeField] private bool type; // false �ٰŸ�, true ���Ÿ�
    public override bool ExcuteRole(int power, SkillType type) // power�� ������� ���ظ� ��
    {
        // power��ŭ ���ظ� ������.
        // type�� ���� �Ӽ����ظ� ������.
        if (this.type)
        {
            // ������ ������
            // instantiate ������Ʈ ����
            // ������Ʈ ������ �̵��ϰ� �ϰ�
            // �ѹ� ������ �ı�
        }
        else
        {
            // ������ ���ư���
            // instantiate ������Ʈ ����
            // ������Ʈ ������ �̵��ϰ� �ϰ�
            // ��Ʈ���� ������ �ָ鼭 �ı��Ǳ�, �ð����� �ı�
        }


        return true;
    }
    void add()
    {

    }

    IEnumerator ShootArrowInArc()
    {
        yield return null;
    }

    IEnumerator ShootStraightArrow()
    {
        yield return null;
    }
}
