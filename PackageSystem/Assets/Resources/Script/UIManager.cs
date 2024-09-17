using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;
    //挂载节点为根节点
    private Transform _uiRoot;
    //路径配置字典
    private Dictionary<string, string> pathDict;
    //预制件缓存字典
    private Dictionary<string, GameObject> prefabDict;
    //已打开的界面缓存字典
    public Dictionary<string,BasePanel> panelDict;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    public Transform UIRoot
    {
        get
        {
            //根节点为空取Canvas为根节点
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
            }
            return _uiRoot;
        }
    }
    private UIManager()
    {
        //初始化字典
        InitDicts();
    }
    //初始化字典
    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
        //配置路径
        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PackagePanel,"Package/PackagePanel" }
        };

    }
    public BasePanel GetPanel(string name)
    {
        BasePanel panel = null;
        //是否打开
        if(panelDict.TryGetValue(name,out panel))
        {
            return panel;
        }
        return null;

    }
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        if(panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("界面已打开"+name);
            return null;
        }
        //路径是否配置
        string path = "";
        if (!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("界面名称错误或者路径未配置" + name);
            return null;
        }
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefab/Panel/" + path;

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
public class UIConst
{
    // menu panels

    public const string PackagePanel = "PackagePanel";
}
