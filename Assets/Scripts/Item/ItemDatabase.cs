using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }
    public List<ItemSO> itemDB = new List<ItemSO>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
        }
    }
    // �ڽ��� ���Ϳ� ������ ������ �����ϴ� �Լ�
    // �ڽ��� ���� �ı�, óġ�ÿ� ���õ� �������� ������ �Լ�, ������ �ִϸ��̼� �ʿ�
    // �ڽ�.cs  -> �������� �� ������ or ��� ������ �����, ������鼭 ������ �ִϸ��̼�, �������ڷ� �̹��� ����
    // �������ڿ� �÷��̾ ������ ������ ȹ��(�ٷ� ȹ������?, ��� ȹ���ߴٰ� �ؽ�Ʈ�� �˷�����?, ������� ����Ʈ�� ����� ������ �������� ȹ���ϰ� ����?)
}
