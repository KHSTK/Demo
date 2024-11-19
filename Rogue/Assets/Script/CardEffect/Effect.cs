using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public int value;
    public EffcetTargetType targetType;
    public abstract void Execute(CharacterBase from, CharacterBase target);

}
