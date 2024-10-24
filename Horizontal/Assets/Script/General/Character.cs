using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;
    public float powerRecoverSpeed;
    [Header("受伤无敌")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> OnHealthChange;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;

    private void Start()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange.Invoke(this);

    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
        if (currentPower < maxPower)
        {

        }
    }
    //受伤掉血
    public void TakeDamage(Attack attacker)
    {
        //是否无敌时间
        if (invulnerable) return;
        Debug.Log(attacker.damage);
        if (currentHealth <= attacker.damage)
        {
            currentHealth = 0;
            Debug.Log("die");

            //触发死亡
            OnDead.Invoke();
            OnHealthChange.Invoke(this);
            return;
        }
        currentHealth -=attacker.damage;
        //无敌时间
        TriggerInvulnerable();
        //执行受伤
        OnTakeDamage?.Invoke(attacker.transform);
        OnHealthChange.Invoke(this);
    }
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    public void OnSlide(int cost)
    {
        currentPower -= cost;
        OnHealthChange.Invoke(this);
    }
}
