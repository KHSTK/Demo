using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour,ISaveable
{
    public Transform playerTransform;
    public Vector3 firstPosition;
    public Vector3 menuPosition;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public VoidEventSO newGame;
    public VoidEventSO backToMenu;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;
    public SceneLoadEventSO unloadSceneEventSO;

    //当前已经加载的场景
    [Header("场景")]
    public GameSceneSO firstLoadScene;
    public GameSceneSO menuScene;
    private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;

    public float fadeTime;
    private void Awake()
    {
        //未获得场景使用异步取得场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadedScene = firstLoadScene;
        //currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

    }
    private void Start()
    {
        loadEventSO.RaisedLoadRequestEvent(menuScene, menuPosition, true);

        //NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
        newGame.OnEventRaised += NewGame;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
        backToMenu.OnEventRaised += OnBackToMenu;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
        newGame.OnEventRaised -= NewGame;
        ISaveable saveable = this;
        saveable.UnRegisterSaveData();
        backToMenu.OnEventRaised -= OnBackToMenu;
    }

    private void OnBackToMenu()
    {
        sceneToLoad = menuScene;
        loadEventSO.RaisedLoadRequestEvent(sceneToLoad, menuPosition, true);
    }

    private void NewGame()
    {
        playerTransform.gameObject.SetActive(true);
        sceneToLoad = firstLoadScene;
        //OnLoadRequestEvent(sceneToLoad, firstPosition, true);
        loadEventSO.RaisedLoadRequestEvent(sceneToLoad, firstPosition, true);
    }
    /// <summary>
    /// 场景加载事件请求
    /// </summary>
    /// <param name="loactionToGo">将要加载的场景</param>
    /// <param name="posToGo">将要传送的点</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    private void OnLoadRequestEvent(GameSceneSO loactionToGo, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading) return;
        isLoading = true;
        sceneToLoad = loactionToGo;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        Debug.Log("接受事件准备卸载和加载场景"+sceneToLoad.sceneReference.SubObjectName);
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            //若为第一个场景则直接加载场景
            LoadNewScene();
        }


    }

    //卸载之前场景
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            fadeEvent.FadeIn(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime);
        //广播事件
        unloadSceneEventSO.RaisedLoadRequestEvent(sceneToLoad, positionToGo, fadeScreen);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        playerTransform.gameObject.SetActive(false);
        LoadNewScene();
    }
    private void LoadNewScene()
    {
       var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
        isLoading = false;
    }
    /// <summary>
    /// 场景加载完成后
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        playerTransform.position = positionToGo;
        playerTransform.gameObject.SetActive(true);
        isLoading = false;
        //场景加载完成后事件
        if(currentLoadedScene.sceneType==SceneType.Location)
        afterSceneLoadedEvent.RaiseEvent();
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeTime);
        if (fadeScreen)
        {
            fadeEvent.FadeOut(fadeTime);
        }
    }

    public DataDefinition GetDataID()
    {
        return GetComponent<DataDefinition>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveGameScene(currentLoadedScene);
    }

    public void LoadData(Data data)
    {
        var playerID = playerTransform.GetComponent<DataDefinition>().ID;
        if (data.characterPosDict.ContainsKey(playerID))
        {
            positionToGo = data.characterPosDict[playerID].ToVecort3();
            sceneToLoad = data.GetSavedScene();
            OnLoadRequestEvent(sceneToLoad, positionToGo, true);
        }
    }
}
