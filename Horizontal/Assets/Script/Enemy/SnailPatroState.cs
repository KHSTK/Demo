using UnityEngine;
public class SnailPatroState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.nomalSpeed;
        currentEnemy.waitTimeCounter = currentEnemy.waitTime;
    }
    public override void LogicUpdate()
    {
        //发现player进入特殊状态
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Skill);
        }
        //巡逻状态
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)
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

    }
}
