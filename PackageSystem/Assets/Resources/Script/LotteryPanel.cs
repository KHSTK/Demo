using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryPanel : BasePanel
{
    private Transform UIClose;
    private Transform UICenter;
    private Transform UILottery10;
    private Transform UILottery1;
    private GameObject LotteryCellPrefab;

    protected override void Awake()
    {
        base.Awake();
        InitUI();
        InitPrefab();
    }
    private void InitUI()
    {
        UIClose = transform.Find("TopRight/Close");
        UICenter = transform.Find("Center");
        UILottery10 = transform.Find("Bottom/Lottery10");
        UILottery1 = transform.Find("Bottom/Lottery1");
        UILottery10.GetComponent<Button>().onClick.AddListener(OnLottery10Btn);
        UILottery1.GetComponent<Button>().onClick.AddListener(OnLottery1Btn);
        UIClose.GetComponent<Button>().onClick.AddListener(OnClose);
    }

    private void OnClose()
    {
        Debug.Log("OnClose");
        ClosePanel();
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
    }

    private void InitPrefab()
    {
        LotteryCellPrefab = Resources.Load("Prefab/Panel/Lottery/LotteryItem") as GameObject;
    }

    private void OnLottery1Btn()
    {
        Debug.Log("OnLottery1Btn");
        //销毁上次抽卡结果
        for (int i=0;i<UICenter.childCount;i++)
        {
            Destroy(UICenter.GetChild(i).gameObject);
        }
        //抽卡
        PackageLocalItem item = GameManager.Instance.GetLotteryRandom1();
        Transform LotteryCellTran = Instantiate(LotteryCellPrefab.transform, UICenter) as Transform;
        //展示抽卡结果
        LotteryCell lottertCell = LotteryCellTran.GetComponent<LotteryCell>();
        lottertCell.Refresh(item, this);
    }

    private void OnLottery10Btn()
    {
        Debug.Log("OnLottery10Btn");
        //销毁上次抽卡结果
        List<PackageLocalItem> packageLocalItems = GameManager.Instance.GetLotteryRandom10(sort: true);
        for (int i = 0; i < UICenter.childCount; i++)
        {
            Destroy(UICenter.GetChild(i).gameObject);
        }
        //抽卡
        foreach (PackageLocalItem item in packageLocalItems)
        {
            Transform LotteryCellTran = Instantiate(LotteryCellPrefab.transform, UICenter) as Transform;
            //展示抽卡结果
            LotteryCell lottertCell = LotteryCellTran.GetComponent<LotteryCell>();
            lottertCell.Refresh(item, this);
        }
    }
}
