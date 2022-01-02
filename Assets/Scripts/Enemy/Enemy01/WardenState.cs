using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WardenState : FSMState
{
    Enemy01Controller parent;

    private Coroutine coroutine;
    public WardenState(EnemyControl enemy)
    {
        this.parent = (Enemy01Controller)enemy;
    }

    public override void OnEnter()
    {
        parent.agent.speed = parent.speed;
        parent.dataBiding.Run = false;
        parent.dataBiding.Walk = true;
        TryWarden();
    }

    public override void OnExit()
    {
        if (coroutine != null)
            parent.StopCoroutine(coroutine);
    }

    private IEnumerator WardenCoro()
    {
        yield return new WaitUntil(() => parent.agent.remainingDistance < .5f);
        parent.GotoState(parent.idleState);
    }

    private void TryWarden()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        if (parent.agent.CalculatePath(RandomPoint(), navMeshPath))
        {
            parent.agent.SetPath(navMeshPath);

            if (coroutine != null)
                parent.StopCoroutine(coroutine);
            coroutine = parent.StartCoroutine(WardenCoro());
        }
        else
        {
            parent.GotoState(parent.idleState);
        }
    }
    private Vector3 RandomPoint()
    {
        Vector2 random = Random.insideUnitCircle;
        return new Vector3(random.x * parent.wardenDistance + parent.transform.position.x,
            0, random.y * parent.wardenDistance + parent.transform.position.z);
    }
}
