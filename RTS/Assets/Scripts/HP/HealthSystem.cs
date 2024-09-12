using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged; // 受伤事件
    public event EventHandler OnDied; // 死亡事件
    private int healthAmountMax; // 最大生命值
    private int healthAmount; // 当前生命值
    public BuildingType buildingType; // 建筑类型引用
    void Awake()
    {
        if (buildingType != null)
        {
            healthAmountMax = buildingType.healthAmountMax;
            healthAmount = healthAmountMax;
        }
    }


    //受伤
    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount; // 减少生命值

        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax); // 限制生命值在0和最大生命值之间

        OnDamaged?.Invoke(this, EventArgs.Empty); // 触发受伤事件

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty); // 触发死亡事件
        }
    }

    private bool IsDead()
    {
        return healthAmount <= 0; // 判断是否死亡
    }

    public int GetHealthAmount()
    {
        Debug.Log(healthAmount);
        return healthAmount; // 获取当前生命值
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax; // 获取当前生命值的归一化值
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax; // 设置最大生命值

        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax; // 如果需要更新当前生命值，将当前生命值设置为最大生命值
        }
    }
}
