using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }


    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Collider Collider { get; private set; }

    public GroundCheck GroundCheck { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public DashForceReceiver DashForceReceiver { get; private set; }
    public StaminaSystem StaminaSystem { get; private set; }
    public HealthSystem HealthSystem{ get; private set; }
    public PlayerExpSystem PlayerExpSystem { get; private set; }
    public SkillCoolTimeController SkillCoolTimeController { get; private set; }

    [field: Header("Weapon")]
    [field:SerializeField] public PlayerWeapon Weapon { get; private set; }

    private PlayerStateMachine _stateMachine;
    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();

        GroundCheck = GetComponent<GroundCheck>();
        ForceReceiver = GetComponent<ForceReceiver>();
        DashForceReceiver = GetComponent<DashForceReceiver>();

        StaminaSystem = GetComponent<StaminaSystem>();
        HealthSystem = GetComponent<HealthSystem>();
        PlayerExpSystem = GetComponent<PlayerExpSystem>();

        SkillCoolTimeController = GetComponent<SkillCoolTimeController>();

        _stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);
        HealthSystem.OnDie += OnDie;
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

    private void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }

}