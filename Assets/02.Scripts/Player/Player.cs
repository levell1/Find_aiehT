using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public FirstSkillCoolTimeController FirstSkillCoolTimeController { get; private set; }
    public SecondSkillCoolTimeController SecondSkillCoolTimeController { get; private set; }
    public PlayerUseHealthPotion PlayerUseHealthPotion { get; private set; }
    public PlayerUseStaminaPotion PlayerUseStaminaPotion { get; private set; }
    public Inventory Inventory { get; private set; }
    public ServingFood ServingFood { get; private set; }

    [field: Header("Weapon")]
    [field: SerializeField] public PlayerWeapon Weapon { get; private set; }
    [field: SerializeField] public PlayerSpreadSkill SandSkill { get; private set; }
    [field: SerializeField] public SkillParticle SkillParticle { get; private set; }
    public SkillInstantiator SkillInstantiator { get; private set; }

    [field: Header("Interact")]
    [field: SerializeField] public PlayerInteraction Interaction { get; private set; }

    [field: SerializeField] public Transform MainCameraTransform { get; set; }

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

        FirstSkillCoolTimeController = GetComponent<FirstSkillCoolTimeController>();
        SecondSkillCoolTimeController = GetComponent<SecondSkillCoolTimeController>();

        PlayerUseHealthPotion = GetComponent<PlayerUseHealthPotion>();
        PlayerUseStaminaPotion = GetComponent<PlayerUseStaminaPotion>();

        Inventory = GetComponent<Inventory>();

        SkillInstantiator = GetComponent<SkillInstantiator>();

        ServingFood = GetComponent<ServingFood>();


        _stateMachine = new PlayerStateMachine(this);

    }

    private void Start()
    {

        if(GameManager.Instance.GameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            Vector3 loadPosition = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerPosition.ToVector3();
            gameObject.transform.position = loadPosition;
        }

        _stateMachine.SceneName = CurrentSceneName();

        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);
        HealthSystem.OnDie += OnDie;

    }
    
    private void Init()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public string CurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private void OnEnable()
    {
        _stateMachine.SceneName = CurrentSceneName();

        if (SceneManager.GetActiveScene().name == SceneName.TycoonScene)
        {
            Weapon.gameObject.SetActive(false);
        }
        else
        {
            Weapon.gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name != SceneName.LoadingScene)
            Init();
    }

    private void Update()
    {
        if(GameManager.Instance.GameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            GameManager.Instance.GameStateManager.CurrentGameState = GameState.NEWGAME;
        }
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
        StartCoroutine(DieDelay());
    }

    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(0.1f);
        enabled = false;
    }

}