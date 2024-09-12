using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private BuildingType buildingType;  // 建筑类型信息
    private float constructionTimer;  // 施工计时器，表示还需要多少时间完成建筑
    private float constructionTimerMax;  // 施工计时器的最大值
    private BoxCollider2D boxCollider2D;  // 用于检测碰撞的组件
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();  // 获取碰撞体组件
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        // 如果施工计时器小于等于0，表示建筑已经完成
        if (constructionTimer <= 0f)
        {
            // 创建建筑物
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);

            // 销毁建筑施工物体
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingType buildingType)
    {
        this.buildingType = buildingType;

        // 设置施工计时器的最大值
        constructionTimerMax = buildingType.constructionTimerMax;

        // 初始化施工计时器
        constructionTimer = constructionTimerMax;

        // 设置碰撞体的偏移和大小，与建筑物预制体保持一致
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        //图片跟着建筑物预制体修改
        spriteRenderer.sprite = buildingType.sprite;
    }

    public static BuildingConstruction Create(Vector3 position, BuildingType buildingType)
    {
        // 加载建筑施工预制体
        Transform pfBuildingConstruction = Resources.Load<Transform>("建筑施工模板");

        // 实例化建筑施工物体
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();

        // 设置建筑类型
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }
    // 返回施工计时器的标准化值，即施工时间的比例（0到1之间）
    public float GetConstructionTimerNormalized()
    {
        return constructionTimer / constructionTimerMax;
    }
}
