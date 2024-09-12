using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGeneratorData
{
    public float timerMax; // 生成资源的时间间隔
    public ResourceType resourceType; // 资源类型
    public float resourecDetectionRadius; //资源检测半径
    public int maxResourceAmount;   //最大资源数量
}

