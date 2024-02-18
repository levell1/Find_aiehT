using UnityEngine;

public class ForceSceneMove : MonoBehaviour
{
    private RestartUI _restartUI;
    private HealthSystem _playerHealthSystem;

    private void Start()
    {
        _restartUI = GameManager.Instance.UIManager.PopupDic[UIName.RestartUI].GetComponent<RestartUI>();
        _playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        _playerHealthSystem.OnDie += DieToGoHome;
        //강제 이동되는 부분 추가
    }

    private void DieToGoHome()
    {
        _restartUI.Description.text = "마을에서 재시작 합니다.";
        _restartUI.gameObject.SetActive(true);
    }
}
