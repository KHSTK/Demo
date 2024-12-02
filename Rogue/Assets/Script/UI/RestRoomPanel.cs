using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RestRoomPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button restButton, backToMapButton;
    public Effect restEffect;
    public ObjectEventSO loadMapEvent;
    public ObjectEventSO restRoomOverEvent;
    private CharacterBase player;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        restButton = rootElement.Q<Button>("RestButton");
        backToMapButton = rootElement.Q<Button>("RestRoomToMapButton");


        player = FindAnyObjectByType<Player>(FindObjectsInactive.Include);

        restButton.clicked += OnRestButtonClicked;
        backToMapButton.clicked += OnBackToMapButtonClicked;
    }

    private void OnRestButtonClicked()
    {
        restEffect.Execute(player, player);
        restButton.SetEnabled(false);
    }
    private void OnBackToMapButtonClicked()
    {
        loadMapEvent.RaiseEvent(null, this);
        restRoomOverEvent.RaiseEvent(null, this);
    }

}
