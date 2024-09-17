using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    //�����Ƿ񱻹ر�
    protected bool isRemove = false;
    //��������
    protected new string name;
    protected virtual void Awake()
    {
        
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    //�򿪽���
    public virtual void OpenPanel(string name)
    {
        if (name == null)
        {
            Debug.LogError("Panel name cannot be null.");
            return;
        }
        this.name = name;
        SetActive(true);
    }
    //�رս���
    public virtual void ClosePanel()
    {
        if (isRemove)
        {
            Debug.LogWarning("Panel is already marked for removal: " + name);
            return;
        }
        isRemove = true;
        SetActive(false);
        if (UIManager.Instance != null && UIManager.Instance.panelDict != null && UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
        else
        {
            Debug.LogWarning("Panel not found in UIManager or UIManager is null: " + name);
        }
        Destroy(gameObject);
    }
}
