using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置表")]
    public MapConfigSO mapConfig;
    [Header("地图布局")]
    public MapLayoutSO mapLayout;
    [Header("预制体")]
    public Room roomPrefab;
    public LineRenderer linePrefab;

    private float screenHeight;
    private float screenWidth;
    private float columnWidth;
    private Vector3 generatePoint;
    public float border;
    private List<Room> rooms=new();
    private List<LineRenderer> lines=new();
    //房间数据
    public List<RoomDataSo> roomDataList=new();
    private Dictionary<RoomType, RoomDataSo> roomDataDict=new();
    private void Awake()
    {
        //屏幕高为摄像机大小的两倍
        screenHeight = Camera.main.orthographicSize * 2;
        //屏幕宽为高*分辨率比
        screenWidth = screenHeight * Camera.main.aspect;
        columnWidth = screenWidth / (mapConfig.roomBlueprints.Count+0.5f);

        foreach(var roomData in roomDataList)
        {
            roomDataDict.Add(roomData.roomType, roomData);
        }
    }

    // private void Start()
    // {
    //     CreateMap();
    // }
    private void OnEnable() {
        if(mapLayout.mapRoomDataList.Count>0){
            LoadMap();
        }else{
            CreateMap();
        }
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
            var amount = UnityEngine.Random.Range(blueprint.min, blueprint.max+1);
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
                   newPosition.x = generatePoint.x+ UnityEngine.Random.Range(-border/2, border/2);
                }
                newPosition.y = startHeight - roomGapY * i;
                //生成房间
                var room = Instantiate(roomPrefab,newPosition,Quaternion.identity, transform);
                RoomType newType= GetRandomRoomType(mapConfig.roomBlueprints[column].roomType);

                room.SetupRoom(column,i,GetRoomDataSo(newType));
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
    
        SaveMap();
    }

    private void ConnectRooms(List<Room> column1, List<Room> column2){
        int nextRoom = 0;
        for(int i=0;i<column1.Count;i++){
            int maxConnections = column2.Count - nextRoom;
            int randomConnections = UnityEngine.Random.Range(1, Mathf.Max(1, maxConnections));
            if(i== column1.Count-1){
                randomConnections=column2.Count - nextRoom;
            }
            for(int j= nextRoom; j <nextRoom+randomConnections; j++){
                var line = Instantiate(linePrefab, transform);
                line.SetPosition(0, column1[i].transform.position);
                line.SetPosition(1, column2[j].transform.position);
                lines.Add(line);
            }
            nextRoom = Mathf.Min(nextRoom + randomConnections, column2.Count)-1;
        }

    }


    // private void ConnectRooms(List<Room> colum1, List<Room> colum2)
    // {
    //     //记录已连接房间
    //     HashSet<Room> connectedColumn2Rooms = new HashSet<Room>();
    //     foreach (var room in colum1)
    //     {
    //         //第一个房间和第二列随机房间产生链接
    //         var tagerRoom=ConnectToRandomRoom(room,colum2,true);
    //         //哈希表记录已连接房间
    //         connectedColumn2Rooms.Add(tagerRoom);
    //     }
    //     //遍历第二列房间，如果未连接则随机连接到第一列
    //     foreach (var room in colum2){
    //         if(!connectedColumn2Rooms.Contains(room)){
    //             ConnectToRandomRoom(room, colum1,false);
    //         }
    //     }

    // }


    // private Room ConnectToRandomRoom(Room room1, List<Room> colum2)
    // {
    //     Room targetRoom;
    //     随机选择第二列房间
    //     targetRoom = colum2[UnityEngine.Random.Range(0, colum2.Count)];
    //     创建房间之间的连线
    //     var line =Instantiate(linePrefab,transform);
    //     line.SetPosition(0, room1.transform.position);
    //     line.SetPosition(1, targetRoom.transform.position);

    //     lines.Add(line);
    //     return targetRoom;
    // }

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
    private RoomDataSo GetRoomDataSo(RoomType roomType)
    {
        return roomDataDict[roomType];
    }
    
    /// <summary>
    /// 根据枚举随机返回一个房间类型
    /// </summary>
    /// <param name="flages"></param>
    /// <returns></returns>
    private RoomType GetRandomRoomType(RoomType flages)
    {
        string[] options=flages.ToString().Split(',');
        string randomOption = options[UnityEngine.Random.Range(0, options.Length)];
        RoomType  roomType = (RoomType)Enum.Parse(typeof(RoomType), randomOption);
        return roomType;
    }

    private void SaveMap(){
        mapLayout.mapRoomDataList=new();
        //添加所有已生成房间
        for(int i = 0; i < rooms.Count; i++)
        {
            var room =new MapRoomData(){
                posX=rooms[i].transform.position.x,
                posY=rooms[i].transform.position.y,
                column=rooms[i].column,
                line=rooms[i].line,
                roomData=rooms[i].roomData,
                roomState=rooms[i].roomState
            };
            mapLayout.mapRoomDataList.Add(room);
        }
        mapLayout.mapLineDataList=new();
        //添加所有已生成连线
        for(int i = 0; i < lines.Count; i++){
            var line =new LinePos(){
                lineType=lines[i].GetComponent<Line>().lineType,
                satrPos=new SerializeVector3(lines[i].GetPosition(0)),
                endPos=new SerializeVector3(lines[i].GetPosition(1))
            };
            mapLayout.mapLineDataList.Add(line);
        }
    }
    private void LoadMap(){
        //读取房间数据生成房间
        var newMapRoomDataList=mapLayout.mapRoomDataList;
        for (int i = 0; i < newMapRoomDataList.Count; i++){
            var newPos=new Vector3(newMapRoomDataList[i].posX,newMapRoomDataList[i].posY,0);
            var newroom = Instantiate(roomPrefab, newPos, Quaternion.identity, transform);
            newroom.roomState = newMapRoomDataList[i].roomState;
            newroom.SetupRoom(newMapRoomDataList[i].column, newMapRoomDataList[i].line,newMapRoomDataList[i].roomData);
            rooms.Add(newroom);
        }
        //读取连线数据生成连线
        var newMapLineDataList=mapLayout.mapLineDataList;
        for (int i = 0; i < newMapLineDataList.Count; i++){
            var line =Instantiate(linePrefab,transform);
            line.GetComponent<Line>().lineType = newMapLineDataList[i].lineType;
            line.SetPosition(0,newMapLineDataList[i].satrPos.ToVector3());
            line.SetPosition(1,newMapLineDataList[i].endPos.ToVector3());
            lines.Add(line);
        }
    }

}
