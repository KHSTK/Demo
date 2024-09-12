using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class BuildingManager : MonoBehaviour
{

    private BuildingTypeList buildingTypeList; // ���������б����
    private BuildingType buildingType; // ��ǰѡ�еĽ������Ͷ���
    public static BuildingManager Instance { get; private set; }
    private BuildingType activeBuildingType; // ��ǰѡ�еĽ������Ͷ���
    private Camera mainCamera;
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    [SerializeField] private Building hqBuilding;//�ܲ�Building���
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingType activeBuildingType;
    }
    void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeList>("ScriptableObject/��������/���������б�"); // ���ؽ��������б�
        buildingType = buildingTypeList.buildingTypeList[0]; // ��ʼ��Ϊ�б��еĵ�һ����������
    }

    private void Start()
    {
        mainCamera = Camera.main; // ��ȡ�����������
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
                        // �������λ�ô���һ������ʵ��
                        //Instantiate(activeBuildingType.prefab, Utilsclass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(Utilsclass.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else
                    {
                        TooltipUI.Instance.Show("��Դ���� " + activeBuildingType.GetConstructionResourceCoststring(), new TooltipUI.TooltipTimer { timer = 2f });
                    }
                }
                else
                {
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
        //���λ�����ɵ���
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Vector3 enemySpawnPosition = Utilsclass.GetMouseWorldPosition() + Utilsclass.GetRandomDir() * 5f;
        //    Enemy.Create(enemySpawnPosition);
        //}

    }
    // �޸ĵ�ǰѡ�еĽ������Ͷ���
    public void SetActiveBuildingType(BuildingType buildingType)
    {
        activeBuildingType = buildingType;

        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    // ��ȡ�����λ�ö�Ӧ����������
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // ��Z��������Ϊ0���Ա�֤�ڶ�άƽ���ϴ���ʵ��
        return mouseWorldPosition;
    }
    //��ȡѡ�еĽ�������
    public BuildingType GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingType buildingType, Vector3 position, out string errorMessage)
    {
        // ��ȡ������Ԥ�������ײ��
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        // ��ָ��λ��ʹ�ú��μ���ȡ�����ص�����ײ��
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        // �ж��Ƿ���������ײ����Ҫ���ɵĽ������ص���������򷵻� false
        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            errorMessage = "�����ص�!";
            return false;
        }
        errorMessage = "";
        // ����������������㣬��������ɽ�������� true
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

