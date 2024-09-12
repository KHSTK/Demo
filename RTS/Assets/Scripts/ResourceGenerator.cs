using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;
    // private BuildingType buildingType; // �������Ͷ���
    private float timer; // ��ʱ��
    private float timerMax; // ��ʱ�����ֵ

    private void Awake() 
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData; // ��ȡ��������
        timerMax = resourceGeneratorData.timerMax; // ��ȡ��ʱ�����ֵ
    }

    private void Start()
    {
        // ��ȡ��������Դ�ڵ�����
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourecDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                // �����Դ�ڵ����Դ���������Դ����������Դ����ƥ�䣬�����Ӹ�����Դ�ڵ������
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        // ����������Դ�ڵ��������������ֵ��Χ�ڣ������ô���Դ�������� Update ����
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            //����������Դ��������Դ�������ٶ�
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
        // ���������Դ�ڵ����������ڵ���
        Debug.Log("������Դ��:" + nearbyResourceAmount + ";��ʱ�����ֵ:" + timerMax);
    }

    private void Update()
    {
        timer -= Time.deltaTime; // ���¼�ʱ��

        if (timer <= 0f) // ����ʱ���Ƿ񵽴�򳬹����ֵ
        {
            timer += timerMax; // ���ü�ʱ��

            // ���� ResourceManager �� AddResource ������������Դ
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        // ������Դ������������
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        // ���ؼ�ʱ���Ĺ�һ��ֵ������ǰ��ʱ��ֵ���Լ�ʱ�����ֵ
        return timer / timerMax;
    }

    public float GetAmountGeneratedPerSecond()
    {
        // ����ÿ�����ɵ��������� 1 ���Լ�ʱ�����ֵ
        return 1 / timerMax;
    }
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        // ��ȡ��������Դ�ڵ�����
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourecDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                // �����Դ�ڵ����Դ���������Դ����������Դ����ƥ�䣬�����Ӹ�����Դ�ڵ������
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        // ����������Դ�ڵ��������������ֵ��Χ�ڣ������ô���Դ�������� Update ����
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
}
