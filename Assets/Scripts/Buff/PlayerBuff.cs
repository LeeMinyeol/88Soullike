using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    // �÷��̾� ���ȿ� �ٿ��� �����ý��� ���� ����
    
    public static PlayerBuff Instance;
    //[SerializeField] private List<Buff> buffs = new List<Buff>();
    [SerializeField] private Dictionary<Buff, bool> buffs = new Dictionary<Buff, bool>();

    public void AddBuff(Buff buff)
    {
        if (!buffs.ContainsKey(buff))
        {
            buffs.Add(buff, true); // activated this, ()
            buff.Activated(transform.GetComponent<CharacterStats>(), () =>
            {
                buffs.Remove(buff);
                Destroy(buff.gameObject);
                Debug.Log(buff.buff.name + " : ���� ���ŵ�");

            });
            Debug.Log(buff.buff.name + " : ���� ����");
        } else
        {
            //���� ���� => amount�� �����ٰ��ΰ�? Item.Use �κп��� ���ƾ��ϳ�?
        }

        /*if (!buffs.Contains(buff))
        {
            buffs.Add(buff); // activated this, ()
            buff.Activated(transform.GetComponent<CharacterStats>(), () =>
            {
                buffs.Remove(buff);
                Destroy(buff.gameObject);
                Debug.Log(buff.buff.name + " : ���� ���ŵ�");

            });
        }
        Debug.Log(buff.buff.name + " : ���� ����");*/
    }
    
}
