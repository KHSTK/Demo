using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvetn;
    public SceneLoadEventSO sceneLoadEvent;
    
    //ע�ᶩ���¼�
    private void OnEnable()
    {
        healthEvetn.OnEventRaised += OnHealthEvent;
        sceneLoadEvent.LoadRequestEvent += OnLoadEvent;
    }
    //ȡ��ע�ᶩ���¼�
    private void OnDisable()
    {
        healthEvetn.OnEventRaised -= OnHealthEvent;
        sceneLoadEvent.LoadRequestEvent -= OnLoadEvent;

    }


    private void OnHealthEvent(Character character)
    {
      var persentage= character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
        playerStatBar.OnPowerChange(character);
    }
    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }
}
