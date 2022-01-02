using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBiding : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    int kWalk, kRun, kAttack, kDead;
    private void Awake()
    {
        kWalk = Animator.StringToHash("Walk");
        kRun = Animator.StringToHash("Run");
        kAttack = Animator.StringToHash("Attack");
        kDead = Animator.StringToHash("Dead");
    }

    public bool Walk
    {
        set { animator.SetBool(kWalk, value); }
    }

    public bool Run
    {
        set { animator.SetBool(kRun, value); }
    }

    public void Attack()
    {
        animator.SetTrigger(kAttack); 
    }

    public void Dead()
    {
        animator.SetTrigger(kDead);
    }
}
