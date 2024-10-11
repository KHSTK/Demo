using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.hurtForce = 7;
        currentEnemy.currentSpeed = currentEnemy.nomalSpeed;
    }
    public override void LogicUpdate()
    {
        //发现player进入追击状态
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        //巡逻状态
        if (!currentEnemy.physicsCheck.isGround||currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)
        {
            Debug.Log("转向");
            currentEnemy.anim.SetBool("walk", false);
            currentEnemy.wait = true;
        }
        else
        {
            currentEnemy.anim.SetBool("walk", true);
        }
    }
    public override void Physicsupdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
        Debug.Log("Exit Patrol");
    }
}
