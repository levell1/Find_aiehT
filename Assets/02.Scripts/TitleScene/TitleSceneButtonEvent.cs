using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneButtonEvent : MonoBehaviour
{
    private SaveDataManager _saveDataManager;
    private GlobalTimeManager _globalTimeManager;
    private QuestManager _questManager;

    public Button NewGameButton;
    public Button LoadGameButton;

    private void Awake()
    {
        _saveDataManager = GameManager.Instance.SaveDataManger;
        NewGameButton.onClick.AddListener(() => OnNewGameButtonEvent());
        LoadGameButton.onClick.AddListener(() => OnLoadGameButtonEvent());

    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnNewGameButtonEvent()
    {
        //데이터 초기화 코드
        GameManager.Instance.JsonReaderManager.InitPlayerData();
        GameManager.Instance.GameStateManager.CurrentGameState = GameState.NEWGAME;
        LoadingSceneController.LoadScene(SceneName.VillageScene);

        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _questManager = GameManager.Instance.QuestManager;

        _globalTimeManager.gameObject.SetActive(true);
        _questManager.gameObject.SetActive(true);


        //HealthSystem healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        //healthSystem.SetCurHealth(); // NewGame
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

        //HealthSystem healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        //healthSystem.SetCurHealth(); // LoadGame
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
