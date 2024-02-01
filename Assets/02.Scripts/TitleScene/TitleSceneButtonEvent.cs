using UnityEngine;

public class TitleSceneButtonEvent : MonoBehaviour
{

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void NewGameButton()
    {
        //데이터 초기화 코드
        LoadingSceneController.LoadScene(SceneName.VillageScene);
    }
    public void LoadButton()
    {
        // 로드?
        LoadingSceneController.LoadScene(SceneName.VillageScene);
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
