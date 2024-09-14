using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : BasePanel
{
    public Button btnCancel;//������Ϸ
    public Button btnAtlas;//�鿴ͼ��
    public Button btnRestart;//���¿�ʼ
    public Button btnMainMenu;//���˵�
    [SerializeField] private SceneField mainMenuScene;//���˵�����

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
        //PanelManage.Instance.OpenTips("�������ڻ��ٿ����У������ڴ�");
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

