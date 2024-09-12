using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Enemy : MonoBehaviour
{
    private HealthSystem_Enemy healthSystem_Enemy;

    private void Start()
    {
        healthSystem_Enemy = GetComponent<HealthSystem_Enemy>(); // ��ȡHealthSystem���
        healthSystem_Enemy.OnDied += HealthSystem_OnDied; // ���������¼�
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthSystem_Enemy.Damage(10); // ��������
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject); // ������Ϸ����
    }
}

