using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, int> resourceAmountDictionary; // 资源类型与数量的字典
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;

    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceType, int>(); // 初始化资源字典

        // 加载资源类型列表
        ResourceTypeList resourceTypeList = Resources.Load<ResourceTypeList>("ScriptableObject/资源类型/资源类型列表");

        // 遍历资源类型列表，将每个资源类型添加到资源字典并初始化数量为0
        foreach (ResourceType resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary(); // 测试输出资源字典
        Instance = this;
    }

    private void TestLogResourceAmountDictionary()
    {
        // 遍历资源字典，输出每个资源类型及其对应的数量
        foreach (ResourceType resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }
    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount; // 增加资源数量

        //使用了 ?.Invoke 运算符来避免空引用异常
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);

        TestLogResourceAmountDictionary(); // 调用测试方法，输出资源数量
    }

    // 获取资源数量
    public int GetResourceAmount(ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    //判断资源是否够
    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        // 遍历所有资源类型和数量
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount)
            {
                // 支付不起该资源，返回 false
                return false;
            }
        }
        // 所有资源的数量都足够支付，返回 true
        return true;
    }

    //减少对应资源
    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        // 遍历所有资源类型和数量
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            // 减少对应资源类型的数量
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }

}

