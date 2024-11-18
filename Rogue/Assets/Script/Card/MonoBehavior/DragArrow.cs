using UnityEngine;

public class DragArrow : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 mousePos;
    public int pointsCount;
    public float arcModifier;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, 10));
        SetArrowPosition();
    }

    public void SetArrowPosition()
    {
        Vector3 cardPos = transform.position;//卡牌位置
        Vector3 direction = mousePos - cardPos;//卡牌指向鼠标的方向
        Vector3 normalizedDirection = direction.normalized;//标准化方向向量
        //垂直于方向向量的向量
        Vector3 perpendicular = new(-normalizedDirection.y, normalizedDirection.x, normalizedDirection.z);
        Vector3 offset = perpendicular * arcModifier;//偏移量
        Vector3 controlPoint = (cardPos + mousePos) / 2 + offset;//控制点
        lineRenderer.positionCount = pointsCount;//设置点的数量
        for (int i = 0; i < pointsCount; i++)
        {
            float t = i / (float)(pointsCount - 1);
            Vector3 point = CalculateQuadraticBezierPoint(t, cardPos, controlPoint, mousePos);
            lineRenderer.SetPosition(i, point);
        }
    }

    /// <summary>
    /// 计算二次贝塞尔曲线上的点
    /// </summary>
    /// <param name="t"></param>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;//第一项
        p += 2 * u * t * p1;//第二项
        p += tt * p2;//第三项
        return p;//返回t时间点对应的贝塞尔曲线上的点坐标
    }
}
