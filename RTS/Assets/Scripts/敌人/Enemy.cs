using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidbody2d; // 敌人刚体组件
    private Transform targetTransform; // 目标建筑物的Transform组件，即敌人要攻击的建筑物
    private float lookForTargetTimer; // 查找目标的计时器
    private float lookForTargetTimerMax = 0.2f; // 查找目标的时间间隔
    private HealthSystem_Enemy healthSystem;

    // 创建一个新的敌人
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("Enemy"); // 加载敌人预制体资源
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity); // 在指定位置生成敌人
        Enemy enemy = enemyTransform.GetComponent<Enemy>(); // 获取敌人脚本组件
        return enemy;
    }

    // 初始化敌人
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); // 获取敌人刚体组件
        if (BuildingManager.Instance.GetHQBuilding() != null) targetTransform = BuildingManager.Instance.GetHQBuilding().transform; // 设置敌人攻击的目标建筑物为总部
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax); // 随机设置查找目标的计时器
        healthSystem = GetComponent<HealthSystem_Enemy>();
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        HandleMovement(); // 处理敌人的移动
        HandleTargeting(); // 处理敌人的目标选择
    }

    // 处理敌人的移动
    private void HandleMovement()
    {
        if (targetTransform != null) // 如果有目标，则向目标方向移动
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized; // 计算向目标方向的移动向量
            float moveSpeed = 6f; // 移动速度
            rigidbody2d.velocity = moveDir * moveSpeed; // 更新敌人刚体组件的速度
        }
        else // 如果没有目标，则停止移动
        {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    // 处理敌人的目标选择
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime; // 减去帧时间，更新查找目标的计时器
        if (lookForTargetTimer <= 0f) // 如果计时器结束了，则开始查找并选择新的目标
        {
            lookForTargetTimer += lookForTargetTimerMax; // 重置计时器
            LookForTargets(); // 查找目标
        }
    }

    // 当敌人与其他物体碰撞时触发的事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>(); // 获取碰撞到的建筑物
        if (building != null) // 如果碰撞到的是建筑物
        {
            // 减少建筑物的生命值，并销毁自身
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    // 查找目标建筑物
    private void LookForTargets()
    {
        float targetMaxRadius = 10f; // 查找的最大半径
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius); // 在指定位置半径内查找碰撞体
        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>(); // 获取碰撞体上的建筑物组件
            if (building != null) // 如果这是一个建筑物
            {
                // 判断是否是更优的攻击目标
                if (targetTransform == null)
                {
                    targetTransform = building.transform; // 如果原来没有目标，则更新为当前建筑物
                }
                else if (Vector3.Distance(transform.position, building.transform.position) <
                         Vector3.Distance(transform.position, targetTransform.position))
                {
                    targetTransform = building.transform; // 如果距离更近，则更新为当前建筑物
                }
            }
        }

        if (targetTransform == null) // 如果没有找到目标，则将目标设置为总部
        {
            if (BuildingManager.Instance.GetHQBuilding() != null)
            {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}


