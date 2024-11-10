using UnityEngine;

public class SceneLoadMananger : MonoBehaviour
{
    public void OnLoadRoomEvent(object data){
        if(data is RoomDataSo){
            var currentRoom = (RoomDataSo)data;
            Debug.Log(currentRoom.roomType);

        }
    }
}
