using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    public EnemyActionDataSO actionData;
    public EnemyAction currentAction;
    private List<EnemyAction> currentActionList = new List<EnemyAction>();
    private HealthBarController healthBarController;
    protected Player player;
    public ObjectEventSO enemyTurnEndEvent;

    protected override void Awake()
    {
        base.Awake();
        healthBarController = GetComponent<HealthBarController>();
    }
    protected override void Start()
    {
        base.Start();
    }
    public virtual void OnPlayerTurnStart()
    {
        int randomIndex = Random.Range(1, actionData.actionList.Count + 1);
        //随机数量个行动
        for (int i = 0; i < randomIndex; i++)
        {
            //随机行动
            currentAction = actionData.actionList[Random.Range(0, actionData.actionList.Count)];
            healthBarController.UpdateIntentBar(currentAction);
            currentActionList.Add(currentAction);
        }
    }
    public virtual void OnEnemyTurnStar()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        // for (int i = 0; i < currentActionList.Count; i++)
        // {
        //     if (currentActionList[i].effect.targetType == EffcetTargetType.Self)
        //     {
        //         currentActionList[i].effect.Execute(this, this);
        //         //Skill();
        //     }
        //     else
        //     {
        //         currentActionList[i].effect.Execute(this, player);
        //         //Attack();
        //     }
        //     Debug.Log("当前行动" + currentActionList[i]);
        // }
        StartCoroutine(DelayAction());

    }
    IEnumerator DelayAction()
    {
        int r = currentActionList.Count;
        for (int i = 0; i < currentActionList.Count; i++)
        {
            var actionName = currentActionList[r - 1].effect.targetType == EffcetTargetType.Self ? "skill" : "attack";
            animator.SetTrigger(actionName);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f > 0.8f
            && !animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).IsName(actionName));
            if (actionName == "skill")
            {

                currentActionList[r - 1].effect.Execute(this, this);
                //Skill();
            }
            else
            {
                currentActionList[r - 1].effect.Execute(this, player);
                //Attack();
            }
            Debug.Log("当前行动" + currentActionList[r - 1]);
            r--;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        Debug.Log("当前行动列表" + currentActionList.Count);
        currentActionList.Clear();
        Debug.Log("敌人行动结束");
        enemyTurnEndEvent.RaiseEvent(null, this);
    }
    protected override void DeadEvent()
    {
        base.DeadEvent();
        currentAction.effect = null;
        currentAction.initentIcon = null;
    }

}
