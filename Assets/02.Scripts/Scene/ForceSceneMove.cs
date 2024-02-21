using System.Collections;
using UnityEngine;

public class ForceSceneMove : MonoBehaviour
{
    private SceneMoveUI _sceneMoveUI;
    private RestartUI _restartUI;
    private TimeToVillageUI _timeToVillageUI;
    private HealthSystem _playerHealthSystem;
    private GlobalTimeManager _globalTimeManager;

    private void Start()
    {
        _globalTimeManager = GameManager.Instance.GlobalTimeManager;
        UIManager uIManager = GameManager.Instance.UIManager;
        _sceneMoveUI = uIManager.PopupDic[UIName.SceneMoveUI].GetComponent<SceneMoveUI>();
        _restartUI = uIManager.PopupDic[UIName.RestartUI].GetComponent<RestartUI>();
        _timeToVillageUI = uIManager.PopupDic[UIName.TimeToVillageUI].GetComponent<TimeToVillageUI>();

        _playerHealthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        _playerHealthSystem.OnDie += DieToGoHome;
        _globalTimeManager.OnOutFieldUI += TimeToGoHome;
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
        if (_globalTimeManager.EventCount == 0)
        {
            // 타이쿤 시간 됬을 때
            _timeToVillageUI.gameObject.SetActive(true); ;
        }
        else
        {

            //24시 or 잠자기
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Sleep);

            _sceneMoveUI.CurrentSceneName = SceneName.VillageScene;
            _sceneMoveUI.Description.text = "어우 졸~려ㅓㅓㅓㅓ";
            _sceneMoveUI.gameObject.SetActive(true);
        }
    }
}
