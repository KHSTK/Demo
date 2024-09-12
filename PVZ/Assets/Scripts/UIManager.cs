using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI sunSumText;
    public ProgressPanel progressPanel;
    int zombieDiedCount = 0;//������ʬ����
    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        Init();
        zombieDiedCount = 0;
        InitProgressPanel();
    }

    public void Init()
    {
        sunSumText.text = GameManager.Instance.sunSum.ToString();
    }
    //��ʼ��������
    public void InitProgressPanel()
    {
        //��ʼ����Ϊ0
        progressPanel.SetPercent(0);

        int count = GameManager.Instance.listData.Count;
        string progressId = GameManager.Instance.listData[0]["progressId"];
        //���������б���ȡ����λ��
        for(int i = 1; i < count; i++)
        {
            //��ȡ��ǰ�ֵ�����
            Dictionary<string, string> dic = GameManager.Instance.listData[i];
            if (progressId != dic["progressId"])
            {
                progressPanel.SetFlagPercent((float)i / count);
            }
            progressId = dic["progressId"];
        }
    }
    //���½���
    public void UpdateProgressPanel()
    {
        zombieDiedCount++;
        progressPanel.SetPercent((float)zombieDiedCount / GameManager.Instance.listData.Count);
    }

}

