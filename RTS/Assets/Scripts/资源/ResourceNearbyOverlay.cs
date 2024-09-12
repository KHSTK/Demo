using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        // 获取附近资源的数量
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        // 计算资源数量占最大资源量的百分比，并取整数值
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);

        // 在界面上显示百分比文本
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        // 记录资源生成器的数据，以便后续使用
        this.resourceGeneratorData = resourceGeneratorData;

        if (resourceGeneratorData.resourceType)
        {
            // 激活显示该界面
            gameObject.SetActive(true);

            // 设置图标的 SpriteRenderer
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        }
        else {
            gameObject.SetActive(false);
        }

    }

    public void Hide()
    {
        // 隐藏该界面
        gameObject.SetActive(false);
    }
}

