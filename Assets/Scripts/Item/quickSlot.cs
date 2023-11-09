using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class quickSlot : MonoBehaviour
{
    public KeyCode slotKey;
    public Item slotItem;
    public Image itemIcon;
    public int slotIndex; // �������� �ε���
    public int invenIndex; // ����� �κ��丮 ������ �ε���

    public void SetQuickSlotItem(int slotnum) // �κ��丮 �������� ���� �ѹ��� �̹����� �����´�. ���� �����ۿ��� �κ��丮�� ���� ��ȣ�� �������� ����
    {
        invenIndex = slotnum;
        slotItem = InventoryUI.instance.slots[slotnum].item;

        itemIcon.sprite = slotItem.sprite;
        itemIcon.transform.localScale = Vector3.one * 0.4f;
        itemIcon.gameObject.SetActive(true);

        Equipment.instance.quickSlotList[slotIndex] = slotItem;
    }

    public void UseQuickSlotItem() //Ű �ԷµǸ� EquipmentUI.quickSlots[Ű �Է¿� �ش��ϴ� ����].UseQuickSlotItem();
    {
        InventoryUI.instance.slots[invenIndex].ApplyUse();
    }

    public void DisplaySettableItem() //�κ��丮�� �Ҹ�ǰ�� ��� ������ / ��ũ�Ѻ�
    {
        EquipmentUI.instance.setSlotIndex = slotIndex;
        EquipmentUI.instance.settableListPanel.SetActive(true);
        EquipmentUI.instance.DrawquickSlot();
    }

    // ������ ��ư�� ������ ������ �ʱ�ȭ�ϰ� ������ �Ҵ�â�� Ȱ��ȭ
    // ������ �Ҵ�â���� �� �κ��丮 �� �Ҹ�ǰ�鸸 ������
    // �Ҵ�â���� ������ ������ ������ ó�� ���� �����Կ� �� �������� �Ҵ�
    // �����Կ� �ش��ϴ� ���ڸ� ������(���ӸŴ��� ������Ʈ) ������ ���(InventoryUI.instance.Slot.ApplyUse())
}
