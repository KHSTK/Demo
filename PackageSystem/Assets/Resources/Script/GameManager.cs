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
        
        UIManager.Instance.OpenPanel(UIConst.MainPanel);
    }
    //删除多个物品
    public void DeletePackageItems(List<string> uids)
    {
        foreach(string uid in uids)
        {
            DeletePackageItem(uid,false);
        }
        PackageLocalData.Instance.SavePackage();
    }
    //删除单个物品
    public void DeletePackageItem(string uid, bool needSave = true)
    {
        PackageLocalItem packageLocalItem = GetPackageLocalItemByUid(uid);
        if (packageLocalItem == null)
        {
            return;
        }
        PackageLocalData.Instance.items.Remove(packageLocalItem);
        if (needSave)
        {
            PackageLocalData.Instance.SavePackage();
        }
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

    //根据类型获取配置的表格数据
    public List<PackageTableItem> GetPackageDataByType(int type)
    {
        List<PackageTableItem> packagesItems = new List<PackageTableItem>();
        foreach(PackageTableItem packageItem in GetPackageTable().DataList)
        {
            if (packageItem.type == type)
            {
                packagesItems.Add(packageItem);
            }
        }
        return packagesItems;
    }
    //单抽
    public PackageLocalItem GetLotteryRandom1()
    {
        //随机获取静态数据
        List<PackageTableItem> packagesItems = GetPackageDataByType(GameConst.PackageTypeWeapon);
        int index = Random.Range(0, packagesItems.Count);
        PackageTableItem packageItem = packagesItems[index];
        //添加动态数据
        PackageLocalItem packageLocalItem = new()
        {
            uid = System.Guid.NewGuid().ToString(),
            id = packageItem.id,
            num = 1,
            level = 1,
            isNew = CheckWeaponIsNew(packageItem.id)
        };
        //存档抽卡
        PackageLocalData.Instance.items.Add(packageLocalItem);
        PackageLocalData.Instance.SavePackage();
        return packageLocalItem;
    }
    //十连
    public List<PackageLocalItem> GetLotteryRandom10(bool sort = false)
    {
        //随机抽卡
        List<PackageLocalItem> packageLocalItems = new();
        for(int i=0;i<10; i++)
        {
            PackageLocalItem packageLocalItem = GetLotteryRandom1();
            packageLocalItems.Add(packageLocalItem);
        }
        if (sort)
        {
            packageLocalItems.Sort(new PackageItemComparer());
        }
        return packageLocalItems;
    }

    //是否新获得
    public bool CheckWeaponIsNew(int id)
    {
        foreach(PackageLocalItem packageLocalItem in GetPackageLocalDatas())
        {
            if (packageLocalItem.id == id)
            {
                return false;
            }
        }
        return true;
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
                return b.level.CompareTo(a.level);
            }
            return idComparison;
        }
        return starComparison;
    }
}
public class GameConst
{
    //武器类型
    public const int PackageTypeWeapon = 1;
    //食物类型
    public const int PackageTypeFood = 2;
}
