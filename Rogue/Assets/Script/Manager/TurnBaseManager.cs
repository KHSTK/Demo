using UnityEngine;

public class TurnBaseManager : MonoBehaviour
{
    public GameObject playerObj;
    private bool isPlayerTurn = false;
    private bool isEnemyTurn = false;
    public bool battleEnd = true;
    private float timeCounter;
    public float enemyTurnDuration;
    public float playerTurnDuration;
    [Header("事件广播")]
    public ObjectEventSO playerTurnStartEvent;
    public ObjectEventSO enemyTurnStartEvent;
    private void Update()
    {
        if (battleEnd == true) return;
        if (isEnemyTurn)
        {

        }
        if (isPlayerTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= playerTurnDuration)
            {
                timeCounter = 0f;
                PlayerTurnStart();
                isPlayerTurn = false;
            }
        }
    }
    [ContextMenu("开始游戏")]
    public void GameStart()
    {
        battleEnd = false;
        isPlayerTurn = true;
        isEnemyTurn = false;
        timeCounter = 0f;
    }

    public void EnemyTurnEnd()
    {
        timeCounter = 0f;
        //敌人回合结束，切换到玩家回合
        Debug.Log("敌人回合结束");
        isPlayerTurn = true;
        // isEnemyTurn = false;
        // enemyTurnEndEvent.RaiseEvent(null, this);
    }
    public void PlayerTurnStart()
    {
        playerObj.GetComponent<Player>().NewTurn();
        playerTurnStartEvent.RaiseEvent(null, this);
    }
    public void EnemyTurnStart()
    {
        isEnemyTurn = true;
        enemyTurnStartEvent.RaiseEvent(null, this);
    }
    public void OnAfterRoomLoadEvent(object room)
    {

        Room currentRoom = (Room)room;
        switch (currentRoom.roomData.roomType)
        {
            case RoomType.Shop:
            case RoomType.Treasure:
                playerObj.SetActive(false);
                break;
            case RoomType.RestRoom:
                playerObj.SetActive(true);
                playerObj.GetComponent<PlayerAnimation>().SetPlayerSleep();
                break;
            case RoomType.MinorEnemy:
            case RoomType.EliteEnemy:
            case RoomType.Boss:
                playerObj.SetActive(true);
                GameStart();
                break;
        }
    }
    public void OnLoadMapEvent()
    {
        battleEnd = true;
        isPlayerTurn = false;
        isEnemyTurn = false;
        playerObj.SetActive(false);
    }
    public void NewGame()
    {
        playerObj.GetComponent<Player>().NewGame();
    }
}
