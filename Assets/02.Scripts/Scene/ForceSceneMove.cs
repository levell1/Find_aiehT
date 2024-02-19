using System.Collections;
using UnityEngine;

public class ForceSceneMove : MonoBehaviour
{
    private RestartUI _restartUI;
    private TimeToVillageUI _timeToVillageUI;
    private HealthSystem _playerHealthSystem;

    private void Start()
    {
        _restartUI = GameManager.Instance.UIManager.PopupDic[UIName.RestartUI].GetComponent<RestartUI>();
        _timeToVillageUI = GameManager.Instance.UIManager.PopupDic[UIName.TimeToVillageUI].GetComponent<TimeToVillageUI>();
        _playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        _playerHealthSystem.OnDie += DieToGoHome;
        GameManager.Instance.GlobalTimeManager.OnOutFieldUI += TimeToGoHome;
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

    private void TimeToGoHome()
    {
        StartCoroutine(ActiveTimeToVillageUI());
    }

    private IEnumerator ActiveTimeToVillageUI()
    {
        yield return new WaitForSeconds(2f);
        _timeToVillageUI.gameObject.SetActive(true);
    }
}
