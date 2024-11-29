using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class FadePanel : MonoBehaviour
{
    private VisualElement rootElement, fadePanel;
    private void Awake()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        fadePanel = rootElement.Q<VisualElement>("FadePanel");
    }
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        fadePanel = rootElement.Q<VisualElement>("FadePanel");
    }
    public void FadeIn(float duration)
    {
        DOVirtual.Float(0, 1f, duration, value =>
        {
            fadePanel.style.opacity = value;
        }).SetEase(Ease.Linear);
    }
    public void FadeOut(float duration)
    {
        DOVirtual.Float(1, 0, duration, value =>
        {
            fadePanel.style.opacity = value;
        }).SetEase(Ease.Linear);
    }
}
