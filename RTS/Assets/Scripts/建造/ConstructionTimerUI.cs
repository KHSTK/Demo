using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructionTimerUI : MonoBehaviour
{
    [SerializeField] private BuildingConstruction buildingConstruction; // 建筑施工组件

    private Image constructionProgressImage; // 施工进度显示图片

    private void Awake()
    {
        // 获取施工进度显示图片组件
        constructionProgressImage = transform.Find("Canvas").Find("image").GetComponent<Image>();
    }

    private void Update()
    {
        // 更新施工进度显示
        constructionProgressImage.fillAmount = buildingConstruction.GetConstructionTimerNormalized();
    }
}
