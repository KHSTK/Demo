using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
    public Button btnCancel;//返回游戏
    public Button btnAtlas;//查看图鉴
    public Button btnRestart;//重新开始
    public Button btnMainMenu;//主菜单
    [SerializeField] private SceneField mainMenuScene;//主菜单场景

    private void Awake()
    {
        btnCancel.onClick.AddListener(OnBtnCancel);
        btnAtlas.onClick.AddListener(OnBtnAtlas);
        btnRestart.onClick.AddListener(OnBtnRestart);
        btnMainMenu.onClick.AddListener(OnBtnMainMenu);
    }

    public void OnBtnCancel()
    {
        ClosePanel();
    }

    public void OnBtnAtlas()
    {
        //PanelManage.Instance.OpenTips("功能正在火速开发中，敬请期待");
    }

    public void OnBtnRestart()
    {
        ClosePanel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBtnMainMenu()
    {
        ClosePanel();
        SceneManager.LoadScene(mainMenuScene);
    }
}

