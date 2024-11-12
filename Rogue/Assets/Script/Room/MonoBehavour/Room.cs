using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //哪列
    public int column;
    //哪行
    public int line;
    private SpriteRenderer spriteRenderer;
    public RoomDataSo roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo = new ();
    [Header("广播")]
    public ObjectEventSO loadRoomEvent;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        //处理点击事件
        // Debug.Log("点击房间" + roomData.roomType);
        if (roomState == RoomState.Attainable)
        {
            loadRoomEvent.RaiseEvent(this, this);
        }
    }
    /// <summary>
    /// 外部创建房间时传入
    /// </summary>
    /// <param name="colum">列数</param>
    /// <param name="line">行数</param>
    /// <param name="roomData">房间数据</param>
    public void SetupRoom(int column, int line, RoomDataSo roomData)
    {
        this.column = column;
        this.line = line;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
        spriteRenderer.color = roomState switch
        {
            RoomState.Attainable => Color.white,
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
            RoomState.Visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
        };
    }
}
