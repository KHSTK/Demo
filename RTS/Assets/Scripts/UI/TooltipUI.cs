using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private RectTransform rectTransform;

    // 文本组件，用于显示提示文字
    private TextMeshProUGUI textMeshPro;
    // 背景 UI 布局组件
    private RectTransform backgroundRectTransform;

    private TooltipTimer tooltipTimer;

    public static TooltipUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        // 查找并获取文本组件和背景 UI 布局组件
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        // 获取鼠标当前坐标
        Vector2 mousePosition = Input.mousePosition;

        // 将鼠标坐标转换为Canvas内的坐标
        RectTransform canvasRectTransform = rectTransform.parent as RectTransform;
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, null, out canvasPosition);
        // 获取UI物体的高度
        float uiElementHeight = rectTransform.rect.height;

        // 调整Y坐标，使UI物体显示在鼠标上方
        canvasPosition.y += uiElementHeight;
        // 设置UI物体的位置为鼠标位置
        rectTransform.localPosition = canvasPosition;

        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0) Hide();
        }

    }

    private void SetText(string tooltipText)
    {
        // 设置提示文字内容
        textMeshPro.SetText(tooltipText);

        // 强制更新文本组件网格
        textMeshPro.ForceMeshUpdate();

        // 获取文本组件渲染后的尺寸
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        // 根据渲染后的尺寸更新背景 UI 布局组件的尺寸 加点高度美化
        backgroundRectTransform.sizeDelta = textSize + new Vector2(8, 8);
    }

    public void Show(string tooltipText, TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;
    }

}
