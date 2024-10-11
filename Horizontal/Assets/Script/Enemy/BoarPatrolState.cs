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
        //����player����׷��״̬
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        //Ѳ��״̬
        if (!currentEnemy.physicsCheck.isGround||currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)
        {
            Debug.Log("ת��");
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
