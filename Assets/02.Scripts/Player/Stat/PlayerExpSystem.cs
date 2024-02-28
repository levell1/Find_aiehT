using System;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour
{
    private PlayerSO _playerData;

    public int PlayerLevel; 
    public int MaxExp; 
    public int PlayerExp;

    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    public event Action<float, float> OnChangeExpUI;

    public event Action OnChangeEnemyName; 
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

    }

    private void SetPlayerLevel()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            int loadPlayerLevel = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerLevel;
            
            PlayerLevel = loadPlayerLevel;

            _playerData.PlayerData.PlayerLevel = PlayerLevel;
            _playerData.PlayerLevelData.ApplyNextLevelData(_playerData.PlayerData, PlayerLevel - 1);

            OnLevelUp?.Invoke(PlayerLevel);
            return;
        }

        PlayerLevel = _playerData.PlayerData.PlayerLevel;

        OnLevelUp?.Invoke(PlayerLevel);
    }

    private void SetPlayerExp()
    {
        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            int loadPlayerExp = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerExp;
            PlayerExp = loadPlayerExp;
            MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).Exp;
            OnChangeExpUI?.Invoke(PlayerExp, MaxExp);

            return;
        }

        PlayerExp = _playerData.PlayerData.PlayerExp;
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).Exp;
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
        if (PlayerLevel == 10)
            return;

        PlayerExp -= MaxExp;
        PlayerLevel++;

        _playerData.PlayerData.PlayerLevel = PlayerLevel;
        _playerData.PlayerData.PlayerExp = PlayerExp;

        _playerData.PlayerLevelData.ApplyNextLevelData(_playerData.PlayerData, PlayerLevel - 1);
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).Exp;

        _healthSystem.SetMaxHealth();
        _healthSystem.Health = _healthSystem.MaxHealth;
        OnChangeHpUI?.Invoke(_healthSystem.Health, _healthSystem.MaxHealth);

        _staminaSystem.SetMaxStamina();
        _staminaSystem.Respawn();

        OnChangeEnemyName?.Invoke(); 
        OnLevelUp?.Invoke(PlayerLevel);
        OnChangeExpUI?.Invoke(PlayerExp, MaxExp);

        GameManager.Instance.EffectManager.PlayLevelUpEffect();
        GameManager.Instance.EffectManager.PlayerLowHpEffect(false);

    }
}
