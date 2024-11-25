using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("地图布局")]
    public MapLayoutSO mapLayout;
    [Header("广播")]
    public ObjectEventSO gameWinEvent;
    public ObjectEventSO gameOverEvent;

    /// <summary>
    /// 更新地图布局数据
    /// </summary>
    /// <param name="roomVector"></param>
    public void UpdateMapLayoutData(object roomVectorInt2)
    {
        if (roomVectorInt2 is Vector2Int)
        {
            var roomVector = (Vector2Int)roomVectorInt2;
            //获取当前房间数据
            var currentRoom = mapLayout.mapRoomDataList.Find(r => r.column == roomVector.x && r.line == roomVector.y);
            //更新当前房间数据
            currentRoom.roomState = RoomState.Visited;
            //更新相邻房间的数据
            var sameColumnRoom = mapLayout.mapRoomDataList.FindAll(r => r.column == currentRoom.column);
            foreach (var room in sameColumnRoom)
            {
                if (room.roomState != RoomState.Visited)
                {
                    room.roomState = RoomState.Locked;
                }
            }
            //更新相邻房间的数据
            foreach (var link in currentRoom.linkTo)
            {
                var linkRoom = mapLayout.mapRoomDataList.Find(r => r.column == link.x && r.line == link.y);
                linkRoom.roomState = RoomState.Attainable;
            }
        }
    }
    public void CharacterDeadEvent(object character)
    {
        if (character is Player)
        {
            //玩家死亡
            Debug.Log("玩家死亡,发出失败通知");
            gameOverEvent?.RaiseEvent(null, this);
        }
        else
        {
            //敌人死亡
            Debug.Log("敌人死亡，发出胜利通知");
            gameWinEvent?.RaiseEvent(null, this);
        }

    }

}
