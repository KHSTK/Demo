using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//关卡列表数据
[CreateAssetMenu(fileName = "LevelList", menuName = "LevelList", order = 0)]
public class LevelList : ScriptableObject
{
    // 存储卡牌数据的列表
    public List<Item> list = new List<Item>();
}

// 表示单个关卡的数据
[System.Serializable]
public class Item
{
    public string levelId; //关卡ID
    public string name; //名称
    public Sprite sprite;//图片
    public Sprite bgSprite;//背景图片
}

