
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager = new UIManager();
    private SoundManager _soundManager = new SoundManager();
    private ResourceManager _resourceManager = new ResourceManager();
    private PoolingManager _poolingManager = new PoolingManager();
    private TycoonManager _tycoonManager = new TycoonManager();

    public UIManager UIManager { get { return instance._uiManager; } }
    public SoundManager SoundManager { get { return instance._soundManager; } }
    public ResourceManager ResourceManager { get { return instance._resourceManager; } }
    public PoolingManager PoolingManager { get { return instance._poolingManager; } }
    public TycoonManager TycoonManager { get { return instance._tycoonManager; } }

    protected override void Awake()
    {
        base.Awake();
        _soundManager = FindObjectOfType<SoundManager>();
        _poolingManager = GetComponentInChildren<PoolingManager>();
        _tycoonManager = FindObjectOfType<TycoonManager>();
    }

    void Start()
    {
        UIManager.CreateCanvas();
    }

    void Update()
    {
        
    }
}
