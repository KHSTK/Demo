using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvetn;
    public SceneLoadEventSO sceneUnloadEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenu;
    [Header("获取组件")]
    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject mobileTouch;

    private void Awake()
    {
        //主机模式
#if UNITY_STANDALONE
        mobileTouch.SetActive(false);
#endif
    }
    //注册订阅事件
    private void OnEnable()
    {
        healthEvetn.OnEventRaised += OnHealthEvent;
        sceneUnloadEvent.LoadRequestEvent += OnUnloadEvent;
        loadDataEvent.OnEventRaised += OnLoadEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenu.OnEventRaised += OnLoadEvent;
    }
    //取消注册订阅事件
    private void OnDisable()
    {
        healthEvetn.OnEventRaised -= OnHealthEvent;
        sceneUnloadEvent.LoadRequestEvent -= OnUnloadEvent;
        loadDataEvent.OnEventRaised -= OnLoadEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
        backToMenu.OnEventRaised -= OnLoadEvent;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        mobileTouch.SetActive(false);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    private void OnLoadEvent()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnHealthEvent(Character character)
    {
      var persentage= character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
        playerStatBar.OnPowerChange(character);
    }
    private void OnUnloadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
        mobileTouch.SetActive(!isMenu);
    }
}
