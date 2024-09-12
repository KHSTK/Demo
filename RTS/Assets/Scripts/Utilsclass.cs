using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilsclass : MonoBehaviour
{
    private static Camera mainCamera;

    // 获取鼠标在世界坐标系中的位置
    public static Vector3 GetMouseWorldPosition()
    {
        // 如果主摄像机对象为空，则获取主摄像机对象
        if (mainCamera == null)
            mainCamera = Camera.main;

        // 将鼠标当前位置从屏幕坐标系转换为世界坐标系
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 将鼠标世界位置的z坐标设置为零
        mouseWorldPosition.z = 0f;

        // 返回鼠标在世界坐标系中的位置
        return mouseWorldPosition;
    }
    public static Vector3 GetRandomDir()
    {
        Debug.Log("GetRandomDir");
        // 生成一个随机方向的单位向量
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        // 根据给定的向量计算角度（弧度转换为角度）
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

}
