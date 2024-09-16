using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PackageTable",fileName ="PackageTable")]
public class PackageTable : ScriptableObject
{
    public List<PackageTableItem> DataList = new List<PackageTableItem>();
}

//背包物体类
[System.Serializable]
public class PackageTableItem
{
    public int id;//ID

    public int type;//类型

    public int star;//星级

    public string name;//名称

    public string description;//粗略描述

    public string skillDescription;//细节描述

    public string imagePath;//图片素材路径
} 
