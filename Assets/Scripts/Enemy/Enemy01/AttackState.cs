using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FSMState
{
    Enemy01Controller parent;
    public AttackState(EnemyControl enemy)
    {
        parent = (Enemy01Controller)enemy;
    }

    public override void OnEnter()
    {
        parent.dataBiding.Attack();
        parent.attackCounter = 0;
        parent.GotoState(parent.ChaseState);

        RaycastHit[] hit = Physics.BoxCastAll(parent.ChestTr.position, Vector3.one, parent.ChestTr.forward);

        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].collider.GetComponent<PlayerController>()?.OnDamage(parent.damage);
            }
        }
    }
}
