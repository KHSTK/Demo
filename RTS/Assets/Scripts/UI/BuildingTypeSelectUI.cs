using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    // 建筑按钮模板
    public Transform btnTemplate;
    private Dictionary<BuildingType, Transform> btnTransformDictionary;

    private void Awake()
    {
        btnTransformDictionary = new Dictionary<BuildingType, Transform>();
        // 加载建筑类型列表资源
        BuildingTypeList buildingTypeList = Resources.Load<BuildingTypeList>("ScriptableObject/建筑类型/建筑类型列表");
        int index = 0;

        // 遍历建筑类型列表，创建对应的按钮
        foreach (BuildingType buildingType in buildingTypeList.buildingTypeList)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);

            // 设置图片
            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            //绑定点击事件
            btnTransform.Find("image").GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            MouseEnterExitEvents mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => {
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCoststring());
            };
            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };

            btnTransformDictionary[buildingType] = btnTransform;

            // 调整位置
            Vector3 position = btnTransform.localPosition;
            position.x += 300 * index;
            btnTransform.localPosition = position;

            index++;
        }

    }
    //private void Update()
    //{
    //    UpdateActiveBuildingTypeButton();
    //}

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    // 更新当前选中建筑类型按钮的样式
    private void UpdateActiveBuildingTypeButton()
    {
        // 默认关闭选中图像
        foreach (KeyValuePair<BuildingType, Transform> entry in btnTransformDictionary)
        {
            Transform btnTransform = entry.Value;
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        // 获取当前激活的建筑类型
        BuildingType activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType != null && btnTransformDictionary.TryGetValue(activeBuildingType, out Transform activeBtnTransform))
        {
            // 开启选中图像
            activeBtnTransform.Find("selected").gameObject.SetActive(true);
        }
    }
}

