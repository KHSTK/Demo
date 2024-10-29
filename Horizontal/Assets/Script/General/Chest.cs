using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    private SpriteRenderer spriteRenderer;
    public Sprite openChest;
    public Sprite closeChest;
    bool isDone;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? openChest : closeChest;
    }
    public void TriggerAction()
    {
        Debug.Log("OpenChest");
        if (!isDone)
        {
            OpenChest();
        }
    }
    private void OpenChest()
    {
        spriteRenderer.sprite = openChest;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
