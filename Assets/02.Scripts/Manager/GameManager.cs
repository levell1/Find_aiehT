
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager = new UIManager();
    private SoundManager _soundManager = new SoundManager();
    private ResourceManager _resourceManager = new ResourceManager();
    private PoolingManager _poolingManager = new PoolingManager();
    private GlobalTimeManager _globalTimeManager = new GlobalTimeManager();
    private Inventory _inventory = new Inventory();

    public UIManager UIManager { get { return instance._uiManager; } }
    public SoundManager SoundManager { get { return instance._soundManager; } }
    public ResourceManager ResourceManager { get { return instance._resourceManager; } }
    public PoolingManager PoolingManager { get { return instance._poolingManager; } }
    public GlobalTimeManager GlobalTimeManager { get { return instance._globalTimeManager; } }
    public Inventory Inventory { get { return instance._inventory; } }

    protected override void Awake()
    {
       base.Awake();
        _soundManager = FindObjectOfType<SoundManager>();
        _poolingManager = FindObjectOfType<PoolingManager>();
        _inventory = FindObjectOfType<Inventory>();
        _globalTimeManager = FindObjectOfType<GlobalTimeManager>();
    }

    private void Start()
    {
        _uiManager.CreateCanvas();
    }

    void Update()
    {
        
    }
}
