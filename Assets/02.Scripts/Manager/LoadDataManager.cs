using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataManager : MonoBehaviour
{
    /// 불러오기
    /// 1. 플레이어 현재 데이터 불러오기
    /// 2. 플레이어 강화 상태 불러오기
    /// 3. 퀘스트 진행상황 불러오기
    /// 4. 날짜 및 시간 불러오기
    /// 5. 플레이어 위치 불러오기
    /// 6. 플레이어 포션 및 인벤토리 불러오기
    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    private PlayerExpSystem _expSystem;

    private Player _player;

    [HideInInspector] public float LoadHealth;
    [HideInInspector] public float LoadStamina;
    [HideInInspector] public Vector3 LoadPlayerPosition;
    [HideInInspector] public int LoadPlayerLevel;

    // TODO SaveDataManager 에서 받아온 내용 
    public void LoadPlayerCurrentStateData()
    {
        _healthSystem = GameManager.Instance.Player.GetComponent<HealthSystem>();
        _staminaSystem = GameManager.Instance.Player.GetComponent<StaminaSystem>();
        _expSystem = GameManager.Instance.Player.GetComponent<PlayerExpSystem>();

        LoadHealth = _healthSystem.Health;
        LoadStamina = _staminaSystem.Stamina;
        LoadPlayerLevel = _expSystem.PlayerLevel;
    }

    public void LoadPlayerData()
    {
        _player = GameManager.Instance.Player.GetComponent<Player>();

        LoadPlayerPosition = _player.transform.position;
    }

    public void LoadPlayerEquipData()
    {

    }

    public void LoadGlobalTimeData()
    {

    }

    public void LoadPlayerPositionData()
    {

    }

    public void LoadPlayerInventoryData()
    {

    }

    public void LoadPlayerQuestData()
    {

    }
}
