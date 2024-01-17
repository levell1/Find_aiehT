using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }
    public Animator Animator { get; private set; }

    private EnemyStateMachine _stateMachine;

    void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();

        _stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdlingState);
    }

    private void Update()
    {
        _stateMachine.HandleInput();

        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhyscisUpdate();
    }
}
