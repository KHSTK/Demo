using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BasePanel : MonoBehaviour  // ��������࣬�̳���MonoBehaviour
{
    protected bool isRemove = false;  // �Ƿ��Ƴ���Ĭ��Ϊ��
    protected new string name;  // �������
    public virtual void SetActive(bool active)  // �������״̬
    {
        gameObject.SetActive(active);  // ������Ϸ����״̬
    }

    public virtual void OpenPanel(string name)  // ����巽��
    {
        this.name = name;  // �����������
        SetActive(true);  // �������Ϊ�״̬
    }

    public virtual void ClosePanel()  // �ر���巽��
    {
        isRemove = true;  // �����Ƴ�״̬Ϊ��
        SetActive(false);  // �������Ϊ�ǻ״̬
        Destroy(gameObject);  // ������Ϸ����
        if (PanelManage.Instance.panelDict.ContainsKey(name))  // ���UI������������ֵ���������
        {
            PanelManage.Instance.panelDict.Remove(name);  // ���ֵ����Ƴ������
        }
    }
}

