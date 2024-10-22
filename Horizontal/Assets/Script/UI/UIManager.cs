using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvetn;
    
    //注册订阅事件
    private void OnEnable()
    {
        healthEvetn.OnEventRaised += OnHealthEvent;
    }
    //取消注册订阅事件
    private void OnDisable()
    {
        healthEvetn.OnEventRaised -= OnHealthEvent;

    }

    private void OnHealthEvent(Character character)
    {
      var persentage= character.currentHealth/character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}
