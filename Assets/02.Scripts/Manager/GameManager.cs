using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject Player;
   
    private UIManager _uiManager = new UIManager();
    private ResourceManager _resourceManager = new ResourceManager();
    private SoundManager _soundManager = new SoundManager();
    private PoolingManager _poolingManager = new PoolingManager();
    private GlobalTimeManager _globalTimeManager = new GlobalTimeManager();
    private DataManager _dataManager = new DataManager();
    private CameraManager _cameraManager = new CameraManager();
    private Inventory _inventory;

    
    public UIManager UIManager { get { return Instance._uiManager; } }
    public SoundManager SoundManager { get { return Instance._soundManager; } }
    public ResourceManager ResourceManager { get { return Instance._resourceManager; } }
    public PoolingManager PoolingManager { get { return Instance._poolingManager; } }
    public GlobalTimeManager GlobalTimeManager { get { return Instance._globalTimeManager; } }
    public DataManager DataManager { get { return Instance._dataManager; } }
    public CameraManager CameraManager { get { return Instance._cameraManager; } }
    public Inventory Inventory { get { return Instance._inventory; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        _soundManager = FindObjectOfType<SoundManager>();
        _poolingManager = FindObjectOfType<PoolingManager>();
        _dataManager = FindObjectOfType<DataManager>();
        _cameraManager = FindObjectOfType<CameraManager>();
        _inventory = Player.GetComponent<Inventory>();
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
