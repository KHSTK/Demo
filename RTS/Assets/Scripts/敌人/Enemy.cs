using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidbody2d; // ���˸������
    private Transform targetTransform; // Ŀ�꽨�����Transform�����������Ҫ�����Ľ�����
    private float lookForTargetTimer; // ����Ŀ��ļ�ʱ��
    private float lookForTargetTimerMax = 0.2f; // ����Ŀ���ʱ����
    private HealthSystem_Enemy healthSystem;

    // ����һ���µĵ���
    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("Enemy"); // ���ص���Ԥ������Դ
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity); // ��ָ��λ�����ɵ���
        Enemy enemy = enemyTransform.GetComponent<Enemy>(); // ��ȡ���˽ű����
        return enemy;
    }

    // ��ʼ������
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); // ��ȡ���˸������
        if (BuildingManager.Instance.GetHQBuilding() != null) targetTransform = BuildingManager.Instance.GetHQBuilding().transform; // ���õ��˹�����Ŀ�꽨����Ϊ�ܲ�
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax); // ������ò���Ŀ��ļ�ʱ��
        healthSystem = GetComponent<HealthSystem_Enemy>();
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update()
    {
        HandleMovement(); // ������˵��ƶ�
        HandleTargeting(); // ������˵�Ŀ��ѡ��
    }

    // ������˵��ƶ�
    private void HandleMovement()
    {
        if (targetTransform != null) // �����Ŀ�꣬����Ŀ�귽���ƶ�
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized; // ������Ŀ�귽����ƶ�����
            float moveSpeed = 6f; // �ƶ��ٶ�
            rigidbody2d.velocity = moveDir * moveSpeed; // ���µ��˸���������ٶ�
        }
        else // ���û��Ŀ�꣬��ֹͣ�ƶ�
        {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    // ������˵�Ŀ��ѡ��
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime; // ��ȥ֡ʱ�䣬���²���Ŀ��ļ�ʱ��
        if (lookForTargetTimer <= 0f) // �����ʱ�������ˣ���ʼ���Ҳ�ѡ���µ�Ŀ��
        {
            lookForTargetTimer += lookForTargetTimerMax; // ���ü�ʱ��
            LookForTargets(); // ����Ŀ��
        }
    }

    // ������������������ײʱ�������¼�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>(); // ��ȡ��ײ���Ľ�����
        if (building != null) // �����ײ�����ǽ�����
        {
            // ���ٽ����������ֵ������������
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    // ����Ŀ�꽨����
    private void LookForTargets()
    {
        float targetMaxRadius = 10f; // ���ҵ����뾶
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius); // ��ָ��λ�ð뾶�ڲ�����ײ��
        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>(); // ��ȡ��ײ���ϵĽ��������
            if (building != null) // �������һ��������
            {
                // �ж��Ƿ��Ǹ��ŵĹ���Ŀ��
                if (targetTransform == null)
                {
                    targetTransform = building.transform; // ���ԭ��û��Ŀ�꣬�����Ϊ��ǰ������
                }
                else if (Vector3.Distance(transform.position, building.transform.position) <
                         Vector3.Distance(transform.position, targetTransform.position))
                {
                    targetTransform = building.transform; // �����������������Ϊ��ǰ������
                }
            }
        }

        if (targetTransform == null) // ���û���ҵ�Ŀ�꣬��Ŀ������Ϊ�ܲ�
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


