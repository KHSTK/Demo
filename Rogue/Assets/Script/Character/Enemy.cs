using System.Collections;
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
    }
    protected override void Start()
    {
        base.Start();
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        // currentAction.effect.Execute(this, this);
        // animator.SetTrigger("akill");
        StartCoroutine(DelayAction("skill"));
    }
    public void Attack()
    {
        // currentAction.effect.Execute(this, player);
        // animator.SetTrigger("attack");
        StartCoroutine(DelayAction("attack"));
    }
    IEnumerator DelayAction(string actionName)
    {
        animator.SetTrigger(actionName);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.6f
        && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName(actionName));
        if (actionName == "attack")
        {
            currentAction.effect.Execute(this, player);
        }
        else
        {
            currentAction.effect.Execute(this, this);
        }
    }
    protected override void DeadEvent()
    {
        base.DeadEvent();
        currentAction.effect = null;
        currentAction.initentIcon = null;
    }

}
