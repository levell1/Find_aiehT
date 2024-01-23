
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager = new UIManager();
    private SoundManager _soundManager = new SoundManager();
    private ResourceManager _resourceManager = new ResourceManager();
    private PoolingManager _poolingManager = new PoolingManager();

    public UIManager UIManager { get { return instance._uiManager; } }
    public SoundManager SoundManager { get { return instance._soundManager; } }
    public ResourceManager ResourceManager { get { return instance._resourceManager; } }
    public PoolingManager PoolingManager { get { return instance._poolingManager; } }

    protected override void Awake()
    {
        base.Awake();
        _soundManager = FindObjectOfType<SoundManager>();
        _poolingManager = FindObjectOfType<PoolingManager>();
    }

    void Start()
    {
        UIManager.CreateCanvas();
    }

    void Update()
    {
        
    }
}
