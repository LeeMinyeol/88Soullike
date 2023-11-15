using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager : MonoBehaviour
{
    public static PopupUIManager instance;
    
    public PopupUI inventoryPopup;
    public PopupUI equipmentPopup;
    public PopupUI characterInfoPopup;
    public PopupUI shopPopup;
    public PopupUI usePopup;
    public PopupUI mapPopup;
    public GameObject optionPopup;
    

    [Space]
    public KeyCode escapeKey = KeyCode.Escape;
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode equipmentKey = KeyCode.E;
    public KeyCode charInfoKey = KeyCode.C;
    public KeyCode npcKey = KeyCode.X;
    public KeyCode mapKey = KeyCode.M;

    // �ǽð� �˾� ���� ��ũ�� ����Ʈ
    public LinkedList<PopupUI> activePopupLList;

    // ��ü �˾� ���
    private List<PopupUI> allPopupList;

    private void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        #endregion

        activePopupLList = new LinkedList<PopupUI>();
        Init();
        InitCloseAll();
    }

    private void Update()
    {
        if(Input.GetKeyDown(escapeKey))
        {
            if(activePopupLList.Count > 0)
            {
                ClosePopup(activePopupLList.First.Value);
            } else
            {
                if(optionPopup.activeSelf)
                {
                    optionPopup.SetActive(false);
                }
                else
                {
                    optionPopup.SetActive(true);
                }
            }
        }

        ToggleKeyDownAction(inventoryKey, inventoryPopup);
        ToggleKeyDownAction(equipmentKey, equipmentPopup);
        ToggleKeyDownAction(charInfoKey, characterInfoPopup);
        ToggleKeyDownAction(mapKey, mapPopup);

        if ((Inventory.instance.currentNPC != null) && (Inventory.instance.currentNPC.isInteractable)) //inventory.instance.currentNPC�� Player.currentNPC�� ���� ����
        {
            if(Inventory.instance.currentNPC.npcName.Equals("Shop"))
            {
                ToggleKeyDownAction(npcKey, shopPopup);
            }
            if(Inventory.instance.currentNPC.npcName.Equals("Stat"))
            {
                CharacterInfoUI.instance.growPopupBtn.SetActive(true);
            }
        }
    }

    private void Init()
    {
        allPopupList = new List<PopupUI>() // ����Ʈ �ʱ�ȭ
        {
            inventoryPopup, equipmentPopup, characterInfoPopup, mapPopup, shopPopup, usePopup
        };

        foreach (PopupUI popup in allPopupList) // ��� �˾��� �̺�Ʈ ���
        {
            popup.OnFocus += () => //��� ��Ŀ�� �̺�Ʈ
            { 
                activePopupLList.Remove(popup);
                activePopupLList.AddFirst(popup);
                RefreshAllPopupDepth();
            };

            popup.closeButton.onClick.AddListener(() => ClosePopup(popup)); // �ݱ��ư �̺�Ʈ
        }
    }

    private void InitCloseAll() // ���۽� ��� �˾� �ݱ�
    {
        foreach (PopupUI popup in allPopupList)
        {
            ClosePopup(popup);
        }
    }

    // ����Ű �Է¿� ���� �˾� ���ų� �ݱ�
    private void ToggleKeyDownAction(in KeyCode key, PopupUI popup)
    {
        if(Input.GetKeyDown(key))
        {
            ToggleOpenClosePopup(popup);
        }
    }

    // �˾��� ���¿� ���� ���ų� �ݱ�(opened / closed)
    private void ToggleOpenClosePopup(PopupUI popup)
    {
        if(!popup.gameObject.activeSelf)
        {
            OpenPopup(popup);
        } else
        {
            ClosePopup(popup);
        }
    }

    // �˾��� ���� ��ũ�帮��Ʈ�� ��ܿ� �߰�
    private void OpenPopup(PopupUI popup)
    {
        activePopupLList.AddFirst(popup);
        popup.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }

    // �˾��� �ݰ� ��ũ�帮��Ʈ���� ����
    private void ClosePopup(PopupUI popup)
    {
        activePopupLList.Remove(popup);
        popup.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }

    //��ũ�帮��Ʈ �� ��� �˾��� �ڽ� ���� ���ġ
    private void RefreshAllPopupDepth()
    {
        foreach(PopupUI popup in activePopupLList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }

}
