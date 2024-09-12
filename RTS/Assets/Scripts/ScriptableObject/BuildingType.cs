using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/建筑类型")]
public class BuildingType : ScriptableObject
{
    public string nameString; // 建筑类型的名称字符串
    public Transform prefab; // 建筑类型对应的预制体
    public ResourceGeneratorData resourceGeneratorData; // 资源生成器的数据
    public Sprite sprite; //建筑的图标
    public float minConstructionRadius; //最小施工半径
    public ResourceAmount[] constructionResourceCostArray;
    public int healthAmountMax;//最大生命值
    public float constructionTimerMax; //施工需要的时间
    public string GetConstructionResourceCoststring()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += resourceAmount.resourceType.nameString + ":" + resourceAmount.amount;
        }
        return str;
    }
    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }
}

