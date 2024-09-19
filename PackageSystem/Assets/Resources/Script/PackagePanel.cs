using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PackageMod
{
    normal,
    delete,
    sort,
}

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
    //当前处于哪种模式
    public PackageMod curMode = PackageMod.normal;

    //选中删除物品
    public List<string> deleteChooseUid;
    //当前选择
    private string _chooseUid;
    public string chooseUID
    {
        get
        {
            return _chooseUid;
        }
        set
        {
            _chooseUid = value;
            RefreshDetail();
        }
    }
    //添加删除选中项
    public void AddChooseDeleteUid(string uid)
    {
        this.deleteChooseUid ??= new List<string>();
        if (!this.deleteChooseUid.Contains(uid))
        {
            this.deleteChooseUid.Add(uid);
        }
        else
        {
            this.deleteChooseUid.Remove(uid);
        }
        RefreshDeletePanel();
    }
    //刷新删除界面
    private void RefreshDeletePanel()
    {
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        //遍历子物品调用删除接口
        foreach(Transform cell in scrollContent)
        {
            PackageCell packageCell = cell.GetComponent<PackageCell>();
            packageCell.RefreshDeleteState();
        }
    }

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
    //刷新详情页面
    private void RefreshDetail()
    {
        //找到uid对应数据
        PackageLocalItem localItem = GameManager.Instance.GetPackageLocalItemByUid(chooseUID);
        //刷新详情页面
        UIDetailPanel.GetComponent<PackageDetail>().Refresh(localItem, this);
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
        UIDetailPanel = transform.Find("Center/DetailPanel");
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
    //进入删除模式
    private void OnDelete()
    {
        Debug.Log("OnDelete");
        curMode = PackageMod.delete;
        UIDeletePanel.gameObject.SetActive(true);

    }
    //确认删除
    private void OnDeleteConfirm()
    {
        Debug.Log("OnDeleteConfirm");
        if (this.deleteChooseUid == null)
        {
            return;
        }
        if (this.deleteChooseUid.Count == 0)
        {
            return;
        }
        GameManager.Instance.DeletePackageItems(this.deleteChooseUid);
        //删除后刷新整个背包
        RefreshUI();
    }
    //退出删除模式
    private void OnDeleteBack()
    {
        Debug.Log("OnDeleteBack");
        curMode = PackageMod.normal;
        UIDeletePanel.gameObject.SetActive(false);
        //重置选中的删除列表
        deleteChooseUid = new List<string>();
        //刷新选中状态
        RefreshDeletePanel();
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
        UIManager.Instance.OpenPanel(UIConst.MainPanel);

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

