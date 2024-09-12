using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourcesUI : MonoBehaviour
{
    private ResourceTypeList resourceTypeList; // 资源类型列表对象
    private Dictionary<ResourceType, Transform> resourceTypeTransformDictionary; // 资源类型与UI Transform的映射字典

    [SerializeField] private Transform resourceTemplate; // 资源UI模板

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeList>("ScriptableObject/资源类型/资源类型列表"); // 加载资源类型列表对象
        resourceTypeTransformDictionary = new Dictionary<ResourceType, Transform>(); // 创建资源类型与UI Transform的映射字典

        resourceTemplate.gameObject.SetActive(false); // 禁用资源UI模板

        int index = 0; // 索引计数器

        foreach (ResourceType resourceType in resourceTypeList.list) // 遍历资源类型列表
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform); // 实例化资源UI
            resourceTransform.gameObject.SetActive(true); // 启用资源UI

            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite; // 设置资源UI的图片

            // 调整位置
            Vector3 position = resourceTransform.localPosition;
            position.x += 200 * index; // 每次循环将位置向左移动200
            resourceTransform.localPosition = position;

            resourceTypeTransformDictionary[resourceType] = resourceTransform; // 将资源类型与UI Transform进行映射

            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount(); // 更新资源数量
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceType resourceType in resourceTypeList.list) // 遍历资源类型列表
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType]; // 获取对应资源类型的UI Transform

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType); // 获取资源数量
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString()); // 设置资源UI的文本


        }
    }
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
}
