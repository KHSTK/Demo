using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;
    public IntVariable hp;
    //调用currentHp时自动获取hp.currentValue，更新currentHp时更新hp.currentValue且广播
    public IntVariable defense;
    public int CurrentHp { get => hp.currentValue; set => hp.SetValue(value); }
    public int MaxHp { get => hp.maxValue; }

    protected Animator animator;
    public bool isDead;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHp = MaxHp;
        ResetDefense();
    }
    public virtual void TakeDamage(int damage)
    {
        var currentDamage = (damage - defense.currentValue) > 0 ? (damage - defense.currentValue) : 0;
        var currentDefense = (damage - defense.currentValue) > 0 ? 0 : (defense.currentValue - damage);
        defense.SetValue(currentDefense);
        if (CurrentHp > currentDamage)
        {
            CurrentHp -= currentDamage;
            Debug.Log("Take Damage:" + currentDamage);
            Debug.Log("CurrentHp" + CurrentHp);

        }
        else
        {
            CurrentHp = 0;
            //当前人物死亡
            isDead = true;
        }
    }
    public void UpdateDefense(int amount)
    {
        var value = defense.currentValue + amount;
        defense.SetValue(value);
    }
    public void ResetDefense()
    {
        defense.SetValue(0);
    }
}
