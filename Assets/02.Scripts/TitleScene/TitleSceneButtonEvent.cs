using UnityEngine;
using UnityEngine.UI;

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

    public void KeyButton() 
    {
        GameManager.instance.UIManager.ShowCanvas(UIName.ControlKeyUI);
    }
    public void OptionButton()
    {
        GameManager.instance.UIManager.ShowCanvas(UIName.SettingUI);
    }
}
