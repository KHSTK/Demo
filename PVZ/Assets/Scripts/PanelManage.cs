using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelManage
{
    // 单例模式的实例
    private static PanelManage _instance;
    // 用户界面的根节点
    private Transform _uiRoot;
    // 预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    // 已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;

    // 获取单例模式的实例
    public static PanelManage Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PanelManage();
            }
            return _instance;
        }
    }

    // 获取用户界面的根节点
    public Transform UIRoot
    {
        get
        {
            if (_uiRoot == null)
            {
                if (GameObject.Find("Canvas"))
                {
                    _uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    _uiRoot = new GameObject("Canvas").transform;
                }
            };
            return _uiRoot;
        }
    }

    // 用户界面管理器的构造函数
    private PanelManage()
    {
        InitDicts();
    }

    // 初始化字典
    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
    }

    // 打开面板
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // 检查是否已打开
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面已打开: " + name);
            return null;
        }

        // 检查路径是否配置
        string path = name;

        // 使用缓存预制件
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/Panel/" + path;
            Debug.Log(realPath);
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;

            prefabDict.Add(name, panelPrefab);
        }

        // 打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    // 关闭面板
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("界面未打开: " + name);
            return false;
        }

        panel.ClosePanel();
        // panelDict.Remove(name);
        return true;
    }
}
