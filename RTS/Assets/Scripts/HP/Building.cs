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
        buildingDemolishBtn = transform.Find("ɾ����ť");
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
        buildingType = GetComponent<BuildingTypeHolder>().buildingType; // ��ȡ��������
        healthSystem = GetComponent<HealthSystem>(); // ��ȡHealthSystem���
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true); // �����������ֵ
        healthSystem.OnDied += HealthSystem_OnDied; // ���������¼�
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthSystem.Damage(10); // ��������
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject); // ������Ϸ����
    }
}

