using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;// 最大生命值
    public float currentHealth;// 当前生命值
    [Header("受伤无敌")]
    public float invulnerableDuration;// 无敌持续时间
    private bool invulnerable;// 是否处于无敌状态

    private void Start()
    {
        currentHealth = maxHealth;// 初始化当前生命值为最大生命值
        Debug.Log(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        Debug.Log(currentHealth);
        if (invulnerable) return;

        if (currentHealth - damage > 0)// 如果扣除伤害后生命值大于0
        {
            currentHealth -= damage;// 扣除伤害值
            StartCoroutine(InvulnerableTimer()); // 启动无敌状态计时器
        }
        else
        {
            currentHealth = 0;
            GetComponent<IInteractable>().OnDie();
        }

    }

    private IEnumerator InvulnerableTimer()
    {
        invulnerable = true; // 设为无敌状态
        yield return new WaitForSeconds(invulnerableDuration); // 等待无敌持续时间
        invulnerable = false; // 取消无敌状态
    }

}

