using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "MapLayoutSO", menuName = "Map/MapLayoutSO", order = 0)]
[System.Serializable]
public class MapLayoutSO : ScriptableObject
{
    public List<MapRoomData> mapRoomDataList = new();
    public List<LinePos> mapLineDataList = new();

}

[System.Serializable]
//房间地图数据类
public class MapRoomData
{
    public float posX, posY;
    public int column, line;
    public RoomDataSo roomData;
    public RoomState roomState;
    public List<Vector2Int> linkTo = new();
}

[System.Serializable]
//连线数据
public class LinePos
{
    public SerializeVector3 satrPos, endPos;
}