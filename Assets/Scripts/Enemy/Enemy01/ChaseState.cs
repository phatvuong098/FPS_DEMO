using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState
{
    Enemy01Controller parent;
    private Coroutine coroutine;

    public ChaseState(EnemyControl enemy)
    {
        parent = (Enemy01Controller)enemy;
    }

    public override void OnEnter()
    {
        if (coroutine != null)
            parent.StopCoroutine(coroutine);

        parent.agent.speed = parent.speed * 2;
        coroutine = parent.StartCoroutine(ChaseCoro());
    }

    public override void OnExit()
    {
        if (coroutine != null)
            parent.StopCoroutine(coroutine);
    }

    IEnumerator ChaseCoro()
    {
        parent.agent.SetDestination(parent.characterController.transform.position);
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        while (true)
        {
            yield return wait;

            if (parent.agent.remainingDistance <= parent.attackRange)
            {
                parent.RotateToAgent();
                parent.agent.updatePosition = false;
                parent.agent.isStopped = true;
                parent.dataBiding.Run = false;
                parent.dataBiding.Walk = false;

                if (parent.attackCounter > parent.attackSpeed)
                {
                    parent.GotoState(parent.AttackState);
                }
            }
            else
            {
                parent.agent.updatePosition = true;
                parent.agent.isStopped = false;
                parent.dataBiding.Run = true;
                parent.dataBiding.Walk = false;
                parent.agent.SetDestination(parent.characterController.transform.position);
            }
        }
    }
}
