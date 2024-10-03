using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("��������")]
    public float maxHealth;
    public float currentHealth;
    [Header("�����޵�")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;
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
    //���˵�Ѫ
    public void TakeDamage(Attack attacker)
    {
        //�Ƿ��޵�ʱ��
        if (invulnerable) return;
        Debug.Log(attacker.damage);
        if (currentHealth <= attacker.damage)
        {
            currentHealth = 0;
            Debug.Log("die");

            //��������
            return;
        }
        currentHealth -=attacker.damage;
        //�����޵�ʱ��
        TriggerInvulnerable();
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
