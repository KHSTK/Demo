using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadMananger : MonoBehaviour
{
    private AssetReference currentScene;
    public GameObject setting;
    public AssetReference map;
    public AssetReference menu;
    public GameObject fadePanel;
    private Vector2Int currentRoomVector;
    private Room currentRoom;
    [Header("广播")]
    public ObjectEventSO roomLoadUpdateEvent;
    public ObjectEventSO AfterRoomloadEvent;
    private void Awake()
    {
        currentRoomVector = Vector2Int.one * -1;
        LoadMenu();
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
        //房间加载完成事件
        AfterRoomloadEvent.RaiseEvent(currentRoom, this);
    }
    public void GameWinEvent()
    {
        //传递当前房间坐标信息
        roomLoadUpdateEvent.RaiseEvent(currentRoomVector, this);
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
            fadePanel.GetComponent<FadePanel>().FadeOut(0.4f);
            fadePanel.SetActive(false);
            Debug.Log("加载成功");
            SceneManager.SetActiveScene(s.Result.Scene);
        }

    }
    private async Awaitable UnloadSceneTask()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<FadePanel>().FadeIn(0.4f);
        //等待一段事件后执行
        await Awaitable.WaitForSecondsAsync(0.4f);
        //完全加载场景后继续
        await Awaitable.FromAsyncOperation(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }
    /// <summary>
    /// 监听返回的房间事件函数
    /// </summary>
    public async void LoadMap()
    {
        if (SceneManager.GetActiveScene().name != "Persistent")
        {
            await UnloadSceneTask();
        }
        currentScene = map;
        setting.SetActive(true);
        await LoadSceneTask();
    }
    public async void LoadMenu()
    {
        if (SceneManager.GetActiveScene().name != "Persistent" && currentScene != null)
        {
            await UnloadSceneTask();
        }
        currentScene = menu;
        setting.SetActive(false);
        await LoadSceneTask();
    }
    public void NewGameEvent()
    {
        LoadMap();
    }
}
