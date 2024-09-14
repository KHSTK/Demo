using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelManage
{
    // ����ģʽ��ʵ��
    private static PanelManage _instance;
    // �û�����ĸ��ڵ�
    private Transform _uiRoot;
    // Ԥ�Ƽ������ֵ�
    private Dictionary<string, GameObject> prefabDict;
    // �Ѵ򿪽���Ļ����ֵ�
    public Dictionary<string, BasePanel> panelDict;

    // ��ȡ����ģʽ��ʵ��
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

    // ��ȡ�û�����ĸ��ڵ�
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

    // �û�����������Ĺ��캯��
    private PanelManage()
    {
        InitDicts();
    }

    // ��ʼ���ֵ�
    private void InitDicts()
    {
        prefabDict = new Dictionary<string, GameObject>();
        panelDict = new Dictionary<string, BasePanel>();
    }

    // �����
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        // ����Ƿ��Ѵ�
        if (panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("�����Ѵ�: " + name);
            return null;
        }

        // ���·���Ƿ�����
        string path = name;

        // ʹ�û���Ԥ�Ƽ�
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefabs/Panel/" + path;
            Debug.Log(realPath);
            panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;

            prefabDict.Add(name, panelPrefab);
        }

        // �򿪽���
        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    // �ر����
    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(name, out panel))
        {
            Debug.Log("����δ��: " + name);
            return false;
        }

        panel.ClosePanel();
        // panelDict.Remove(name);
        return true;
    }
}
