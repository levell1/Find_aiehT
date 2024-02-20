using UnityEngine;

public class RestartUI : BaseUI
{
    private SceneMoveUI _sceneMoveUI;

    private void Start()
    {
        _sceneMoveUI = GameManager.Instance.UIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Time.timeScale = 0f;
        GameManager.Instance.CameraManager.DisableCam();
    }

    private void OnDisable()
    {
        GameManager.Instance.GlobalTimeManager.DayTime = 7f / 24f;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameManager.Instance.CameraManager.EnableCam();
    }

    public void GoVillageBtn()
    {
        _sceneMoveUI.CurrentSceneName = SceneName.VillageScene;
        _sceneMoveUI.Description.text = "마을로 돌아갑니다.";
        _sceneMoveUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    //public void GoTitleBtn()
    //{
    //    _sceneMoveUI.CurrentSceneName = SceneName.TitleScene;
    //    _sceneMoveUI.Description.text = "타이틀로 돌아갑니다.";
    //    _sceneMoveUI.gameObject.SetActive(true);
    //    gameObject.SetActive(false);
    //}

}
