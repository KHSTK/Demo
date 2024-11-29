using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayPanel : MonoBehaviour
{
    [Header("事件广播")]
    public ObjectEventSO playerTurnEndEvent;

    public Transform endTurnButtonPos;
    private VisualElement rootElement, energyElement;
    private Label turnLabel, energyAmountLabel, drawAmountLabel, discardAmountLabel;
    private Button endTurnButton;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        energyElement = rootElement.Q<VisualElement>("EnergyElement");
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
        endTurnButton.SetEnabled(false);

    }

    private void OnEndTurnButtonClicked()
    {
        Debug.Log("玩家回合结束");
        playerTurnEndEvent.RaiseEvent(null, this);

    }
    public void OnPlayerTurnStart()
    {
        Debug.Log("玩家回合开始");
        turnLabel.text = "玩家回合";
        turnLabel.style.color = new StyleColor(new Color(0.4f, 0.8f, 0.2f));
        endTurnButton.SetEnabled(true);
        rootElement.pickingMode = PickingMode.Ignore;
    }
    public void OnEnemyTurnStart()
    {
        Debug.Log("敌人回合开始");
        turnLabel.text = "敌人回合";
        turnLabel.style.color = new StyleColor(new Color(1f, 0.5f, 0.5f));
        endTurnButton.SetEnabled(false);
        rootElement.pickingMode = PickingMode.Position;
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
    public void UpdateEnergyAmount(int amount)
    {
        energyAmountLabel.text = amount.ToString();
    }
    public void OnGameWinEvent()
    {
        endTurnButton.SetEnabled(false);
    }
}
