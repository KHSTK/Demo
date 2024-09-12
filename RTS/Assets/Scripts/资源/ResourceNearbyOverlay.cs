using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        // ��ȡ������Դ������
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        // ������Դ����ռ�����Դ���İٷֱȣ���ȡ����ֵ
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);

        // �ڽ�������ʾ�ٷֱ��ı�
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        // ��¼��Դ�����������ݣ��Ա����ʹ��
        this.resourceGeneratorData = resourceGeneratorData;

        if (resourceGeneratorData.resourceType)
        {
            // ������ʾ�ý���
            gameObject.SetActive(true);

            // ����ͼ��� SpriteRenderer
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        }
        else {
            gameObject.SetActive(false);
        }

    }

    public void Hide()
    {
        // ���ظý���
        gameObject.SetActive(false);
    }
}

