using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject newGameButton;
    private void OnEnable()
    {
        //只有一个EventSystem
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }
    //退出游戏固定写法
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
