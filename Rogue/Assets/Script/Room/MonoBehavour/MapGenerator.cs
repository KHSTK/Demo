using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfig;
    [Header("预制体")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    private float screenHeight;
    private float screenWidth;
    private float columnWidth;
    private Vector3 generatePoint;
    public float border;
    private List<Room> rooms;
    private List<LineRenderer> lines;
    private void Awake()
    {
        //屏幕高为摄像机大小的两倍
        screenHeight = Camera.main.orthographicSize * 2;
        //屏幕宽为高*分辨率比
        screenWidth = screenHeight * Camera.main.aspect;
        columnWidth = screenWidth / (mapConfig.roomBlueprints.Count+0.5f);
        rooms = new List<Room>();
        lines=new List<LineRenderer>();
    }

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        //创建前一列房间的列表
        List<Room> previousColumn = new List<Room>();

        //循环生成每列房间
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var blueprint = mapConfig.roomBlueprints[column];
            //随机房间数
            var amount = Random.Range(blueprint.min, blueprint.max+1);
            //每行初始房间高
            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            //每行初始房间坐标generatePoint
            generatePoint = new Vector3(-screenWidth/2+border+column*columnWidth, startHeight, 0);
            //每个房间坐标
            var newPosition = generatePoint;

            //当前列房间列表
            List<Room> currentColumn = new List<Room>();
            //每行间距
            var roomGapY = screenHeight / (amount + 1);
            //循环生成每行房间
            for (int i = 0; i < amount; i++)
            {
                //最后一个房间固定x轴
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                //随机偏移
                else if (column != 0)
                {
                   newPosition.x = generatePoint.x+ Random.Range(-border/2, border/2);
                }
                newPosition.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab,newPosition,Quaternion.identity, transform);
                rooms.Add(room);
                currentColumn.Add(room);
            }
            //判断是否为第一列，如果不是则连接到上一列
            if(previousColumn.Count>0)
            {
                //创建房间连线
                ConnectRooms(previousColumn, currentColumn);
            }
            
            //记录当前列
            previousColumn = currentColumn;
        }
    }

    private void ConnectRooms(List<Room> colum1, List<Room> colum2)
    {
        //记录已连接房间
        HashSet<Room> connectedColumn2Rooms = new HashSet<Room>();
        foreach (var room in colum1)
        {
            //第一个房间和第二列随机房间产生链接
            var tagerRoom=ConnectToRandomRoom(room,colum2);
            //哈希表记录已连接房间
            connectedColumn2Rooms.Add(tagerRoom);
        }
        //遍历第二列房间，如果未连接则随机连接到第一列
        foreach (var room in colum2){
            if(!connectedColumn2Rooms.Contains(room)){
                ConnectToRandomRoom(room, colum1);
            }
        }
    }

    private Room ConnectToRandomRoom(Room room1, List<Room> colum2)
    {
        Room targetRoom;
        //随机选择第二列房间
        targetRoom = colum2[Random.Range(0, colum2.Count)];
        //创建房间之间的连线
        var line =Instantiate(linePrefab,transform);
        line.SetPosition(0, room1.transform.position);
        line.SetPosition(1, targetRoom.transform.position);

        lines.Add(line);
        return targetRoom;
    }

    [ContextMenu(itemName: "ReGenerateRoom")]
    //重新生成地图
    public void ReGenerateRoom()
    {
        //销毁游戏物体
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        foreach (var line in lines){
            Destroy(line.gameObject);
        }
        //清空列表
        lines.Clear();
        rooms.Clear();
        CreateMap();
    }
}
