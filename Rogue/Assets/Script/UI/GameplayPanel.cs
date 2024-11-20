using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    [Header("事件广播")]
    public ObjectEventSO playerTurnEndEvent;

    public Transform endTurnButtonPos;
    private VisualElement rootElement;
    private Label turnLabel, energyAmountLabel, drawAmountLabel, discardAmountLabel;
    private Button endTurnButton;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        turnLabel = rootElement.Q<Label>("TurnLabel");
        energyAmountLabel = rootElement.Q<Label>("EnergyAmount");
        drawAmountLabel = rootElement.Q<Label>("DrawAmount");
        discardAmountLabel = rootElement.Q<Label>("DisAmount");
        endTurnButton = rootElement.Q<Button>("EndTurn");
        MoveToWorldPos(endTurnButton, endTurnButtonPos.position, Vector3.zero);

        endTurnButton.clicked += OnEndTurnButtonClicked;
        energyAmountLabel.text = "0";
        drawAmountLabel.text = "0";
        discardAmountLabel.text = "0";
        turnLabel.text = "开始游戏";
    }

    private void OnEndTurnButtonClicked()
    {
        playerTurnEndEvent.RaiseEvent(null, this);
    }

    private void MoveToWorldPos(VisualElement element, Vector3 worldPos, Vector3 size)
    {
        Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(element.panel, worldPos, size, Camera.main);
        element.transform.position = rect.position;
    }
    public void UpdateDrawAmount(int amount)
    {
        drawAmountLabel.text = amount.ToString();
    }
    public void UpdateDiscardAmount(int amount)
    {
        discardAmountLabel.text = amount.ToString();
    }
}
