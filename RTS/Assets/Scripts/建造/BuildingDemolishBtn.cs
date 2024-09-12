using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{
    [SerializeField] private Building building;

    private void Awake()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            // ��ȡ��������
            BuildingType buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;

            // ���ڽ������͵�ÿ����Դ�ɱ�
            foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
            {
                // �۳���Դ������60%
                int deductedAmount = Mathf.FloorToInt(resourceAmount.amount * 0.6f);

                // ��ӿ۳������Դ����
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, deductedAmount);

                // ���ٽ��� GameObject
                Destroy(building.gameObject);
            }
        });
    }
}
