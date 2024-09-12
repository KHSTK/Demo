using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged; // �����¼�
    public event EventHandler OnDied; // �����¼�
    private int healthAmountMax; // �������ֵ
    private int healthAmount; // ��ǰ����ֵ
    public BuildingType buildingType; // ������������
    void Awake()
    {
        if (buildingType != null)
        {
            healthAmountMax = buildingType.healthAmountMax;
            healthAmount = healthAmountMax;
        }
    }


    //����
    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount; // ��������ֵ

        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax); // ��������ֵ��0���������ֵ֮��

        OnDamaged?.Invoke(this, EventArgs.Empty); // ���������¼�

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty); // ���������¼�
        }
    }

    private bool IsDead()
    {
        return healthAmount <= 0; // �ж��Ƿ�����
    }

    public int GetHealthAmount()
    {
        Debug.Log(healthAmount);
        return healthAmount; // ��ȡ��ǰ����ֵ
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax; // ��ȡ��ǰ����ֵ�Ĺ�һ��ֵ
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax; // �����������ֵ

        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax; // �����Ҫ���µ�ǰ����ֵ������ǰ����ֵ����Ϊ�������ֵ
        }
    }
}
