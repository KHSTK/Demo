using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    // ������ťģ��
    public Transform btnTemplate;
    private Dictionary<BuildingType, Transform> btnTransformDictionary;

    private void Awake()
    {
        btnTransformDictionary = new Dictionary<BuildingType, Transform>();
        // ���ؽ��������б���Դ
        BuildingTypeList buildingTypeList = Resources.Load<BuildingTypeList>("ScriptableObject/��������/���������б�");
        int index = 0;

        // �������������б�������Ӧ�İ�ť
        foreach (BuildingType buildingType in buildingTypeList.buildingTypeList)
        {
            Transform btnTransform = Instantiate(btnTemplate, transform);

            // ����ͼƬ
            btnTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            //�󶨵���¼�
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

            // ����λ��
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

    // ���µ�ǰѡ�н������Ͱ�ť����ʽ
    private void UpdateActiveBuildingTypeButton()
    {
        // Ĭ�Ϲر�ѡ��ͼ��
        foreach (KeyValuePair<BuildingType, Transform> entry in btnTransformDictionary)
        {
            Transform btnTransform = entry.Value;
            btnTransform.Find("selected").gameObject.SetActive(false);
        }

        // ��ȡ��ǰ����Ľ�������
        BuildingType activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType != null && btnTransformDictionary.TryGetValue(activeBuildingType, out Transform activeBtnTransform))
        {
            // ����ѡ��ͼ��
            activeBtnTransform.Find("selected").gameObject.SetActive(true);
        }
    }
}

