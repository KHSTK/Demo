using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy currentEnemy);
    public abstract void LogicUpdate();
    //�����жϷ���FixedUpdate��
    public abstract void Physicsupdate();
    public abstract void OnExit();
}
