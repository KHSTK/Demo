using UnityEngine;
[CreateAssetMenu(menuName = "CardEffect/HealEffect")]
public class HealEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (targetType == EffcetTargetType.Self)
        {
            from.HealHealth(value);
        }
        else
        {
            target.HealHealth(value);
        }
    }
}
