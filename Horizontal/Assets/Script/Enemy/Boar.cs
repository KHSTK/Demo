using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy {
    protected override void Awake()
    {
        base.Awake();
        //����Ѳ���߼�,�൱�ڸ�Enemy��patrolState��ֵ
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }
}
