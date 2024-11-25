using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadMananger : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;
    private Vector2Int currentRoomVector;
    private Room currentRoom;
    [Header("广播")]
    public ObjectEventSO roomLoadUpdateEvent;
    public ObjectEventSO AfterRoomloadEvent;


    private void Start()
    {
        currentRoomVector = Vector2Int.one * -1;
    }

    /// <summary>
    /// 在房间加载事件中监听
    /// </summary>
    /// <param name="data"></param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            currentRoom = data as Room;
            var currentData = currentRoom.roomData;
            currentRoomVector = new(currentRoom.column, currentRoom.line);
            currentScene = currentData.senceToLoad;
        }
        //卸载场景
        await UnloadSceneTask();

        //加载房间
        await LoadSceneTask();
        //传递当前房间坐标信息
        roomLoadUpdateEvent.RaiseEvent(currentRoomVector, this);
        //房间加载完成事件
        AfterRoomloadEvent.RaiseEvent(currentRoom, this);
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    private async Awaitable LoadSceneTask()
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("加载成功");
            SceneManager.SetActiveScene(s.Result.Scene);
        }

    }
    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    /// <summary>
    /// 监听返回的房间事件函数
    /// </summary>
    public async void LoadMap()
    {
        await UnloadSceneTask();
        currentScene = map;
        await LoadSceneTask();
    }
}
