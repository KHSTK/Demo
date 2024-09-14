using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BasePanel : MonoBehaviour  // 基础面板类，继承自MonoBehaviour
{
    protected bool isRemove = false;  // 是否移除，默认为否
    protected new string name;  // 面板名称
    public virtual void SetActive(bool active)  // 设置面板活动状态
    {
        gameObject.SetActive(active);  // 设置游戏对象活动状态
    }

    public virtual void OpenPanel(string name)  // 打开面板方法
    {
        this.name = name;  // 设置面板名称
        SetActive(true);  // 设置面板为活动状态
    }

    public virtual void ClosePanel()  // 关闭面板方法
    {
        isRemove = true;  // 设置移除状态为是
        SetActive(false);  // 设置面板为非活动状态
        Destroy(gameObject);  // 销毁游戏对象
        if (PanelManage.Instance.panelDict.ContainsKey(name))  // 如果UI管理器的面板字典包含该面板
        {
            PanelManage.Instance.panelDict.Remove(name);  // 从字典中移除该面板
        }
    }
}

