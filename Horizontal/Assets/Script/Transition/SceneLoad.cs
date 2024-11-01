using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 firstPosition;
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;
    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;

    //当前已经加载的场景
    [SerializeField]private GameSceneSO currentLoadedScene;
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
        NewGame();
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
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
}
