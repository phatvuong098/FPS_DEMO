using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    Enemy01Controller parent;
    public IdleState(EnemyControl enemy)
    {
        parent = (Enemy01Controller)enemy;
    }
    Coroutine coroutine;

    public override void OnEnter()
    {
        parent.dataBiding.Run = false;
        parent.dataBiding.Walk = false;

        if (coroutine != null)
            parent.StopCoroutine(coroutine);
        coroutine = parent.StartCoroutine(WaitAminuteCoro());
    }
    IEnumerator WaitAminuteCoro()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        parent.GotoState(parent.wardenState);
    }
    public override void OnExit()
    {
        if (coroutine != null)
            parent.StopCoroutine(coroutine);
    }
}
