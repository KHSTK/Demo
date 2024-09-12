using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce; // �Ƿ�ֻ����һ��
    [SerializeField] private float positionOffsetY; // Y��λ��ƫ����

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��ȡ��ǰ�����SpriteRenderer���
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5f; // ���ȳ��������Ը�����Ҫ����

        // ���ݵ�ǰ�����λ�ú�Y��ƫ���������sortingOrderֵ�������丳��SpriteRenderer�����sortingOrder����
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);

        if (runOnce)
        {
            Destroy(this); // ���������ֻ����һ�Σ��������һ����������ٽű����
        }
    }

}
