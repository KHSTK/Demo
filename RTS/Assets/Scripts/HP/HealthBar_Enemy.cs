using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_Enemy : MonoBehaviour
{
    [SerializeField]
    private HealthSystem_Enemy healthSystem_Enemy;
    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("bar"); // ��ȡbar��Transform���
    }

    private void Start()
    {
        healthSystem_Enemy.OnDamaged += HealthSystem_OnDamaged; // ���������¼�
        UpdateBar();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem_Enemy.GetHealthAmountNormalized(), barTransform.localScale.y, 1); // ����Ѫ�������ű���
    }

}

