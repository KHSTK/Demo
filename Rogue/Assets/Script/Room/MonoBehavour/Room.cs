using UnityEngine;

public class Room : MonoBehaviour
{
    //����
    public int column;
    //����
    public int line;
    private SpriteRenderer spriteRenderer;
    public RoomDataSo roomData;
    public RoomState roomState;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        SetupRoom(0, 0, roomData);
    }
    private void OnMouseDown()
    {
        //�������¼�
        Debug.Log("�������" + roomData.roomType);
    }
    /// <summary>
    /// �ⲿ��������ʱ����
    /// </summary>
    /// <param name="colum">����</param>
    /// <param name="line">����</param>
    /// <param name="roomData">��������</param>
    public void SetupRoom(int colum,int line, RoomDataSo roomData)
    {
        this.column = column;
        this.line = line;
        this.roomData = roomData;
        spriteRenderer.sprite = roomData.roomIcon;
    }
}
