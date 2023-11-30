using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Scriptable/SkillEft/ActiveEft")]
public class ActiveSkillEffect : SkillEffect
{
    [SerializeField] private bool type; // false �ٰŸ�, true ���Ÿ�
    public GameObject skillPrefab; // false �ٰŸ�, true ���Ÿ�
    public Vector3 skillPosition;
    public override bool ExcuteRole(int power, SkillType type) // power�� ������� ���ظ� ��
    {
        // power��ŭ ���ظ� ������.
        // type�� ���� r�ٰŸ�/ ���Ÿ� ����
        GameObject go = Instantiate(skillPrefab, skillPosition, Quaternion.identity);
        if (this.type)
        {
            
           
           
            // ������ ������
            // instantiate ������Ʈ ����
            // �ѹ� ������ �ı�
        }
        else
        {
            // instantiate ������Ʈ ����
            // ������ ���ư���
            // ������Ʈ ������ �̵��ϰ� �ϰ�
            // ��Ʈ���� ������ �ָ鼭 �ı��Ǳ�, �ð����� �ı�
        }


        return true;
    }

}
