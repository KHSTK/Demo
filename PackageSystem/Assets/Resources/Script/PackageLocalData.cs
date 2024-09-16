using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//本地数据存储
public class PackageLocalData
{
    //单例模式
    private static PackageLocalData _instance;
    public static PackageLocalData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLocalData();
            }
            return _instance;
        }
    }
    //缓存所有动态信息
    public List<PackageLocalItem> items;

    //存储数据
    public void SavePackage()
    {
        string inventoryJson = JsonUtility.ToJson(this);
        //数据存储为本地文件
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
        PlayerPrefs.Save();
    }
    //读取数据
    public List<PackageLocalItem> LoadPackage()
    {
        //缓存是否存在
        if (items != null)
        {
            return items;
        }
        //本地文件读取
        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }
}
//动态背包子项类
[System.Serializable]
public class PackageLocalItem
{
    public string uid;//唯一标识

    public int id;//标识是哪一个物品

    public int num;//物品数量

    public int level;//等级

    public bool isNew;//是否新获得

    public override string ToString()
    {
        return string.Format("[id]:{0} [num]:{1}", id, num);
    }
}

