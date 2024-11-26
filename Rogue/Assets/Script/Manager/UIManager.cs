using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("面板")]
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject pickCardPanel;
    public void OnLoadRoomEvent(object obj)
    {
        Room currentRoom = obj as Room;
        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Shop:
                break;
            case RoomType.Treasure:
                break;
            case RoomType.RestRoom:
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

    }
    public void OnGameWinEvent()
    {
        gamePanel?.SetActive(false);
        gameWinPanel?.SetActive(true);
    }
    public void OnGameOverEvent()
    {
        gamePanel?.SetActive(false);
        gameOverPanel?.SetActive(true);
    }
    public void OnPickCardEvent()
    {
        pickCardPanel?.SetActive(true);
    }
    public void OnPickCardOverEvent()
    {
        pickCardPanel?.SetActive(false);
    }
}
