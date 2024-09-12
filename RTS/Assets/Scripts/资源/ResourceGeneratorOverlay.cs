using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField]
    private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start()
    {
        // ��ȡ��Դ������������
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        // ���Ҳ����ý������� Transform
        barTransform = transform.Find("bar");

        // ���Ҳ�����ͼ��� SpriteRenderer
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        // ���Ҳ������ı��� TextMeshPro �������ʾÿ�����ɵ�����������һλС����
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        // ���½����������ű��������ݵ�ǰ��ʱ���Ĺ�һ��ֵȷ��
        barTransform.localScale = new Vector3(resourceGenerator.GetTimerNormalized(), barTransform.localScale.y, 1);
    }

}
