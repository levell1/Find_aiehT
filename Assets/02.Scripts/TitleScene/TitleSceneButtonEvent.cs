using UnityEngine;
using UnityEngine.UI;

public class TitleSceneButtonEvent : MonoBehaviour
{
    private SaveDataManager _saveDataManager;
    private GlobalTimeManager _globalTimeManager;
    private QuestManager _questManager;
    private JsonReader _jsonReader;

    public Button NewGameButton;
    public Button LoadGameButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _keyButton;
    [SerializeField] private Button _settingButton;
    
    private void Awake()
    {
        _saveDataManager = GameManager.Instance.SaveDataManger;
        _jsonReader = GameManager.Instance.JsonReaderManager;

        NewGameButton.onClick.AddListener(() => OnNewGameButtonEvent());
        LoadGameButton.onClick.AddListener(() => OnLoadGameButtonEvent());
        _exitButton.onClick.AddListener(ExitButton);
        _keyButton.onClick.AddListener(ControlKeyButton);
        _settingButton.onClick.AddListener(OptionButton);
        
        if(_jsonReader.CheckJsonFileExist())
        {
            LoadGameButton.interactable = true;
        }
        else
        {
            LoadGameButton.interactable = false;
        }

    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnNewGameButtonEvent()
    {
        GameManager.Instance.JsonReaderManager.InitPlayerData();
        GameManager.Instance.GameStateManager.CurrentGameState = GameState.NEWGAME;
        LoadingSceneController.LoadScene(SceneName.TutorialScene);

        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _questManager = GameManager.Instance.QuestManager;

        _globalTimeManager.gameObject.SetActive(true);
        _questManager.gameObject.SetActive(true);
    }
    public void OnLoadGameButtonEvent()
    {
        GameManager.Instance.JsonReaderManager.LoadPlayerData();
        GameManager.Instance.GameStateManager.CurrentGameState = GameState.LOADGAME;
        LoadingSceneController.LoadScene(GameManager.Instance.JsonReaderManager.LoadedPlayerData.CurrentSceneName);

        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _questManager = GameManager.Instance.QuestManager;

        _globalTimeManager.gameObject.SetActive(true);
        _questManager.gameObject.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }

    public void ControlKeyButton() 
    {
        GameManager.Instance.UIManager.ShowCanvas(UIName.ControlKeyUI);
    }
    public void OptionButton()
    {
        GameManager.Instance.UIManager.ShowCanvas(UIName.SettingUI);
    }
}
