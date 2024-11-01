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
    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;
    [Header("�㲥")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;

    //��ǰ�Ѿ����صĳ���
    [SerializeField]private GameSceneSO currentLoadedScene;
    private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;
    private bool isLoading;

    public float fadeTime;
    private void Awake()
    {
        //δ��ó���ʹ���첽ȡ�ó���
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
    /// ���������¼�����
    /// </summary>
    /// <param name="loactionToGo">��Ҫ���صĳ���</param>
    /// <param name="posToGo">��Ҫ���͵ĵ�</param>
    /// <param name="fadeScreen">�Ƿ��뽥��</param>
    private void OnLoadRequestEvent(GameSceneSO loactionToGo, Vector3 posToGo, bool fadeScreen)
    {
        if (isLoading) return;
        isLoading = true;
        sceneToLoad = loactionToGo;
        positionToGo = posToGo;
        this.fadeScreen = fadeScreen;
        Debug.Log("�����¼�׼��ж�غͼ��س���"+sceneToLoad.sceneReference.SubObjectName);
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        else
        {
            //��Ϊ��һ��������ֱ�Ӽ��س���
            LoadNewScene();
        }


    }

    //ж��֮ǰ����
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
    /// ����������ɺ�
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        playerTransform.position = positionToGo;
        playerTransform.gameObject.SetActive(true);
        isLoading = false;
        //����������ɺ��¼�
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
