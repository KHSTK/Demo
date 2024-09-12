using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Enemy : MonoBehaviour
{
    private HealthSystem_Enemy healthSystem_Enemy;

    private void Start()
    {
        healthSystem_Enemy = GetComponent<HealthSystem_Enemy>(); // 获取HealthSystem组件
        healthSystem_Enemy.OnDied += HealthSystem_OnDied; // 订阅死亡事件
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            healthSystem_Enemy.Damage(10); // 测试受伤
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject); // 销毁游戏对象
    }
}

