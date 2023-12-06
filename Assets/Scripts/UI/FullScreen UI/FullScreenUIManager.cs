using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenUIManager : MonoBehaviour
{
    public static FullScreenUIManager instance;

    [SerializeField] private Image menuIcon;
    [SerializeField] private List<Sprite> menuIconList;
    [SerializeField] private TMP_Text menuName;
    [SerializeField] private GameObject fullScreenBase;

    public List<FullScreenUI> currentUI; // ���� ���� UIList Ȯ�� => �����ܰ� �޴� �̸� �������ֱ� ����?
    public int uiIndex;

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

    [SerializeField] private LinkedList<FullScreenUI> activeFullScreenUILList;

    // ��ü UI ���
    [SerializeField] private List<FullScreenUI> allFullScreenUIList;
    [SerializeField] private Vector2 leftPanelPosition;
    [SerializeField] private Vector2 centerPanelPosition;
    [SerializeField] private Vector2 rightPanelPosition;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        activeFullScreenUILList = new LinkedList<FullScreenUI>();
        //menuIconList = new List<Sprite>(); Resources/,...

        Init();
        InitCloseAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(escapeKey))
        {
            if (activeFullScreenUILList.Count > 0)
            {
                CloseUIList(allFullScreenUIList);
            }
            else
            {
                if (optionUI.gameObject.activeSelf)
                {
                    optionUI.gameObject.SetActive(false);
                }
                else
                {
                    optionUI.gameObject.SetActive(true);
                }
            }
        }
        if(activeFullScreenUILList.Count == 0) // �ݱ�� esc�� ���� List�� �ߺ��� �г��� �����ϱ� ����
        {
            ToggleKeyDownAction(inventoryKey, inventoryList);
            ToggleKeyDownAction(equipmentKey, equipmentList);
            ToggleKeyDownAction(charInfoKey, statusList);
            ToggleKeyDownAction(mapKey, mapList);
        }


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
        statusList = new List<FullScreenUI>() { abillityStatusUI,playerImageUI, basicStatusUI, mainStatusUI };
        inventoryList = new List<FullScreenUI>() { abillityStatusUI,basicStatusUI, mainStatusUI, itemInformationUI, inventoryUI };
        equipmentList = new List<FullScreenUI>() { abillityStatusUI,basicStatusUI, mainStatusUI, itemInformationUI, equipmentUI };
        shopList = new List<FullScreenUI>() { abillityStatusUI,basicStatusUI, mainStatusUI, itemInformationUI, shopUI };
        levelUpList = new List<FullScreenUI>() { abillityStatusUI,basicStatusUI, levelUpUI };
        mapList = new List<FullScreenUI>() { travelUI, mapUI };


        foreach (FullScreenUI fscreen in allFullScreenUIList) // ��� �˾��� �̺�Ʈ ���
        {
            fscreen.OnFocus += () => //��� ��Ŀ�� �̺�Ʈ
            {
                activeFullScreenUILList.Remove(fscreen);
                activeFullScreenUILList.AddFirst(fscreen);
                RefreshAllPopupDepth();
            };
        }

    }

    private void InitCloseAll() // ���۽� ��� �˾� �ݱ�
    {
        CloseUIList(allFullScreenUIList);
        /*foreach (FullScreenUI fScreen in allFullScreenUIList)
        {
            CloseUI(fScreen);
        }*/
        //fullScreenBase.SetActive(false);
    }

    // ����Ű �Է¿� ���� �˾� ���ų� �ݱ�
    private void ToggleKeyDownAction(in KeyCode key, List<FullScreenUI> fScreens)
    {
        

        if (Input.GetKeyDown(key))
        {
            ToggleOpenCloseUIList(fScreens);

            if(fScreens != null) SetBase(key);

        }

        if(activeFullScreenUILList.Count > 0)
        {
            fullScreenBase.gameObject.SetActive(true);
        } else
        {
            fullScreenBase.gameObject.SetActive(false);
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
        activeFullScreenUILList.AddFirst(fScreen);
        fScreen.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }
    private void OpenUIList(List<FullScreenUI> fScreens)
    {
        if (fScreens == null) return;

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
        activeFullScreenUILList.Remove(fScreen);
        fScreen.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }
    private void CloseUIList(List<FullScreenUI> fScreens)
    {
        if (fScreens == null) return;

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
    void SetBase(in KeyCode key)
    {
        switch(key)
        {
            case KeyCode.C:
                SetBaseInform("�������ͽ�", menuIconList[0]);
                break;
            case KeyCode.I:
                SetBaseInform("�κ��丮", menuIconList[1]);
                break;
            case KeyCode.E:
                SetBaseInform("���", menuIconList[2]);
                break;
            default:
                break;
        }

    }
    void SetBaseInform(string name, Sprite icon)
    {
        menuName.text = name;
        menuIcon.sprite = icon;
    }
}