using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapConfigSO mapConfig;
    public Room roomPrefab;

    private float screenHeight;
    private float screenWidth;
    private float columnWidth;
    private Vector3 generatePoint;
    public float border;
    private List<Room> rooms;
    private void Awake()
    {
        //屏幕高为摄像机大小的两倍
        screenHeight = Camera.main.orthographicSize * 2;
        //屏幕宽为高*宽高比
        screenWidth = screenHeight * Camera.main.aspect;
        columnWidth = screenWidth / (mapConfig.roomBlueprints.Count+0.5f);
        rooms = new List<Room>();
    }
    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        //循环生成每行房间
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var blueprint = mapConfig.roomBlueprints[column];
            //每列有amount个房间
            var amount = Random.Range(blueprint.min, blueprint.max+1);
            //初始房间高
            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            //每列第一个房间的位置generatePoint
            generatePoint = new Vector3(-screenWidth/2+border+column*columnWidth, startHeight, 0);
            //房间位置
            var newPosition = generatePoint;

            //每行间距
            var roomGapY = screenHeight / (amount + 1);
            //循环当前列所有数量生成房间
            for (int i = 0; i < amount; i++)
            {
                //固定最后一个房间x轴
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                ////随机偏移
                //else if (column != 0)
                //{
                //    newPosition.x = generatePoint.x+ Random.Range(-border/2, border/2);
                //}
                newPosition.y = startHeight - roomGapY * i;
                var room = Instantiate(roomPrefab,newPosition,Quaternion.identity, transform);
                rooms.Add(room);
            }
        }
    }

    [ContextMenu(itemName: "ReGenerateRoom")]
    //重新生成地图
    public void ReGenerateRoom()
    {
        //销毁已生成房间
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        //清空列表
        rooms.Clear();
        CreateMap();
    }
}
