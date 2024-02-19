using System.Collections;
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
        StartCoroutine(ActiveReStartUI());
    }

    private IEnumerator ActiveReStartUI()
    {
        yield return new WaitForSeconds(2f);
        _restartUI.gameObject.SetActive(true);
    }
}
