using UnityEngine;
[CreateAssetMenu(menuName = "CardEffect/StrongEffect")]
public class StrongEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        //对使用目标自己使用
        if (targetType == EffcetTargetType.Self)
        {
            from.ResetStrong(value, true);
        }
        if (targetType == EffcetTargetType.Target)
        {
            target.ResetStrong(value, false);
        }
    }
}
