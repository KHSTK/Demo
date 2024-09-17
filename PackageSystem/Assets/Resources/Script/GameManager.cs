using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    //处理静态属性
    private PackageTable packageTable;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
    //获取静态数据
    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        }
        return packageTable;
    }

    //根据id取表格物体
    public PackageTableItem GetPackageItemById(int id)
    {
        List<PackageTableItem> packageDataList = GetPackageTable().DataList; 
        foreach(PackageTableItem item in packageDataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }

    //获取动态数据
    public List<PackageLocalItem> GetPackageLocalDatas()
    {
        return PackageLocalData.Instance.LoadPackage();
    }
    //根据Uid取动态数据
    public PackageLocalItem GetPackageLocalItemByUid(string uid)
    {
        List<PackageLocalItem> packageDataList = GetPackageLocalDatas();
        foreach(PackageLocalItem item in packageDataList)
        {
            if (item.uid == uid)
            {
                return item;
            }
        }
        return null;
    }
    public List<PackageLocalItem> GetSortPackageLocalData()
    {
        List<PackageLocalItem> localItems = PackageLocalData.Instance.LoadPackage();
        localItems.Sort(new PackageItemComparer());
        return localItems;
    }
}
public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem a,PackageLocalItem b)
    {
        PackageTableItem x = GameManager.Instance.GetPackageItemById(a.id);
        PackageTableItem y = GameManager.Instance.GetPackageItemById(b.id);
        //按star排序
        int starComparison = y.star.CompareTo(x.star);
        //如果star相同按id大小排
        if (starComparison == 0)
        {
            int idComparison = y.id.CompareTo(x.id);
            if (idComparison == 0)
            {
                return b.level.CompareTo(a);
            }
            return idComparison;
        }
        return starComparison;
    }
}
