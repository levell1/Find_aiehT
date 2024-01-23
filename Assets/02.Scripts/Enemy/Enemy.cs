using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    public Animator Animator { get; private set; }
    public EnemyHealthSystem HealthSystem { get; private set; }
    public EnemyRespawn EnemyRespawn { get; private set; }

    [field: Header("Weapon")]
    [field: SerializeField] public EnemyAttackSpot Spot { get; private set; }

    [field: Header("Patrol")]
    [field: SerializeField] public float MinPatrolDistance;
    [field: SerializeField] public float MaxPatrolDistance;
    [field: SerializeField] public float DetectDistance;

    public float PatrolDelay = 0;
    public float AttackDelay = 0;

    public EnemyStateMachine _stateMachine;

    public NavMeshAgent Agent;


    void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();
        HealthSystem = GetComponent<EnemyHealthSystem>();
        EnemyRespawn = GetComponent<EnemyRespawn>();

        _stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdlingState);
        HealthSystem.OnDie += OnDie;
    }

    private void Update()
    {
        _stateMachine.HandleInput();

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        PatrolDelay += Time.deltaTime;
        AttackDelay += Time.deltaTime;

        _stateMachine.PhyscisUpdate();
    }

    private void OnDie()
    {
        _stateMachine.Target.PlayerExpSystem.EnemyExpPlus(Data.DropEXP);
        if(Data.DropItem != null)
        {
            for (int i = 0; i < Data.DropItem.Length; i++)
            {
                //TODO 랜덤으로 생성 될지 말지 정해주기++
                Instantiate(Data.DropItem[i], transform.position + Vector3.up * 2, Quaternion.identity);
            }
        }
        Collider.enabled = false;
    }

}
