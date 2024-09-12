using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField]
    private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start()
    {
        // 获取资源生成器的数据
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        // 查找并设置进度条的 Transform
        barTransform = transform.Find("bar");

        // 查找并设置图标的 SpriteRenderer
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        // 查找并设置文本的 TextMeshPro 组件，显示每秒生成的数量（保留一位小数）
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        // 更新进度条的缩放比例，根据当前计时器的归一化值确定
        barTransform.localScale = new Vector3(resourceGenerator.GetTimerNormalized(), barTransform.localScale.y, 1);
    }

}
