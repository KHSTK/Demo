using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRock : MonoBehaviour, IInteractable
{
    [Header("广播事件")]
    public VoidEventSO SaveGameEvent;

    public SpriteRenderer spriteRenderer;
    public Sprite dark;
    public Sprite light;
    public bool isSave;
    private void OnEnable()
    {
        spriteRenderer.sprite = isSave ? light : dark;

    }
    public void TriggerAction()
    {
        if (!isSave)
        {
            Save();
            SaveGameEvent.RaiseEvent();
        }
    }
    public void Save()
    {
        spriteRenderer.sprite = light;
        isSave = true;
        //保存数据
        this.gameObject.tag = "Untagged";
    }
}
