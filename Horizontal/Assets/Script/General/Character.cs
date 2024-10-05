using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDead;

    private void Start()
    {
        currentHealth = maxHealth;
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
            return;
        }
        currentHealth -=attacker.damage;
        //无敌时间
        TriggerInvulnerable();
        //执行受伤
        OnTakeDamage?.Invoke(attacker.transform);
    }
    public void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
