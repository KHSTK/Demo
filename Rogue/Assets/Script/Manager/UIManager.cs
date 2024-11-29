using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("面板")]
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject pickCardPanel;
    public GameObject resetRoomPanel;
    public GameObject openTreasurePanel;
    public void OnLoadRoomEvent(object obj)
    {
        Room currentRoom = obj as Room;
        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Shop:
                break;
            case RoomType.Treasure:
                openTreasurePanel?.SetActive(true);
                break;
            case RoomType.RestRoom:
                resetRoomPanel?.SetActive(true);
                break;
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                gamePanel.SetActive(true);
                break;
        }
    }
    /// <summary>
    /// 加载地图或加载菜单关闭所有面板
    /// </summary>
    public void HideAllPanel()
    {
        gamePanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
        gameWinPanel?.SetActive(false);
        pickCardPanel?.SetActive(false);
        resetRoomPanel?.SetActive(false);
        openTreasurePanel?.SetActive(false);
    }
    public void OnGameWinEvent()
    {
        StartCoroutine(ClosePanel(gamePanel));
        StartCoroutine(OpenPanel(gameWinPanel));
    }
    public void OnGameOverEvent()
    {
        StartCoroutine(ClosePanel(gamePanel));
        StartCoroutine(OpenPanel(gameOverPanel));
    }
    public void OnPickCardEvent()
    {
        pickCardPanel?.SetActive(true);
    }
    public void OnPickCardOverEvent()
    {
        pickCardPanel?.SetActive(false);
    }
    IEnumerator OpenPanel(GameObject panel)
    {
        Debug.Log("OpenPanel");
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
    }
    IEnumerator ClosePanel(GameObject panel)
    {
        Debug.Log("ClosePanel");
        yield return new WaitForSeconds(1f);
        panel.SetActive(false);
    }
}
