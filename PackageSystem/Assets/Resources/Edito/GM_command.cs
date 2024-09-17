using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GM_command 
{
    [MenuItem("GM_command/��ȡ���")]
    public static void ReadTable()
    {
        PackageTable packageTable = Resources.Load<PackageTable>("TableData/PackageTable");
        foreach(PackageTableItem packageTableItem in packageTable.DataList)
        {
            Debug.Log(string.Format("��id��:{0}, ��name��:{1}", packageTableItem.id, packageTableItem.name));
        }
    }
    [MenuItem("GM_command/����������������")]
    public static void CrateLocalPackageData()
    {
        //��������
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 1; i < 9; i++)
        {
            PackageLocalItem packageLocalItem = new()
            {
                uid = Guid.NewGuid().ToString(),
                id = i,
                num = i,
                level = i,
                isNew = i % 2 == 1
            };
            //List<T>��Add����
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();

    }
    [MenuItem("GM_command/��ȡ������������")]
    public static void ReadLocalPackageData()
    {
        // ��ȡ����
        List<PackageLocalItem> readItems = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("GM_command/�򿪱���������")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }
}
