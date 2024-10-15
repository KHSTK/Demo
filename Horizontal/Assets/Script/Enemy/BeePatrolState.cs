using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private Vector3 target;
    private Vector3 moveDir;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.nomalSpeed;
        target = currentEnemy.GetNewPoint();
    }
    public override void LogicUpdate()
    {
        //检测到玩家进行追击
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        //判断是否飞到目标点
        if(Mathf.Abs(target.x-currentEnemy.transform.position.x)<0.1f&& Mathf.Abs(target.y - currentEnemy.transform.position.y) < 0.1f)
        {
            currentEnemy.wait = true;
            target = currentEnemy.GetNewPoint();
        }
        moveDir = (target - currentEnemy.transform.position).normalized;
        if (moveDir.x > 0 && !currentEnemy.wait) currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if (moveDir.x < 0 && !currentEnemy.wait) currentEnemy.transform.localScale = new Vector3(1, 1, 1);
    }



    public override void Physicsupdate()
    {
        if (!currentEnemy.wait && !currentEnemy.isDead && !currentEnemy.isHurt)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed*Time.deltaTime;
        }
        else
        {
            currentEnemy.rb.velocity = Vector2.zero;
        }
    }
    public override void OnExit()
    {

    }
}
