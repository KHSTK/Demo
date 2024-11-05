using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvetn;
    public SceneLoadEventSO sceneUnloadEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;
    public VoidEventSO backToMenu;
    [Header("��ȡ���")]
    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject mobileTouch;

    private void Awake()
    {
        //����ģʽ
#if UNITY_STANDALONE
        mobileTouch.SetActive(false);
#endif
    }
    //ע�ᶩ���¼�
    private void OnEnable()
    {
        healthEvetn.OnEventRaised += OnHealthEvent;
        sceneUnloadEvent.LoadRequestEvent += OnUnloadEvent;
        loadDataEvent.OnEventRaised += OnLoadEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
        backToMenu.OnEventRaised += OnLoadEvent;
    }
    //ȡ��ע�ᶩ���¼�
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
