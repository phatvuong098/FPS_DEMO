using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : FSMState
{
    Enemy01Controller parent;

    public DeadState(EnemyControl enemy)
    {
        parent = (Enemy01Controller)enemy;
    }

    public override void OnEnter()
    {
        parent.agent.isStopped = true;
        parent.isAlive = false;
        parent.StopCoroutine("LoopDetect");
        parent.dataBiding.Dead();
        UI_Controller.EnemyDead();
        parent.StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(4f);
        GameObject.Destroy(parent.gameObject);
    }
}
