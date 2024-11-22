using UnityEngine;

[CreateAssetMenu(menuName = "CardEffect/DefenseEffect")]
public class DefenseEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        //对使用目标自己使用
        if (targetType == EffcetTargetType.Self)
        {
            from.UpdateDefense(value);
        }
        if (targetType == EffcetTargetType.Target)
        {
            target.UpdateDefense(value);
        }
    }
}
