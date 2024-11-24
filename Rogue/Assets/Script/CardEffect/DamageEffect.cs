using System;
using UnityEngine;

[CreateAssetMenu(menuName = "CardEffect/DamageEffect")]
public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (target == null) return;
        var damage = Convert.ToInt32(value * from.baseStrong);
        switch (targetType)
        {
            case EffcetTargetType.Self:
                from.TakeDamage(damage);
                break;
            case EffcetTargetType.Target:
                target.TakeDamage(damage);
                break;
            case EffcetTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(damage);
                }
                break;
        }
    }
}
