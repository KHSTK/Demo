using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachinevirtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;

    // 获取初始的正交大小
    private void Start()
    {
        orthographicSize = cinemachinevirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    // 处理摄像机移动
    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 20f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    // 处理摄像机缩放
    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrthographicSize += Input.mouseScrollDelta.y * zoomAmount*-1;
        float minOrthographicSize = 10;
        float maxOrthographicSize = 20;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        // 设置摄像机的正交大小
        cinemachinevirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
