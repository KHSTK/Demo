using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRock : MonoBehaviour, IInteractable
{
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
        }
    }
    public void Save()
    {
        spriteRenderer.sprite = light;
        isSave = true;
        //±£´æÊý¾Ý
        this.gameObject.tag = "Untagged";
    }
}
