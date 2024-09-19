using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    //����̬����
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
    //ɾ�������Ʒ
    public void DeletePackageItems(List<string> uids)
    {
        foreach(string uid in uids)
        {
            DeletePackageItem(uid,false);
        }
        PackageLocalData.Instance.SavePackage();
    }
    //ɾ��������Ʒ
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
    //��ȡ��̬����
    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
            packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        }
        return packageTable;
    }

    //�������ͻ�ȡ���õı������
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
    //����
    public PackageLocalItem GetLotteryRandom1()
    {
        //�����ȡ��̬����
        List<PackageTableItem> packagesItems = GetPackageDataByType(GameConst.PackageTypeWeapon);
        int index = Random.Range(0, packagesItems.Count);
        PackageTableItem packageItem = packagesItems[index];
        //��Ӷ�̬����
        PackageLocalItem packageLocalItem = new()
        {
            uid = System.Guid.NewGuid().ToString(),
            id = packageItem.id,
            num = 1,
            level = 1,
            isNew = CheckWeaponIsNew(packageItem.id)
        };
        //�浵�鿨
        PackageLocalData.Instance.items.Add(packageLocalItem);
        PackageLocalData.Instance.SavePackage();
        return packageLocalItem;
    }
    //ʮ��
    public List<PackageLocalItem> GetLotteryRandom10(bool sort = false)
    {
        //����鿨
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

    //�Ƿ��»��
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

    //����idȡ�������
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

    //��ȡ��̬����
    public List<PackageLocalItem> GetPackageLocalDatas()
    {
        return PackageLocalData.Instance.LoadPackage();
    }
    //����Uidȡ��̬����
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
        //��star����
        int starComparison = y.star.CompareTo(x.star);
        //���star��ͬ��id��С��
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
    //��������
    public const int PackageTypeWeapon = 1;
    //ʳ������
    public const int PackageTypeFood = 2;
}
