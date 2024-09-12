using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    private RectTransform rectTransform;

    // �ı������������ʾ��ʾ����
    private TextMeshProUGUI textMeshPro;
    // ���� UI �������
    private RectTransform backgroundRectTransform;

    private TooltipTimer tooltipTimer;

    public static TooltipUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        // ���Ҳ���ȡ�ı�����ͱ��� UI �������
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        // ��ȡ��굱ǰ����
        Vector2 mousePosition = Input.mousePosition;

        // ���������ת��ΪCanvas�ڵ�����
        RectTransform canvasRectTransform = rectTransform.parent as RectTransform;
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, mousePosition, null, out canvasPosition);
        // ��ȡUI����ĸ߶�
        float uiElementHeight = rectTransform.rect.height;

        // ����Y���꣬ʹUI������ʾ������Ϸ�
        canvasPosition.y += uiElementHeight;
        // ����UI�����λ��Ϊ���λ��
        rectTransform.localPosition = canvasPosition;

        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0) Hide();
        }

    }

    private void SetText(string tooltipText)
    {
        // ������ʾ��������
        textMeshPro.SetText(tooltipText);

        // ǿ�Ƹ����ı��������
        textMeshPro.ForceMeshUpdate();

        // ��ȡ�ı������Ⱦ��ĳߴ�
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        // ������Ⱦ��ĳߴ���±��� UI ��������ĳߴ� �ӵ�߶�����
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
