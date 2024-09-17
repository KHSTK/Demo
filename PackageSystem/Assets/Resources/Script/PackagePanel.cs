using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    //定义背包UI各项
    private Transform UIMenu;
    private Transform UIMenuWeapon;
    private Transform UIMenuFood;
    private Transform UITabName;
    private Transform UICloseBtn;
    private Transform UICenter;
    private Transform UIScrollView;
    private Transform UIDetailPanel;
    private Transform UILeftBtn;
    private Transform UIRightBtn;
    private Transform UIDeletePanel;
    private Transform UIDeleteBackBtn;
    private Transform UIDeleteConfirmBtn;
    private Transform UIBottomMenus;
    private Transform UIDeleteBtn;
    private Transform UIDetailBtn;

    public GameObject PackageUIItemPrefab;
    override protected void Awake()
    {
        base.Awake();
        InitUI();
    }
    private void Start()
    {
        RefreshUI();
    }

    private void InitUI()
    {
        InitUIName();
        InitClick();
    }
    private void RefreshUI()
    {
        RefreshScroll();
    }
    //刷新滚动容器
    private void RefreshScroll()
    {
        //清除原有物体
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for(int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        //获取背包数据
        foreach(PackageLocalItem localData in GameManager.Instance.GetSortPackageLocalData())
        {
            Transform PackageUIItem = Instantiate(PackageUIItemPrefab.transform, scrollContent) as Transform;
            PackageCell packageCell = PackageUIItem.GetComponent<PackageCell>();
            packageCell.Refresh(localData, this);
        }
    }

    private void InitUIName()
    {
        UIMenu = transform.Find("CenterTop/Menu");
        UIMenuWeapon = transform.Find("CenterTop/Menus/Weapon");
        UIMenuFood = transform.Find("CenterTop/Menus/Food");
        UITabName = transform.Find("LeftTop/TabName");
        UICloseBtn = transform.Find("RightTop/Close");
        UICenter = transform.Find("Center");
        UIScrollView = transform.Find("Center/Scroll View");
        UIDetailPanel = transform.Find("Center/DeatilPanel");
        UILeftBtn = transform.Find("Left/Button");
        UIRightBtn = transform.Find("Right/Button");

        UIDeletePanel = transform.Find("Bottom/DeletePanel");
        UIDeleteBackBtn = transform.Find("Bottom/DeletePanel/Back");
        UIDeleteConfirmBtn = transform.Find("Bottom/DeletePanel/ConfirmBtn");
        UIBottomMenus = transform.Find("Bottom/BottomMenus");
        UIDeleteBtn = transform.Find("Bottom/BottomMenus/DeleteBtn");
        UIDetailBtn = transform.Find("Bottom/BottomMenus/DetailBtn");

        UIDeletePanel.gameObject.SetActive(false);
        UIBottomMenus.gameObject.SetActive(true);
    }
    //定义点击事件
    private void InitClick()
    {
        UIMenuWeapon.GetComponent<Button>().onClick.AddListener(OnClickWeapon);
        UIMenuFood.GetComponent<Button>().onClick.AddListener(OnClickFood);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(OnClickClose);
        UILeftBtn.GetComponent<Button>().onClick.AddListener(OnClickLeft);
        UIRightBtn.GetComponent<Button>().onClick.AddListener(OnClickRight);

        UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(OnDeleteBack);
        UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(OnDeleteConfirm);
        UIDeleteBtn.GetComponent<Button>().onClick.AddListener(OnDelete);
        UIDetailBtn.GetComponent<Button>().onClick.AddListener(OnDeatil);

    }

    private void OnDeatil()
    {
        Debug.Log("OnDeatil");
    }

    private void OnDelete()
    {
        Debug.Log("OnDelete");
    }

    private void OnDeleteConfirm()
    {
        Debug.Log("OnDeleteConfirm");
    }

    private void OnDeleteBack()
    {
        Debug.Log("OnDeleteBack");
    }

    private void OnClickRight()
    {
        Debug.Log("OnClickRight");
    }

    private void OnClickLeft()
    {
        Debug.Log("OnClickLeft");
    }

    private void OnClickClose()
    {
        Debug.Log("OnClickClose");
        ClosePanel();
        //UIManager.Instance.ClosePanel(UIConst.PackagePanel);

    }

    private void OnClickFood()
    {
        Debug.Log("OnClickFood");
    }

    private void OnClickWeapon()
    {
        Debug.Log("OnClickWeapon");
    }
}

