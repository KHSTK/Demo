using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 当鼠标进入UI物体时触发的事件
    public event EventHandler OnMouseEnter;

    // 当鼠标离开UI物体时触发的事件
    public event EventHandler OnMouseExit;

    // 当鼠标进入UI物体时调用该方法
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 如果有订阅事件的方法存在，则触发鼠标进入事件
        OnMouseEnter?.Invoke(this, EventArgs.Empty);
    }

    // 当鼠标离开UI物体时调用该方法
    public void OnPointerExit(PointerEventData eventData)
    {
        // 如果有订阅事件的方法存在，则触发鼠标离开事件
        OnMouseExit?.Invoke(this, EventArgs.Empty);
    }
}
