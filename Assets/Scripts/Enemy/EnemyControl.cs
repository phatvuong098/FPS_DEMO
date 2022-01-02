using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyControl : FSMSystem
{
    public FSMState idleState;
    public FSMState wardenState;
    public FSMState ChaseState;
    public FSMState DeadState;
    public FSMState AttackState;

    public EEnemy enemy;
    public int currentHealth;
    public float detectRange;
    public float attackRange;
    public int damage;
    public float speed;
    public float attackSpeed;
    public float attackCounter;
    public bool isAlive;

    public Transform ChestTr;
    public CharacterController characterController;
    public EnemyDataBiding dataBiding;
    public NavMeshAgent agent;
    public HPPanel hpPanel;
    public LayerMask mask;
    public Transform _mts;

    private int baseHealth;

    protected abstract void InitFSM();

    private void Start()
    {
        InitFSM();
        Setup();
    }

    public void Setup()
    {
        EnemyConfig enemyConfig = Resources.Load("ScriptableObject/Enemyconfig", typeof(EnemyConfig)) as EnemyConfig;
        EnemyConfigRecord record = enemyConfig.GetRecordByEnemy(enemy);

        this.currentHealth = record.health;
        this.detectRange = record.detectRange;
        this.attackRange = record.attackRange;
        this.damage = record.damage;
        this.speed = record.speed;
        this.attackSpeed = record.attackSpeed;
        this.baseHealth = record.health;
        isAlive = true;
        hpPanel.UpdateHeath(currentHealth / baseHealth);
        GotoState(idleState);
        StartCoroutine("LoopDetect");
        agent.speed = this.speed;
    }

    public override void OnSystemUpdate()
    {
        attackCounter += Time.deltaTime;
    }

    public virtual void OnDamage(int damage, CharacterController actor)
    {
        if (!isAlive)
            return;

        currentHealth -= damage;
        hpPanel.UpdateHeath((float)currentHealth / (float)baseHealth);

        if (currentState != ChaseState)
        {
            characterController = actor;
            GotoState(ChaseState);
        }

        StopCoroutine("LoopDetect");
        if (currentHealth <= 0)
            GotoState(DeadState);
    }

    public void RotateToAgent()
    {
        Vector3 dir = agent.steeringTarget - _mts.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            _mts.localRotation = Quaternion.RotateTowards(_mts.localRotation, q, Time.deltaTime * 720f);
        }
    }

    private void Awake()
    {
        _mts = GetComponent<Transform>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.DrawCube(transform.position, transform.forward);
    }

    IEnumerator LoopDetect()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (isAlive)
        {
            yield return wait;

            if (characterController != null)
                yield return null;

            Collider[] cols = Physics.OverlapSphere(_mts.position, detectRange, mask);
            if (cols.Length > 0)
            {
                foreach (Collider e in cols)
                {
                    characterController = e.GetComponent<CharacterController>();
                    break;
                }
            }

            if (characterController != null && currentState != ChaseState)
                GotoState(ChaseState);
        }
    }



}
