using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupUIHeader : MonoBehaviour, IBeginDragHandler, IDragHandler
{   //����κ��� �巡�� �� ������� �ű� �� �ְ� ��
    private RectTransform parentRect;
    private Vector2 rectBegin;
    private Vector2 moveBegin;
    private Vector2 moveOffset;

    private void Awake()
    {
        parentRect = transform.parent.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rectBegin = parentRect.anchoredPosition;
        moveBegin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveOffset = eventData.position - moveBegin;
        parentRect.anchoredPosition = rectBegin + moveOffset;
    }
}
