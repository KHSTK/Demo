using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int maxHp;
    public IntVariable hp;
    //调用currentHp时自动获取hp.currentValue，更新currentHp时更新hp.currentValue且广播
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
    }
    public virtual void TakeDamage(int damage)
    {
        if (CurrentHp > damage)
        {
            CurrentHp -= damage;
            Debug.Log("Take Damage:" + damage);
            Debug.Log("CurrentHp" + CurrentHp);

        }
        else
        {
            CurrentHp = 0;
            //当前人物死亡
            isDead = true;
        }
    }
}
