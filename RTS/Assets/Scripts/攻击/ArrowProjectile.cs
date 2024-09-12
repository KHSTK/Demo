using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Enemy targetEnemy; // Ŀ����˵����
    private Vector3 lastMoveDir; // ��һ���ƶ�����
    private float timeToDie = 1.5f; // ���ʱ��
    [SerializeField]
    private int damageAmount = 50;


    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("��"); // ���ؼ�ͷԤ����
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity); // ʵ������ͷ����
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>(); // ��ȡ��ͷ�Ľű����
        arrowProjectile.SetTarget(enemy); // ����Ŀ�����
        return arrowProjectile;
    }

    // ��ÿ֡����λ��
    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null) // �����Ŀ�����
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized; // ���㵱ǰ�ƶ�����
            lastMoveDir = moveDir; // ������һ���ƶ�����
        }
        else
        {
            moveDir = lastMoveDir; // û��Ŀ�����ʱ����ʹ����һ�ε��ƶ�����
        }
        float moveSpeed = 20f; // �ƶ��ٶ�
        transform.position += moveDir * moveSpeed * Time.deltaTime; // �����ƶ������ٶȺ�ʱ�����λ��
        transform.eulerAngles = new Vector3(0, 0, Utilsclass.GetAngleFromVector(moveDir)); // �����ƶ����������ת�Ƕ�
        timeToDie -= Time.deltaTime; // ��ȥ֡ʱ�䣬���´��ʱ��
        if (timeToDie < 0f)
        {
            Destroy(gameObject); // ���ʱ�����ʱ���ټ�ͷ����
        }
    }

    // ����Ŀ�����
    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    // �����봥����ʱ��������Ķ����Ƿ��ǵ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>(); // ��ȡ�������ڵ���ײ���еĵ������
        if (enemy != null) // ����������������ǵ��ˣ������ټ�ͷ
        {
            //��������
            enemy.GetComponent<HealthSystem_Enemy>().Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}

