using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��������UI����ʱ�������¼�
    public event EventHandler OnMouseEnter;

    // ������뿪UI����ʱ�������¼�
    public event EventHandler OnMouseExit;

    // ��������UI����ʱ���ø÷���
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ����ж����¼��ķ������ڣ��򴥷��������¼�
        OnMouseEnter?.Invoke(this, EventArgs.Empty);
    }

    // ������뿪UI����ʱ���ø÷���
    public void OnPointerExit(PointerEventData eventData)
    {
        // ����ж����¼��ķ������ڣ��򴥷�����뿪�¼�
        OnMouseExit?.Invoke(this, EventArgs.Empty);
    }
}
