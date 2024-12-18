using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private Attack attack;
    //Ŀ���
    private Vector3 target;
    //����
    private Vector3 moveDir;
    private bool isAttack;
    private float attackRateCurrent;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        attack = currentEnemy.GetComponent<Attack>();
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("chase", true);
    }
    public override void LogicUpdate()
    {
        //��ʱ�������󷵻�Ѳ��
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f,0);
        //�жϹ�������
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange&& Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.attackRange)
        {
            isAttack = true;
            if(!currentEnemy.isHurt)
            currentEnemy.rb.velocity = Vector2.zero;
            //��ʱ��
            attackRateCurrent -= Time.deltaTime;
            if (attackRateCurrent <= 0)
            {
                currentEnemy.anim.SetTrigger("attack");
                attackRateCurrent = attack.attackRate;
            }
        }
        else//����������Χ
        {
            isAttack = false;
        }
        moveDir = (target - currentEnemy.transform.position).normalized;
        if (moveDir.x > 0 && !currentEnemy.wait) currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if (moveDir.x < 0 && !currentEnemy.wait) currentEnemy.transform.localScale = new Vector3(1, 1, 1);
    }



    public override void Physicsupdate()
    {
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("chase", false);
    }
}
