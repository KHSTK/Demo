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
        //��Ļ��Ϊ�������С������
        screenHeight = Camera.main.orthographicSize * 2;
        //��Ļ��Ϊ��*��߱�
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
        //ѭ������ÿ�з���
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var blueprint = mapConfig.roomBlueprints[column];
            //ÿ����amount������
            var amount = Random.Range(blueprint.min, blueprint.max+1);
            //��ʼ�����
            var startHeight = screenHeight / 2 - screenHeight / (amount + 1);
            //ÿ�е�һ�������λ��generatePoint
            generatePoint = new Vector3(-screenWidth/2+border+column*columnWidth, startHeight, 0);
            //����λ��
            var newPosition = generatePoint;

            //ÿ�м��
            var roomGapY = screenHeight / (amount + 1);
            //ѭ����ǰ�������������ɷ���
            for (int i = 0; i < amount; i++)
            {
                //�̶����һ������x��
                if (column == mapConfig.roomBlueprints.Count - 1)
                {
                    newPosition.x = screenWidth / 2 - border * 2;
                }
                ////���ƫ��
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
    //�������ɵ�ͼ
    public void ReGenerateRoom()
    {
        //���������ɷ���
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        //����б�
        rooms.Clear();
        CreateMap();
    }
}
