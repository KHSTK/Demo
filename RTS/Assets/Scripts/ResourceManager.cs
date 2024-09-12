using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, int> resourceAmountDictionary; // ��Դ�������������ֵ�
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;

    private void Awake()
    {
        resourceAmountDictionary = new Dictionary<ResourceType, int>(); // ��ʼ����Դ�ֵ�

        // ������Դ�����б�
        ResourceTypeList resourceTypeList = Resources.Load<ResourceTypeList>("ScriptableObject/��Դ����/��Դ�����б�");

        // ������Դ�����б���ÿ����Դ������ӵ���Դ�ֵ䲢��ʼ������Ϊ0
        foreach (ResourceType resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary(); // ���������Դ�ֵ�
        Instance = this;
    }

    private void TestLogResourceAmountDictionary()
    {
        // ������Դ�ֵ䣬���ÿ����Դ���ͼ����Ӧ������
        foreach (ResourceType resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }
    public void AddResource(ResourceType resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount; // ������Դ����

        //ʹ���� ?.Invoke �����������������쳣
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);

        TestLogResourceAmountDictionary(); // ���ò��Է����������Դ����
    }

    // ��ȡ��Դ����
    public int GetResourceAmount(ResourceType resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    //�ж���Դ�Ƿ�
    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        // ����������Դ���ͺ�����
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) < resourceAmount.amount)
            {
                // ֧���������Դ������ false
                return false;
            }
        }
        // ������Դ���������㹻֧�������� true
        return true;
    }

    //���ٶ�Ӧ��Դ
    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        // ����������Դ���ͺ�����
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            // ���ٶ�Ӧ��Դ���͵�����
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }

}

