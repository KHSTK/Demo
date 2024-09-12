using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI sunSumText;
    public ProgressPanel progressPanel;
    int zombieDiedCount = 0;//死亡僵尸数量
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
    //初始化进度条
    public void InitProgressPanel()
    {
        //初始进度为0
        progressPanel.SetPercent(0);

        int count = GameManager.Instance.listData.Count;
        string progressId = GameManager.Instance.listData[0]["progressId"];
        //遍历数据列表，获取旗帜位置
        for(int i = 1; i < count; i++)
        {
            //获取当前字典数据
            Dictionary<string, string> dic = GameManager.Instance.listData[i];
            if (progressId != dic["progressId"])
            {
                progressPanel.SetFlagPercent((float)i / count);
            }
            progressId = dic["progressId"];
        }
    }
    //更新进度
    public void UpdateProgressPanel()
    {
        zombieDiedCount++;
        progressPanel.SetPercent((float)zombieDiedCount / GameManager.Instance.listData.Count);
    }

}

