
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager = new UIManager();
    private SoundManager _soundManager = new SoundManager();
    private ResourceManager _resourceManager = new ResourceManager();


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
