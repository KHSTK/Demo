using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private GameObject spriteGameobject;
    public SpriteRenderer sp;
    private ResourceNearbyOverlay resourceNearbyOverlay;
    // ��ʼʱ���ؽ�����
    private void Awake()
    {
        spriteGameobject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("Ч��").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    // ����BuildingManager�е��¼�
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    // ����BuildingManager�е��¼�
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

    // ÿ֡���½������λ��
    private void Update()
    {
        transform.position = Utilsclass.GetMouseWorldPosition();
    }

    // ��ʾ������
    private void Show(Sprite ghostSprite)
    {
        spriteGameobject.SetActive(true);
        SpriteRenderer spriteRenderer = spriteGameobject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ghostSprite;
        //ʵ��͸��
        Color color = spriteRenderer.color;
        color.a = 0.7f;
        spriteRenderer.color = color;
    }

    // ���ؽ�����
    private void Hide()
    {
        spriteGameobject.SetActive(false);
    }

}
