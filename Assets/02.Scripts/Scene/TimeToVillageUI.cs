using UnityEngine;

public class TimeToVillageUI : MonoBehaviour
{
    private SceneMoveUI _sceneMoveUI;

    private void Start()
    {
        _sceneMoveUI = GameManager.Instance.UIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
    }

    private void Update()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.CameraManager.DisableCam();
    }

    private void OnDisable()
    {
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

    public void StayHuntingBtn()
    {
        Time.timeScale = 1f;
        GameManager.Instance.GlobalTimeManager.NightChecker(); 
        GameManager.Instance.CameraManager.EnableCam();
        gameObject.SetActive(false);
    }
}
