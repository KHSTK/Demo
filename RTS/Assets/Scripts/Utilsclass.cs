using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilsclass : MonoBehaviour
{
    private static Camera mainCamera;

    // ��ȡ�������������ϵ�е�λ��
    public static Vector3 GetMouseWorldPosition()
    {
        // ��������������Ϊ�գ����ȡ�����������
        if (mainCamera == null)
            mainCamera = Camera.main;

        // ����굱ǰλ�ô���Ļ����ϵת��Ϊ��������ϵ
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // ���������λ�õ�z��������Ϊ��
        mouseWorldPosition.z = 0f;

        // �����������������ϵ�е�λ��
        return mouseWorldPosition;
    }
    public static Vector3 GetRandomDir()
    {
        Debug.Log("GetRandomDir");
        // ����һ���������ĵ�λ����
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        // ���ݸ�������������Ƕȣ�����ת��Ϊ�Ƕȣ�
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

}
