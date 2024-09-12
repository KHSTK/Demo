using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameobject;
    public SpriteRenderer sp;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    // 初始时隐藏建筑物
    private void Awake()
    {
        spriteGameobject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("效率").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    // 监听BuildingManager中的事件
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    // 处理BuildingManager中的事件
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType.prefab == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
        }
    }

    // 每帧更新建筑物的位置
    private void Update()
    {
        transform.position = Utilsclass.GetMouseWorldPosition();
    }

    // 显示建筑物
    private void Show(Sprite ghostSprite)
    {
        spriteGameobject.SetActive(true);
        SpriteRenderer spriteRenderer = spriteGameobject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ghostSprite;
        //实现透明
        Color color = spriteRenderer.color;
        color.a = 0.7f;
        spriteRenderer.color = color;
    }

    // 隐藏建筑物
    private void Hide()
    {
        spriteGameobject.SetActive(false);
    }

}
