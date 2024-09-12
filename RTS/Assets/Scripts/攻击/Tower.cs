using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy; // 当前目标敌人
    private float lookForTargetTimer; // 目标查找计时器
    private float lookForTargetTimerMax = 0.2f; // 目标查找计时器最大值
    private Vector3 projectileSpawnPosition; // 子弹生成位置

    private float shootTimer; // 射击计时器
    [SerializeField] 
    private float shootTimerMax; // 射击计时器最大值
    [SerializeField]
    private float targetMaxRadius; // 查找的最大半径



    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position; // 找到子弹生成位置的Transform组件，并获取其坐标
    }

    // 每帧执行一次
    private void Update()
    {
        HandleTargeting(); // 处理目标选择
        HandleShooting(); // 处理射击
    }

    // 查找目标敌人
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius); // 在指定位置半径内查找碰撞体
        foreach (Collider2D collider2D in collider2DArray) // 遍历所有的碰撞体
        {
            Enemy enemy = collider2D.GetComponent<Enemy>(); // 获取碰撞到的敌人组件
            if (enemy != null) // 如果这是一个敌人
            {
                // 判断是否是更优的攻击目标
                if (targetEnemy == null) // 如果原来没有目标，则更新为当前敌人
                {
                    targetEnemy = enemy;
                }
                else if (Vector3.Distance(transform.position, enemy.transform.position) <
                         Vector3.Distance(transform.position, targetEnemy.transform.position)) // 如果距离更近，则更新为当前敌人
                {
                    targetEnemy = enemy;
                }
            }
        }
    }

    // 处理目标选择
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime; // 减去帧时间，更新目标查找计时器
        if (lookForTargetTimer <= 0f) // 如果计时器结束了，则开始查找并选择新的目标
        {
            lookForTargetTimer += lookForTargetTimerMax; // 重置计时器
            LookForTargets(); // 查找目标
        }
    }

    // 处理射击
    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime; // 减去帧时间，更新射击计时器
        if (shootTimer <= 0f) // 如果计时器结束了，则开始射击
        {
            shootTimer += shootTimerMax; // 重置计时器
            if (targetEnemy != null) ArrowProjectile.Create(projectileSpawnPosition, targetEnemy); // 创建箭头实例
        }
    }


}

