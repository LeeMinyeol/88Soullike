using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenUIManager : MonoBehaviour
{
    public static FullScreenUIManager instance;

    [SerializeField] private Image menuIcon;
    [SerializeField] private TMP_Text menuName;
    [SerializeField] private GameObject fullScreenBase;

    public List<FullScreenUI> currentUI; // ���� ���� UIList Ȯ�� => �����ܰ� �޴� �̸� �������ֱ� ����?

    [Space]
    public KeyCode escapeKey = KeyCode.Escape;
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode equipmentKey = KeyCode.E;
    public KeyCode charInfoKey = KeyCode.C;
    public KeyCode npcKey = KeyCode.F;
    public KeyCode mapKey = KeyCode.M;

    [Header("UI���")]
    [SerializeField] private FullScreenUI mainStatusUI;
    [SerializeField] private FullScreenUI basicStatusUI;
    [SerializeField] private FullScreenUI abillityStatusUI;
    [SerializeField] private FullScreenUI playerImageUI;
    [SerializeField] private FullScreenUI inventoryUI;
    [SerializeField] private FullScreenUI itemInformationUI;
    [SerializeField] private FullScreenUI equipmentUI;
    [SerializeField] private FullScreenUI shopUI;
    [SerializeField] private FullScreenUI levelUpUI;
    [SerializeField] private FullScreenUI optionUI;
    [SerializeField] private FullScreenUI mapUI; //
    [SerializeField] private FullScreenUI travelUI; //

    [Header("UI �׷�")] // �������� UI�� �־�ΰ� 0, 1, 2�� ó���� ���� ... ǥ����ȯ ������ n��°�� ���� �����ִ°� �� �����Ÿ� �θ�?
    [SerializeField] private List<FullScreenUI> statusList;
    [SerializeField] private List<FullScreenUI> inventoryList;
    [SerializeField] private List<FullScreenUI> equipmentList;
    [SerializeField] private List<FullScreenUI> shopList;
    [SerializeField] private List<FullScreenUI> levelUpList;
    [SerializeField] private List<FullScreenUI> mapList;

    public LinkedList<FullScreenUI> activeFullScreenUILList;

    // ��ü UI ���
    private List<FullScreenUI> allFullScreenUIList;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        } 

        Init();
        InitCloseAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(escapeKey))
        {
            if (activeFullScreenUILList.Count > 0)
            {
                CloseUI(activeFullScreenUILList.First.Value);
            }
            else
            {
                if (optionUI.gameObject.activeSelf)
                {
                    optionUI.gameObject.SetActive(true);
                }
                else
                {
                    optionUI.gameObject.SetActive(true);
                }
            }
        }

        ToggleKeyDownAction(inventoryKey, inventoryList);
        ToggleKeyDownAction(equipmentKey, equipmentList);
        ToggleKeyDownAction(charInfoKey, statusList);
        ToggleKeyDownAction(mapKey, mapList);

        if ((Inventory.instance.currentNPC != null) && (Inventory.instance.currentNPC.isInteractable)) //inventory.instance.currentNPC�� Player.currentNPC�� ���� ����
        {
            if (Inventory.instance.currentNPC.npcName.Equals("Shop"))
            {
                ToggleKeyDownAction(npcKey, shopList);
            }
        }
    }

    private void Init()
    {
        allFullScreenUIList = new List<FullScreenUI>() // ����Ʈ �ʱ�ȭ
        {
            mainStatusUI, basicStatusUI, abillityStatusUI, playerImageUI, inventoryUI, itemInformationUI, equipmentUI, shopUI, levelUpUI
        };
        statusList = new List<FullScreenUI>() {mainStatusUI, basicStatusUI, playerImageUI, abillityStatusUI };
        inventoryList = new List<FullScreenUI>() { inventoryUI, itemInformationUI, mainStatusUI, basicStatusUI, abillityStatusUI };
        equipmentList = new List<FullScreenUI>() { equipmentUI, itemInformationUI, mainStatusUI, basicStatusUI, abillityStatusUI };
        shopList = new List<FullScreenUI>() {shopUI, itemInformationUI, mainStatusUI, basicStatusUI, abillityStatusUI };
        levelUpList = new List<FullScreenUI>() { levelUpUI, basicStatusUI, abillityStatusUI };
        mapList = new List<FullScreenUI>() { mapUI, travelUI};

    }

    private void InitCloseAll() // ���۽� ��� �˾� �ݱ�
    {
        foreach (FullScreenUI fScreen in allFullScreenUIList)
        {
            CloseUI(fScreen);
        }
    }

    // ����Ű �Է¿� ���� �˾� ���ų� �ݱ�
    private void ToggleKeyDownAction(in KeyCode key, List<FullScreenUI> fScreens)
    {
        if (Input.GetKeyDown(key))
        {
            ToggleOpenCloseUIList(fScreens);
        }
    }

    // �˾��� ���¿� ���� ���ų� �ݱ�(opened / closed)
    private void ToggleOpenCloseUI(FullScreenUI fScreen)
    {
        if (!fScreen.gameObject.activeSelf)
        {
            OpenUI(fScreen);
        }
        else
        {
            CloseUI(fScreen);
        }
    }

    private void ToggleOpenCloseUIList(List<FullScreenUI> fScreens)
    {
        if (fScreens == null) return;

        foreach(FullScreenUI fScreen in fScreens)
        {
            if (!fScreen.gameObject.activeSelf)
            {
                OpenUI(fScreen);
            }
            else
            {
                CloseUI(fScreen);
            }
        }
    }

    // �˾��� ���� ��ũ�帮��Ʈ�� ��ܿ� �߰�
    private void OpenUI(FullScreenUI fScreen)
    {
        fullScreenBase.SetActive(true);
        activeFullScreenUILList.AddFirst(fScreen);
        fScreen.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }
    private void OpenUIList(List<FullScreenUI> fScreens)
    {
        if (fScreens == null) return;

        fullScreenBase.SetActive(true);
        foreach (FullScreenUI fScreen in fScreens)
        {
            activeFullScreenUILList.AddFirst(fScreen);
            fScreen.gameObject.SetActive(true);
        }
        RefreshAllPopupDepth();
    }

    // �˾��� �ݰ� ��ũ�帮��Ʈ���� ����
    private void CloseUI(FullScreenUI fScreen)
    {
        fullScreenBase.SetActive(false);
        activeFullScreenUILList.Remove(fScreen);
        fScreen.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }
    private void CloseUIList(List<FullScreenUI> fScreens)
    {
        if (fScreens == null) return;

        fullScreenBase.SetActive(false);
        foreach (FullScreenUI fScreen in fScreens)
        {
            activeFullScreenUILList.Remove(fScreen);
            fScreen.gameObject.SetActive(false);
        }
        RefreshAllPopupDepth();
    }

    //��ũ�帮��Ʈ �� ��� �˾��� �ڽ� ���� ���ġ
    private void RefreshAllPopupDepth()
    {
        foreach (FullScreenUI fScreen in activeFullScreenUILList)
        {
            fScreen.transform.SetAsFirstSibling();
        }
    }

}