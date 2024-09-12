using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private BuildingType buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishBtn;

    private void Awake()
    {
        buildingDemolishBtn = transform.Find("删除按钮");
        if (buildingDemolishBtn != null) buildingDemolishBtn.gameObject.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (buildingDemolishBtn != null) buildingDemolishBtn.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (buildingDemolishBtn != null) buildingDemolishBtn.gameObject.SetActive(false);
    }

    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType; // 获取建筑类型
        healthSystem = GetComponent<HealthSystem>(); // 获取HealthSystem组件
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true); // 设置最大生命值
        healthSystem.OnDied += HealthSystem_OnDied; // 订阅死亡事件
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.Damage(10); // 测试受伤
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject); // 销毁游戏对象
    }
}

