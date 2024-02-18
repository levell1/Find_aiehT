using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerExpSystem : MonoBehaviour
{
    private PlayerSO _playerData;

    public int PlayerLevel; // 현재 플레이어 레벨
    public int MaxExp; // 전체 경험치
    public int PlayerExp; // 플레이어 경험치

    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    public event Action<float, float> OnChangeExpUI;

    public event Action<int> OnLevelUp;
    public event Action<float, float> OnChangeHpUI;
    public event Action<float, float> OnChangeSpUI;

    private GameStateManager _gameStateManager;

    private void Start()
    {
        _gameStateManager = GameManager.Instance.GameStateManager;
        _healthSystem = gameObject.GetComponent<HealthSystem>();
        _staminaSystem = gameObject.GetComponent<StaminaSystem>();
        _playerData = GetComponent<Player>().Data;

        SetPlayerLevel();
        SetPlayerExp();

        //Debug.Log("현재 경험치: " + _playerExp);
        //Debug.Log("전체 경험치: " + _maxExp);

    }

    private void SetPlayerLevel()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            int loadPlayerLevel = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerLevel;
            
            PlayerLevel = loadPlayerLevel;

            _playerData.PlayerData.SetPlayerLevel(PlayerLevel);
            _playerData.PlayerLevelData.ApplyNextLevelData(_playerData.PlayerData, PlayerLevel);

            OnLevelUp?.Invoke(PlayerLevel);
            return;
        }

        PlayerLevel = _playerData.PlayerData.GetPlayerLevel();

        OnLevelUp?.Invoke(PlayerLevel);
    }

    private void SetPlayerExp()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            int loadPlayerExp = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerExp;
            PlayerExp = loadPlayerExp;
            MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).GetExp();
            OnChangeExpUI?.Invoke(PlayerExp, MaxExp);

            return;
        }

        PlayerExp = _playerData.PlayerData.GetPlayerExp();
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).GetExp();
        OnChangeExpUI?.Invoke(PlayerExp, MaxExp);
    }

    public void GetExpPlus(int getExp)
    {
        PlayerExp += getExp;
        OnChangeExpUI?.Invoke(PlayerExp, MaxExp);

        if( PlayerExp >= MaxExp )
        {
            LevelUp();
        }

    }

    private void LevelUp()
    {
        PlayerExp -= MaxExp;

        PlayerLevel++;

        _playerData.PlayerData.SetPlayerLevel(PlayerLevel);
        _playerData.PlayerData.SetPlayerExp(PlayerExp);

        _playerData.PlayerLevelData.ApplyNextLevelData(_playerData.PlayerData, PlayerLevel);
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).GetExp();

        _healthSystem.SetMaxHealth();
        _healthSystem.Health = _healthSystem.MaxHealth;
        OnChangeHpUI?.Invoke(_healthSystem.Health, _healthSystem.MaxHealth);

        _staminaSystem.SetMaxStamina();
        _staminaSystem.Stamina = _staminaSystem.MaxStamina;

        OnLevelUp?.Invoke(PlayerLevel);
        OnChangeExpUI?.Invoke(PlayerExp, MaxExp);

        GameManager.Instance.EffectManager.PlayLevelUpEffect();
        GameManager.Instance.EffectManager.PlayerLowHpEffect(false);
        //Debug.Log("레벨업!");

        //int a = _playerData.PlayerData.GetPlayerLevel();
        //int b = _playerData.PlayerData.GetPlayerMaxHealth();
        //int c = _playerData.PlayerData.GetPlayerMaxStamina();
        //int d = _playerData.PlayerData.GetPlayerAtk();
        //int e = _playerData.PlayerData.GetPlayerDef();
        //int f = _playerData.PlayerData.GetPlayerExp();

        //Debug.Log("level" + a);
        //Debug.Log("PlayerMaxHealth" + b);
        //Debug.Log("PlayerMaxStamina" + c);
        //Debug.Log("PlayerAttack" + d);
        //Debug.Log("PlayerDef" + e);
        //Debug.Log("PlayerExp" + f);

    }

    //IEnumerator TestCoroutine()
    //{
    //    int enemyExp = 13;

    //    while(true)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        EnemyExpPlus(enemyExp);
    //    }
    //}


}
