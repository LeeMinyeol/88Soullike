using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenUI : MonoBehaviour
{
    int uiIndex; // uiIndex�� ���� ���� ���� UI�� �ε��� ���÷� ó�� 3��¥�� UI�� �����Ǹ� uiIndex�� 3�̰� ���� 3��° UI�� �߰��ϴ� ����

    public void ChangeIndex(int index) // index�� List���� �ٲ� UI, SwitchPanel.cs ����鼭 �ٽ� �� ��
    {

        uiIndex++;
    }  
}
