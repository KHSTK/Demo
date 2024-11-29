using System;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenTreasurePanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button openTreasureButton;
    public ObjectEventSO gameWinEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        openTreasureButton = rootElement.Q<Button>("OpenTreasureButton");
        openTreasureButton.clicked += OpenTreasure;
    }

    private void OpenTreasure()
    {
        gameWinEvent.RaiseEvent(null, this);
    }
}
