using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy targetEnemy; // 目标敌人的组件
    private Vector3 lastMoveDir; // 上一次移动方向
    private float timeToDie = 1.5f; // 存活时间
    [SerializeField]
    private int damageAmount = 50;


    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("箭"); // 加载箭头预制体
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity); // 实例化箭头对象
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>(); // 获取箭头的脚本组件
        arrowProjectile.SetTarget(enemy); // 设置目标敌人
        return arrowProjectile;
    }

    // 在每帧更新位置
    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null) // 如果有目标敌人
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized; // 计算当前移动方向
            lastMoveDir = moveDir; // 更新上一次移动方向
        }
        else
        {
            moveDir = lastMoveDir; // 没有目标敌人时继续使用上一次的移动方向
        }
        float moveSpeed = 20f; // 移动速度
        transform.position += moveDir * moveSpeed * Time.deltaTime; // 根据移动方向、速度和时间更新位置
        transform.eulerAngles = new Vector3(0, 0, Utilsclass.GetAngleFromVector(moveDir)); // 根据移动方向更新旋转角度
        timeToDie -= Time.deltaTime; // 减去帧时间，更新存活时间
        if (timeToDie < 0f)
        {
            Destroy(gameObject); // 存活时间结束时销毁箭头对象
        }
    }

    // 设置目标敌人
    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    // 当进入触发器时检查碰到的对象是否是敌人
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>(); // 获取触发器内的碰撞体中的敌人组件
        if (enemy != null) // 如果触发器碰到的是敌人，则销毁箭头
        {
            //攻击敌人
            enemy.GetComponent<HealthSystem_Enemy>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}

