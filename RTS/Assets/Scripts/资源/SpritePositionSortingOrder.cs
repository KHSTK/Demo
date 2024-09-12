using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce; // 是否只运行一次
    [SerializeField] private float positionOffsetY; // Y轴位置偏移量

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取当前对象的SpriteRenderer组件
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5f; // 精度乘数，可以根据需要调整

        // 根据当前对象的位置和Y轴偏移量计算出sortingOrder值，并将其赋给SpriteRenderer组件的sortingOrder属性
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);

        if (runOnce)
        {
            Destroy(this); // 如果设置了只运行一次，就在完成一次排序后销毁脚本组件
        }
    }

}
