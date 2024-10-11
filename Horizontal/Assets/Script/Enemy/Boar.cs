using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy {
    protected override void Awake()
    {
        base.Awake();
        //创建巡逻逻辑,相当于给Enemy的patrolState赋值
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
}
