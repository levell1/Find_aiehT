using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    public EnemyStateMachine StateMachine;

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

        SetData();
    }

    private void Start()
    {
        StateMachine = new EnemyStateMachine(this);
        StateMachine.ChangeState(StateMachine.IdlingState);
        HealthSystem.OnDie += OnDie;
    }

    private void Update()
    {
        StateMachine.HandleInput();

        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        PatrolDelay += Time.deltaTime;
        AttackDelay += Time.deltaTime;

        StateMachine.PhyscisUpdate();
    }

    private void OnDie()
    {
        // 경험치
        StateMachine.Target.PlayerExpSystem.GetExpPlus(EnemyDropEXP);
        //드랍아이템
        if(Data.DropItem != null)
        {
            float dropValue = UnityEngine.Random.Range(0f, 1f);
            for (int i = 0; i < Data.DropItem.Length; ++i)
            {
                if (dropValue < Data.DropPercent)
                {
                    Instantiate(Data.DropItem[i], transform.position + Vector3.up * 2, Quaternion.identity);
                }
            }
        }
        //콜라이더 비활성화
        Collider.enabled = false;
    }

    public void SetData()
    {
        EnemyDamage = Data.Damage;
        EnemyMaxHealth = Data.MaxHealth;
        EnemyDropEXP = Data.DropEXP;
        if (SceneManager.GetActiveScene().name == SceneName.DungeonScene)
        {
            EnemyDamage *= 3;
            EnemyMaxHealth *= 3;
            EnemyDropEXP *= 2;
        }
    }
    
}
