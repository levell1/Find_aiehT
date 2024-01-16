
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager;
    private SoundManager _soundManager;
    private ResourceManager _resourceManager;


    public UIManager UIManager { get { return _uiManager; } }
    public SoundManager SoundManager { get { return _soundManager; } }
    public ResourceManager ResourceManager { get { return _resourceManager; } }

    protected override void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _soundManager = FindObjectOfType<SoundManager>();
        _resourceManager = FindObjectOfType<ResourceManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
