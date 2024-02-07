using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TycoonUI : MonoBehaviour
{
    [Header("Tycoon Base UI")]
    [SerializeField] private TMP_Text _currentGoldText;
    [SerializeField] private TMP_Text _remainingCustomerText;

    [Header("Result UI")]
    [SerializeField] private GameObject _resultUI;
    [SerializeField] private TMP_Text _resultCustomerText;
    [SerializeField] private TMP_Text _resultGoldText;
    [SerializeField] private TMP_Text _playerGoldText;

    int _currentGold = 0;
    int _todayArrivalCustomerNum = 0;
    int _todayMaxCustomerNum;

    private PlayerSO _playerData;

    private void Start()
    {
        _currentGoldText.text = "0";
        _remainingCustomerText.text = "0 / 0";
        _todayMaxCustomerNum = TycoonManager.Instance.TodayMaxCustomerNum;

        if (_playerData == null)
        {
            _playerData = GameObject.FindWithTag("Player").GetComponent<Player>().Data;
        }
    }

    public void UpdateCurrentGold(int gold)
    {
        _currentGold += gold;
        _currentGoldText.text = $"{_currentGold}";
    }
    
    public void UpdateInitUI()
    {
        _remainingCustomerText.text =
            $"{_todayArrivalCustomerNum} / {_todayMaxCustomerNum}";
    }

    public void UpdateRemainingCustomerNum()
    {
        ++_todayArrivalCustomerNum;
        _remainingCustomerText.text = 
            $"{_todayArrivalCustomerNum} / {_todayMaxCustomerNum}";
    }

    public void OnReusltUI()
    {
        UpdateResultUI();
        _resultUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void UpdateResultUI()
    {
        _resultCustomerText.text
            = $"{_todayArrivalCustomerNum - TycoonManager.Instance.AngryCustomerNum} / {_todayMaxCustomerNum}";
        _resultGoldText.text = $"{_currentGold}";

        _playerGoldText.text = $"{UpdatePlayerGoldData(_currentGold)}";
    }

    public int UpdatePlayerGoldData(int todayEarnGold)
    {
        int playerGold = _playerData.PlayerData.GetPlayerGold();
        playerGold += todayEarnGold;
        _playerData.PlayerData.SetPlayerGold(playerGold);

        return playerGold;
    }

    public void GoVillageScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.CameraManager.NonTycoonCamSetting();
        LoadingSceneController.LoadScene(SceneName.VillageScene);
    }
}
