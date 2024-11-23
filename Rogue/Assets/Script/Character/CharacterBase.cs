using System;
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
    public GameObject buff;
    public GameObject debuff;
    public IntVariable buffRound;
    public float baseStrong = 1f;
    private float strongEffect = 0.5f;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        hp.maxValue = maxHp;
        CurrentHp = MaxHp;
        ResetDefense();
        buffRound.SetValue(0);
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
    public void HealHealth(int amount)
    {
        CurrentHp += amount;
        CurrentHp = Mathf.Min(CurrentHp, MaxHp);
        buff.SetActive(true);
    }
    public void ResetStrong(int round, bool isPositive)
    {

        if (isPositive)
        {
            float newStrong = baseStrong + strongEffect;
            baseStrong = MathF.Min(newStrong, 1.5f);
            buff.SetActive(true);
        }
        else
        {
            float newStrong = baseStrong - strongEffect;
            baseStrong = MathF.Max(newStrong, 0.5f);
            debuff.SetActive(true);
        }

        var currentRound = buffRound.currentValue + round;
        if (baseStrong == 1)
        {
            buffRound.SetValue(0);
        }
        else
        {
            buffRound.SetValue(currentRound);
        }
    }
    public void UpdateBuffRound()
    {
        if (buffRound.currentValue > 0)
        {
            buffRound.SetValue(buffRound.currentValue - 1);
        }
        else
        {
            buffRound.SetValue(0);
            baseStrong = 1;
        }
    }

    public void ResetDefense()
    {
        defense.SetValue(0);
    }
}
