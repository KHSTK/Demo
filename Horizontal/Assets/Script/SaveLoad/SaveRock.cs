using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRock : MonoBehaviour, IInteractable
{
    [Header("�㲥�¼�")]
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
        //��������
        this.gameObject.tag = "Untagged";
    }
}
