using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Controller : EnemyControl
{
    public float wardenDistance = 5;
    protected override void InitFSM()
    {
        idleState = new IdleState(this);
        wardenState = new WardenState(this);
        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);
        DeadState = new DeadState(this);
    }
}
