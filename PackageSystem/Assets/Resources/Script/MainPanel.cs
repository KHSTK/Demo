using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private Transform UILottery;
    private Transform UIPackage;
    private Transform UIQuitBtn;

    protected override void Awake()
    {
        base.Awake();
        InitUI();
    }
    private void InitUI()
    {
        UILottery = transform.Find("Top/LotteryBtn");
        UIPackage = transform.Find("Top/PackageBtn");
        UIQuitBtn = transform.Find("BottomLeft/QuitBtn");
        UILottery.GetComponent<Button>().onClick.AddListener(OnBtnLottery);
        UIPackage.GetComponent<Button>().onClick.AddListener(OnBtnPackage);
        UIQuitBtn.GetComponent<Button>().onClick.AddListener(OnQuitGame);
    }

    private void OnQuitGame()
    {
        Debug.Log("OnQuitGame");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnBtnPackage()
    {
        Debug.Log("OnBtnPackage");
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        ClosePanel();
    }

    private void OnBtnLottery()
    {
        Debug.Log("OnBtnLottery");
        UIManager.Instance.OpenPanel(UIConst.LotteryPanel);
        ClosePanel();
    }
}

