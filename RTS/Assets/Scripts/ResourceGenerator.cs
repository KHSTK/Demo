using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    // private BuildingType buildingType; // 建筑类型对象
    private float timer; // 计时器
    private float timerMax; // 计时器最大值

    private void Awake() 
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData; // 获取建筑类型
        timerMax = resourceGeneratorData.timerMax; // 获取计时器最大值
    }

    private void Start()
    {
        // 获取附近的资源节点数量
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourecDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                // 如果资源节点的资源类型与此资源生成器的资源类型匹配，则增加附近资源节点的数量
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        // 将附近的资源节点数量限制在最大值范围内，并禁用此资源生成器的 Update 方法
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            //按附近的资源数控制资源的增加速度
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
        // 输出附近资源节点数量，用于调试
        Debug.Log("附近资源量:" + nearbyResourceAmount + ";计时器最大值:" + timerMax);
    }

    private void Update()
    {
        timer -= Time.deltaTime; // 更新计时器

        if (timer <= 0f) // 检查计时器是否到达或超过最大值
        {
            timer += timerMax; // 重置计时器

            // 调用 ResourceManager 的 AddResource 方法，增加资源
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        // 返回资源生成器的数据
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        // 返回计时器的归一化值，即当前计时器值除以计时器最大值
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        // 返回每秒生成的数量，即 1 除以计时器最大值
        return 1 / timerMax;
    }
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        // 获取附近的资源节点数量
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourecDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                // 如果资源节点的资源类型与此资源生成器的资源类型匹配，则增加附近资源节点的数量
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        // 将附近的资源节点数量限制在最大值范围内，并禁用此资源生成器的 Update 方法
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
}
