using System.Collections;
using UnityEngine;

public class ForceSceneMove : MonoBehaviour
{
    private SceneMoveUI _sceneMoveUI;
    private RestartUI _restartUI;
    private TimeToVillageUI _timeToVillageUI;
    private HealthSystem _playerHealthSystem;

    private void Start()
    {
        _sceneMoveUI = GameManager.Instance.UIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
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
        if (GameManager.Instance.GlobalTimeManager.EventCount == 0)
        {
            StartCoroutine(ActiveTimeToVillageUI());
        }
        else
        {
            _sceneMoveUI.Description.text = "어우 졸~려ㅓㅓㅓㅓ";
            _sceneMoveUI.gameObject.SetActive(true);
        }
    }

    private IEnumerator ActiveTimeToVillageUI()
    {
        yield return new WaitForSeconds(2f);
        _timeToVillageUI.gameObject.SetActive(true);
    }
}
