using System;
using UnityEngine;

public class TitleSceneButtonEvent : MonoBehaviour
{
    private SaveDataManager _saveDataManager;
    private GlobalTimeManager _globalTimeManager;

    private void Awake()
    {
        _saveDataManager = GameManager.Instance.SaveDataManger;
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void NewGameButton()
    {
        //데이터 초기화 코드
        GameManager.Instance.JsonReaderManager.InitPlayerData();
        GameManager.Instance.GameStateManager.CurrentGameState = GameState.NEWGAME;
        LoadingSceneController.LoadScene(SceneName.VillageScene);

        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _globalTimeManager.gameObject.SetActive(true);



        //HealthSystem healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        //healthSystem.SetCurHealth(); // NewGame
    }
    public void LoadButton()
    {
        GameManager.Instance.JsonReaderManager.LoadPlayerData();
        GameManager.Instance.GameStateManager.CurrentGameState = GameState.LOADGAME;
        LoadingSceneController.LoadScene(GameManager.Instance.JsonReaderManager.LoadedPlayerData.CurrentSceneName);

        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        _globalTimeManager.gameObject.SetActive(true);


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
