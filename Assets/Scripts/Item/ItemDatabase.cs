using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }
    public List<ItemSO> itemDB = new List<ItemSO>();
    public List<Transform> BoxTr = new List<Transform>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        Item fieldItem = new Item();

        foreach(Transform btr in BoxTr)
        {
            fieldItem.CurItem = itemDB[Random.Range(0, itemDB.Count)];
            fieldItem.Init();

            GameObject go = Instantiate(fieldItemPrefab, btr.position, Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(fieldItem);
        }
    }
    // �ڽ��� ���Ϳ� ������ ������ �����ϴ� �Լ�
    // �ڽ��� ���� �ı�, óġ�ÿ� ���õ� �������� ������ �Լ�, ������ �ִϸ��̼� �ʿ�
    // �ڽ�.cs  -> �������� �� ������ or ��� ������ �����, ������鼭 ������ �ִϸ��̼�, �������ڷ� �̹��� ����
    // �������ڿ� �÷��̾ ������ ������ ȹ��(�ٷ� ȹ������?, ��� ȹ���ߴٰ� �ؽ�Ʈ�� �˷�����?, ������� ����Ʈ�� ����� ������ �������� ȹ���ϰ� ����?)
}
