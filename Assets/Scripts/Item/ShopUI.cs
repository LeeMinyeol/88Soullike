using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public ShopSlot[] slots;
    public Transform slotHolder;
    public GameObject slotPrefab;
    ItemDatabase itmDB;

    private void Start()
    {
        itmDB = ItemDatabase.instance;
        slots = slotHolder.GetComponentsInChildren<ShopSlot>();
        SetShop();

    }

    void SetShop() // ù �����̳� é�� �� �� ���� ������ ����
    {
        
        foreach(Transform tr in slotHolder) // �ʱ�ȭ
        {
            Destroy(tr.gameObject);
        }

        for (int i = 0; i < itmDB.itemDB.Count; i++)
        {
            if (!itmDB.itemDB[i].Buyable()) continue;
            Item newItem = new Item();
            newItem.CurItem = itmDB.itemDB[i];
            newItem.Init();

            GameObject go = Instantiate(slotPrefab, slotHolder);
            go.GetComponent<ShopSlot>().SetItem(newItem);

        }
    }

}
