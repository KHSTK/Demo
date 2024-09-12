using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/��������")]
public class BuildingType : ScriptableObject
{
    public string nameString; // �������͵������ַ���
    public Transform prefab; // �������Ͷ�Ӧ��Ԥ����
    public ResourceGeneratorData resourceGeneratorData; // ��Դ������������
    public Sprite sprite; //������ͼ��
    public float minConstructionRadius; //��Сʩ���뾶
    public ResourceAmount[] constructionResourceCostArray;
    public int healthAmountMax;//�������ֵ
    public float constructionTimerMax; //ʩ����Ҫ��ʱ��
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

