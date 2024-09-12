using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    private BuildingType buildingType;  // ����������Ϣ
    private float constructionTimer;  // ʩ����ʱ������ʾ����Ҫ����ʱ����ɽ���
    private float constructionTimerMax;  // ʩ����ʱ�������ֵ
    private BoxCollider2D boxCollider2D;  // ���ڼ����ײ�����
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();  // ��ȡ��ײ�����
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        constructionTimer -= Time.deltaTime;

        // ���ʩ����ʱ��С�ڵ���0����ʾ�����Ѿ����
        if (constructionTimer <= 0f)
        {
            // ����������
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);

            // ���ٽ���ʩ������
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingType buildingType)
    {
        this.buildingType = buildingType;

        // ����ʩ����ʱ�������ֵ
        constructionTimerMax = buildingType.constructionTimerMax;

        // ��ʼ��ʩ����ʱ��
        constructionTimer = constructionTimerMax;

        // ������ײ���ƫ�ƺʹ�С���뽨����Ԥ���屣��һ��
        boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
        //ͼƬ���Ž�����Ԥ�����޸�
        spriteRenderer.sprite = buildingType.sprite;
    }

    public static BuildingConstruction Create(Vector3 position, BuildingType buildingType)
    {
        // ���ؽ���ʩ��Ԥ����
        Transform pfBuildingConstruction = Resources.Load<Transform>("����ʩ��ģ��");

        // ʵ��������ʩ������
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();

        // ���ý�������
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }
    // ����ʩ����ʱ���ı�׼��ֵ����ʩ��ʱ��ı�����0��1֮�䣩
    public float GetConstructionTimerNormalized()
    {
        return constructionTimer / constructionTimerMax;
    }
}
