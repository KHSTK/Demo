using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy; // ��ǰĿ�����
    private float lookForTargetTimer; // Ŀ����Ҽ�ʱ��
    private float lookForTargetTimerMax = 0.2f; // Ŀ����Ҽ�ʱ�����ֵ
    private Vector3 projectileSpawnPosition; // �ӵ�����λ��

    private float shootTimer; // �����ʱ��
    [SerializeField] 
    private float shootTimerMax; // �����ʱ�����ֵ
    [SerializeField]
    private float targetMaxRadius; // ���ҵ����뾶



    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position; // �ҵ��ӵ�����λ�õ�Transform���������ȡ������
    }

    // ÿִ֡��һ��
    private void Update()
    {
        HandleTargeting(); // ����Ŀ��ѡ��
        HandleShooting(); // �������
    }

    // ����Ŀ�����
    private void LookForTargets()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius); // ��ָ��λ�ð뾶�ڲ�����ײ��
        foreach (Collider2D collider2D in collider2DArray) // �������е���ײ��
        {
            Enemy enemy = collider2D.GetComponent<Enemy>(); // ��ȡ��ײ���ĵ������
            if (enemy != null) // �������һ������
            {
                // �ж��Ƿ��Ǹ��ŵĹ���Ŀ��
                if (targetEnemy == null) // ���ԭ��û��Ŀ�꣬�����Ϊ��ǰ����
                {
                    targetEnemy = enemy;
                }
                else if (Vector3.Distance(transform.position, enemy.transform.position) <
                         Vector3.Distance(transform.position, targetEnemy.transform.position)) // �����������������Ϊ��ǰ����
                {
                    targetEnemy = enemy;
                }
            }
        }
    }

    // ����Ŀ��ѡ��
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime; // ��ȥ֡ʱ�䣬����Ŀ����Ҽ�ʱ��
        if (lookForTargetTimer <= 0f) // �����ʱ�������ˣ���ʼ���Ҳ�ѡ���µ�Ŀ��
        {
            lookForTargetTimer += lookForTargetTimerMax; // ���ü�ʱ��
            LookForTargets(); // ����Ŀ��
        }
    }

    // �������
    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime; // ��ȥ֡ʱ�䣬���������ʱ��
        if (shootTimer <= 0f) // �����ʱ�������ˣ���ʼ���
        {
            shootTimer += shootTimerMax; // ���ü�ʱ��
            if (targetEnemy != null) ArrowProjectile.Create(projectileSpawnPosition, targetEnemy); // ������ͷʵ��
        }
    }


}

