using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElemet;
    private Button newGameButton, loadGameButton, quitGameButton;
    public MapLayoutSO currentMapLayout;
    public ObjectEventSO newGameEvent;
    public ObjectEventSO loadGameEvent;
    private void OnEnable()
    {
        rootElemet = GetComponent<UIDocument>().rootVisualElement;
        newGameButton = rootElemet.Q<Button>("NewGameButton");
        loadGameButton = rootElemet.Q<Button>("LoadGameButton");
        quitGameButton = rootElemet.Q<Button>("QuitGameButton");
        newGameButton.clicked += OnNewGameButtonClicked;
        loadGameButton.clicked += OnLoadGameButtonClicked;
        quitGameButton.clicked += OnQuitGameButtonClicked;
        CanLoadGameEvent();
    }

    private void OnNewGameButtonClicked()
    {
        Debug.Log("新游戏");
        newGameEvent.RaiseEvent(null, this);
    }

    private void OnLoadGameButtonClicked()
    {
        Debug.Log("继续游戏");
        loadGameEvent.RaiseEvent(null, this);
    }
    private void OnQuitGameButtonClicked()
    {
        Debug.Log("退出游戏");
        Application.Quit();
    }
    public void CanLoadGameEvent()
    {
        bool canLoadGame = currentMapLayout.mapRoomDataList.Count > 0;
        Debug.Log("CanLoadGameEvent:" + canLoadGame);
        if (canLoadGame)
        {
            loadGameButton.SetEnabled(true);
        }
        else
        {
            loadGameButton.SetEnabled(false);
        }
    }

}
