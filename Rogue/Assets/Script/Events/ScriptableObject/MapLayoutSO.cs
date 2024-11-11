using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/MapLayoutSO", order = 0)]
public class MapLayoutSO : ScriptableObject {
    public List<MapRoomData> mapRoomDataList=new();
    public List<LinePos> mapLineDataList=new();

}

[System.Serializable]
//房间地图数据类
public class MapRoomData {
    public float posX,posY;
    public int column,line;
    public RoomDataSo roomData;
    public RoomState roomState;
}

[System.Serializable]
//连线数据
public class LinePos{
    public SerializeVector3 satrPos,endPos;
    public LineType lineType;
}