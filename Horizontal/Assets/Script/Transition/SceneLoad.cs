using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    //��ǰ�Ѿ����صĳ���
    [SerializeField]private GameSceneSO currentLoadedScene;
    [SerializeField] private GameSceneSO sceneToLoad;
    private Vector3 positionToGo;
    private bool fadeScreen;

    float fadeTime;
    private void Awake()
    {
        //δ��ó���ʹ���첽ȡ�ó���
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
        Debug.Log("�����¼�׼��ж�غͼ��س���"+sceneToLoad.sceneReference.SubObjectName);
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }


    }

    //ж��֮ǰ����
    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            //ʵ�ֵ��뵭��
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
