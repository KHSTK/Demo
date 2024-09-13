using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 0)]
public class CardData : ScriptableObject
{
    // 存储卡牌数据的列表
    public List<CardItem> cardItemDataList = new List<CardItem>();
}

// 表示单个关卡的数据
[System.Serializable]
public class CardItem
{
    public string name; //名称
    public float waitTime; //等待时间
    public int useSun;//需要阳光
    public GameObject prefab;//预制体
    public Sprite sprite;//图片
}

