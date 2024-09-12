using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthSystem healthSystem;
    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("bar"); // ��ȡbar��Transform���
    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged; // ���������¼�
        UpdateBar();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), barTransform.localScale.y, 1); // ����Ѫ�������ű���
    }

}
