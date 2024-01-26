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

    public float EnemyDamage;
    public float EnemyMaxHealth;
    public int EnemyDropEXP;


    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();
        HealthSystem = GetComponent<EnemyHealthSystem>();
        EnemyRespawn = GetComponent<EnemyRespawn>();

        _stateMachine = new EnemyStateMachine(this);

        SetData();
    }
    private void OnEnable()
    {
        SetData();

        if (GameManager.instance.GlobalTimeManager.NightCheck())
        {
            EnemyDamage *= 2f;
            EnemyMaxHealth *= 2f;
            EnemyDropEXP *= 2;
        }
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
        // 경험치
        _stateMachine.Target.PlayerExpSystem.EnemyExpPlus(EnemyDropEXP);
        //드랍아이템
        if(Data.DropItem != null)
        {
            int drop = UnityEngine.Random.Range(0, Data.DropItem.Length + 1);
            for (int i = 0; i < drop; ++i)
            {
                Instantiate(Data.DropItem[i], transform.position + Vector3.up * 2, Quaternion.identity);
            }
        }
        //콜라이더 비활성화
        Collider.enabled = false;
    }

    private void SetData()
    {
        EnemyDamage = Data.Damage;
        EnemyMaxHealth = Data.MaxHealth;
        EnemyDropEXP = Data.DropEXP;
    }
    
}
