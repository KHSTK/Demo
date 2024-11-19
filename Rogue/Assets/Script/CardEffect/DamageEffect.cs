using UnityEngine;

[CreateAssetMenu(menuName = "CardEffect/DamageEffect")]
public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        if (target == null) return;
        switch (targetType)
        {
            case EffcetTargetType.Self:
                from.TakeDamage(value);
                Debug.Log("造成伤害：" + value);
                break;
            case EffcetTargetType.Target:
                target.TakeDamage(value);
                break;
            case EffcetTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }
}
