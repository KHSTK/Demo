using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeList resourceTypeList; // ��Դ�����б����
    private Dictionary<ResourceType, Transform> resourceTypeTransformDictionary; // ��Դ������UI Transform��ӳ���ֵ�

    [SerializeField] private Transform resourceTemplate; // ��ԴUIģ��

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeList>("ScriptableObject/��Դ����/��Դ�����б�"); // ������Դ�����б����
        resourceTypeTransformDictionary = new Dictionary<ResourceType, Transform>(); // ������Դ������UI Transform��ӳ���ֵ�

        resourceTemplate.gameObject.SetActive(false); // ������ԴUIģ��

        int index = 0; // ����������

        foreach (ResourceType resourceType in resourceTypeList.list) // ������Դ�����б�
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform); // ʵ������ԴUI
            resourceTransform.gameObject.SetActive(true); // ������ԴUI

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite; // ������ԴUI��ͼƬ

            // ����λ��
            Vector3 position = resourceTransform.localPosition;
            position.x += 200 * index; // ÿ��ѭ����λ�������ƶ�200
            resourceTransform.localPosition = position;

            resourceTypeTransformDictionary[resourceType] = resourceTransform; // ����Դ������UI Transform����ӳ��

            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount(); // ������Դ����
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceType resourceType in resourceTypeList.list) // ������Դ�����б�
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType]; // ��ȡ��Ӧ��Դ���͵�UI Transform

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType); // ��ȡ��Դ����
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString()); // ������ԴUI���ı�


        }
    }
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
}
