using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button pickCardButton;
    private Button backMapButton;
    [Header("广播")]
    public ObjectEventSO loadMapEvent;
    public ObjectEventSO pickCardEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        pickCardButton = rootElement.Q<Button>("PickCardButton");
        backMapButton = rootElement.Q<Button>("BackMapButton");
        Debug.Log("注册点击事件");
        pickCardButton.clicked += OnClickPickCardButton;
        backMapButton.clicked += OnClickBackMapButton;
    }
    private void OnClickPickCardButton()
    {
        Debug.Log("点击了抽卡按钮");
        pickCardEvent.RaiseEvent(null, this);
    }

    private void OnClickBackMapButton()
    {
        Debug.Log("点击了返回地图按钮");
        loadMapEvent.RaiseEvent(null, this);
    }
    public void OnPickCardOverEvent()
    {
        pickCardButton.SetEnabled(false);
    }
}
