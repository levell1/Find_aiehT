using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    /// 저장하기
    /// 1. 플레이어 현재 데이터 저장
    /// 2. 플레이어 강화 상태 저장
    /// 3. 퀘스트 진행상황 저장
    /// 4. 날짜 및 시간 저장
    /// 5. 플레이어 위치 저장
    /// 6. 플레이어 포션 및 인벤토리 저장

    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    private PlayerExpSystem _expSystem;

    private Player _player;

    [HideInInspector] public float SaveHealth;
    [HideInInspector] public float SaveStamina;
    [HideInInspector] public Vector3 SavePlayerPosition;
    [HideInInspector] public int SavePlayerLevel;

    // TODO SaveDataManager 에서 받아온 내용 
    public void SavePlayerCurrentStateData()
    {
        _healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        _staminaSystem = GameManager.Instance.Player.GetComponent<StaminaSystem>();
        _expSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();

        // SaveHealth = 10;

        SaveHealth = _healthSystem.Health;
        SaveStamina = _staminaSystem.Stamina;
        SavePlayerLevel = _expSystem.PlayerLevel;
    }

    public void SavePlayerData()
    {
        _player =GameManager.Instance.Player.GetComponent<Player>();

        SavePlayerPosition = _player.transform.position;

        //_playerSO = GameManager.Instance.Player.GetComponent<Player>().Data;

    }

    public void SavePlayerEquipData()
    {

    }

    public void SaveGlobalTimeData()
    {

    }

    public void SavePlayerPositionData()
    {

    }

    public void SavePlayerInventoryData()
    {

    }

    public void SavePlayerQuestData()
    {

    }

}
