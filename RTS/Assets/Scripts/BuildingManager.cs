using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class BuildingManager : MonoBehaviour
{

    private BuildingTypeList buildingTypeList; // 建筑类型列表对象
    private BuildingType buildingType; // 当前选中的建筑类型对象
    public static BuildingManager Instance { get; private set; }
    private BuildingType activeBuildingType; // 当前选中的建筑类型对象
    private Camera mainCamera;
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    [SerializeField] private Building hqBuilding;//总部Building组件
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingType activeBuildingType;
    }
    void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeList>("ScriptableObject/建筑类型/建筑类型列表"); // 加载建筑类型列表
        buildingType = buildingTypeList.buildingTypeList[0]; // 初始化为列表中的第一个建筑类型
    }

    private void Start()
    {
        mainCamera = Camera.main; // 获取主摄像机对象
        hqBuilding.GetComponent<HealthSystem>().OnDied += HQ_OnDied;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null&&activeBuildingType.prefab != null)
            {
                if (CanSpawnBuilding(activeBuildingType, Utilsclass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        // 在鼠标点击位置创建一个建筑实例
                        //Instantiate(activeBuildingType.prefab, Utilsclass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(Utilsclass.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("资源不够 " + activeBuildingType.GetConstructionResourceCoststring(), new TooltipUI.TooltipTimer { timer = 2f });
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
        //鼠标位置生成敌人
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Vector3 enemySpawnPosition = Utilsclass.GetMouseWorldPosition() + Utilsclass.GetRandomDir() * 5f;
        //    Enemy.Create(enemySpawnPosition);
        //}

    }
    // 修改当前选中的建筑类型对象
    public void SetActiveBuildingType(BuildingType buildingType)
    {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    // 获取鼠标点击位置对应的世界坐标
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // 将Z轴坐标设为0，以保证在二维平面上创建实例
        return mouseWorldPosition;
    }
    //获取选中的建筑类型
    public BuildingType GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingType buildingType, Vector3 position, out string errorMessage)
    {
        // 获取建筑物预制体的碰撞器
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        // 在指定位置使用盒形检测获取所有重叠的碰撞体
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        // 判断是否有其他碰撞体与要生成的建筑物重叠，如果有则返回 false
        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "区域重叠!";
            return false;
        }
        errorMessage = "";
        // 如果以上条件都满足，则可以生成建筑物，返回 true
        return true;
    }
    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
    private void HQ_OnDied(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
    }
}

