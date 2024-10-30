using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    //当前已经加载的场景
    [SerializeField]private GameSceneSO currentLoadedScene;
    [SerializeField] private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    float fadeTime;
    private void Awake()
    {
        //未获得场景使用异步取得场景
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentLoadedScene = firstLoadScene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
    private void OnEnable()
    {
        loadEventSO.LoadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        loadEventSO.LoadRequestEvent -= OnLoadRequestEvent;
    }
    private void OnLoadRequestEvent(GameSceneSO loactionToGo, Vector3 posToGo, bool fadeScreen)
    {
        sceneToLoad = loactionToGo;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        Debug.Log("接受事件准备卸载和加载场景"+sceneToLoad.sceneReference.SubObjectName);
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }


    }

    //卸载之前场景
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //实现淡入淡出
        }
        yield return new WaitForSeconds(fadeTime);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        LoadNewScene();
    }
    private void LoadNewScene()
    {
        sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
}
