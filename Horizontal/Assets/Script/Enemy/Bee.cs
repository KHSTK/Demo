using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("�ƶ���Χ")]
    public float patrolRadius;
    protected override void Awake()
    {
        base.Awake();
        patrolState =new BeePatrolState();
    }
    public override bool FoundPlayer()
    {
        var obj= Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if (obj)
        {
            attacker = obj.transform;
        }
        return obj;
    }
    public override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
    public override Vector3 GetNewPoint()
    {
        //���з�Χ�����ȡΪĿ���
        var targeX = Random.Range(-patrolRadius, patrolRadius);
        var targeY = Random.Range(-patrolRadius, patrolRadius);
        return spwanPoint + new Vector3(targeX, targeY);
    }
    public override void Move()
    {

    }
}
