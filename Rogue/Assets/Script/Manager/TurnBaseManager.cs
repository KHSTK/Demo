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
    public ObjectEventSO enemyTurnEndEvent;
    private void Update()
    {
        if (battleEnd == true) return;
        if (isEnemyTurn)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= enemyTurnDuration)
            {
                timeCounter = 0f;
                //敌人回合结束，切换到玩家回合
                EnemyTurnEnd();
                Debug.Log("敌人回合结束");
                isPlayerTurn = true;
            }
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
        isEnemyTurn = false;
        enemyTurnEndEvent.RaiseEvent(null, this);
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
}
