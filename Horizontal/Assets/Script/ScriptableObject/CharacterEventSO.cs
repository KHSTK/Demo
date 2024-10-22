using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Event/SprictableObjectSO")]
public class CharacterEventSO : ScriptableObject
{
    //�����¼�
    public UnityAction<Character> OnEventRaised;

    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
