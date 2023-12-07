using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int slotnum;
    public TMP_Text amountTxt;
    public Item item;
    public Image itemIcon;
    [SerializeField] private string descriptions;

    FullScreenUIManager fManager;

    void Start()
    {
        Init();
    }

    void Init()
    {
        itemIcon.gameObject.SetActive(true);
        amountTxt.text = "";

        if (item == null) return;

        itemIcon.sprite = item.Sprite;
        itemIcon.transform.localScale = Vector3.one * 0.9f;
        
    }

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.Sprite;
        itemIcon.transform.localScale = Vector3.one * 0.9f;
        itemIcon.gameObject.SetActive(true);

        if (item.CurItem.IsStackable())
        {
            amountTxt.text = item.Amount.ToString();
        }
        else
        {
            amountTxt.text = "";
            
        }

    }
    public void RemoveSlot()
    {
        item = null;
        amountTxt.text = "";
        itemIcon.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData) // ������ ����
    {
        if (item == null)
        {
            return;
        }
        else
        {
            if (item.CurItem == null) return;
        }

        fManager.itemSelectUI.gameObject.SetActive(true);

        for (int i = 0; i < item.Description.Count; i++)
        {
            descriptions += $"{item.Description[i]}\n";

        }
        //InventoryUI.instance.usePanel.GetComponent<UsePopup>().SetPopup(item.ItemName, descriptions, slotnum);
        // ������ ����
        //fManager.itemSelectUI.
        ShowItemInformation();
        //fManager.itemSelectUI.SetFunc(Action ddd);
    }

    public void ApplyUse()
    {
        if (item == null) return;

        bool isUse = item.Use(); //������ ȿ�� ���

        if (isUse && (item.Amount <= 0)) // ��� ���Ǹ� ������ ������ �ʱ�Ȱ
        {
            Inventory.instance.RemoveItem(slotnum);
        }
        else
        {
            UpdateSlotUI();
        }
    }

    void ShowItemInformation()
    {
        Item selectedItem = fManager.itemInformationUI.GetComponent<FItemInformationUI>().selectedItem;
        selectedItem = item;
        selectedItem.Init();
    }


}
