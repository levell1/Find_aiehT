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

    private EnemyStateMachine _stateMachine;

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
        PatrolDelay += Time.deltaTime;

        _stateMachine.HandleInput();

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhyscisUpdate();
    }

    private void OnDie()
    {
        Collider.enabled = false;
    }

}
