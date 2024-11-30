using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Setting : MonoBehaviour
{
    private VisualElement roomElement;
    private Button backButton;
    public ObjectEventSO loadMenuEvent;
    private void OnEnable()
    {
        roomElement = GetComponent<UIDocument>().rootVisualElement;
        backButton = roomElement.Q<Button>("BackButton");
        backButton.style.display = DisplayStyle.Flex;
        backButton.text = "返回菜单";
        backButton.clickable.clicked += BackToMenu;
    }
    private void BackToMenu()
    {
        Debug.Log("Back to menu");
        loadMenuEvent.RaiseEvent(null, this);
    }
}
