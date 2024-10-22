using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("�¼�����")]
    public CharacterEventSO healthEvetn;
    
    //ע�ᶩ���¼�
    private void OnEnable()
    {
        healthEvetn.OnEventRaised += OnHealthEvent;
    }
    //ȡ��ע�ᶩ���¼�
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
