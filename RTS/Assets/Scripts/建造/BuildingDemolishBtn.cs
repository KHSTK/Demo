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
            // 获取建筑类型
            BuildingType buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;

            // 对于建筑类型的每个资源成本
            foreach (ResourceAmount resourceAmount in buildingType.constructionResourceCostArray)
            {
                // 扣除资源总量的60%
                int deductedAmount = Mathf.FloorToInt(resourceAmount.amount * 0.6f);

                // 添加扣除后的资源数量
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, deductedAmount);

                // 销毁建筑 GameObject
                Destroy(building.gameObject);
            }
        });
    }
}
