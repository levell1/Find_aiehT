
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private UIManager _uiManager;
    private SoundManager _soundManager;
    private ResourceManager _resourceManager = new ResourceManager();


    public UIManager UIManager { get { return instance._uiManager; } }
    public SoundManager SoundManager { get { return instance._soundManager; } }
    public ResourceManager ResourceManager { get { return instance._resourceManager; } }

    protected override void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
