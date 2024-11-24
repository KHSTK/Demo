using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;
    public EnemyAction currentAction;
    private List<EnemyAction> currentActionList = new List<EnemyAction>();
    protected Player player;
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public virtual void OnPlayerTurnStart()
    {
        // var randomIndex = Random.Range(0, actionData.actionList.Count);
        // for (int i = 0; i < Random.Range(0, 3); i++)
        // {
        //     currentAction = actionData.actionList[randomIndex];
        //     currentActionList.Add(currentAction);
        // }
        currentAction = actionData.actionList[0];
    }
    public virtual void OnEnemyTurnStar()
    {
        switch (currentAction.effect.targetType)
        {
            case EffcetTargetType.Self:
                Skill();
                break;
            case EffcetTargetType.Target:
                Attack();
                break;
            case EffcetTargetType.All:
                break;
        }
    }
    public void Skill()
    {
        currentAction.effect.Execute(this, this);
    }
    public void Attack()
    {
        currentAction.effect.Execute(this, player);
    }
}
