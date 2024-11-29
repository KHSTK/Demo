using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button backToMenuButton;
    private Label overTitleLabel;
    public ObjectEventSO loadMenuEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        overTitleLabel = rootElement.Q<Label>("OverTitle");
        backToMenuButton = rootElement.Q<Button>("BackToMenuButton");
        backToMenuButton.clicked += OnBackToMenuButtonClicked;
    }

    private void OnBackToMenuButtonClicked()
    {
        loadMenuEvent?.RaiseEvent(null, this);
    }
}
