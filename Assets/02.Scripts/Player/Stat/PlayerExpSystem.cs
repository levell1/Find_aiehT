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
    private int _playerExp; // 플레이어 경험치

    private HealthSystem _healthSystem;
    private StaminaSystem _staminaSystem;
    public event Action<float, float> OnChangeExpUI;

    public event Action<int> OnLevelUp;

    private void Start()
    {
        _healthSystem = gameObject.GetComponent<HealthSystem>();
        _staminaSystem = gameObject.GetComponent<StaminaSystem>();
        _playerData = GetComponent<Player>().Data;
        PlayerLevel = _playerData.PlayerData.GetPlayerLevel();
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).GetExp();
        OnLevelUp?.Invoke(PlayerLevel);
        OnChangeExpUI?.Invoke(_playerExp, MaxExp);
        _playerExp = _playerData.PlayerData.GetPlayerExp();

        //Debug.Log("현재 경험치: " + _playerExp);
        //Debug.Log("전체 경험치: " + _maxExp);

    }

    public void GetExpPlus(int getExp)
    {
        _playerExp += getExp;
        OnChangeExpUI?.Invoke(_playerExp, MaxExp);

        if( _playerExp >= MaxExp )
        {
            LevelUp();
        }

    }

    private void LevelUp()
    {
        _playerExp -= MaxExp;

        PlayerLevel++;

        _playerData.PlayerData.SetPlayerLevel(PlayerLevel);
        _playerData.PlayerData.SetPlayerExp(_playerExp);

        _playerData.PlayerLevelData.ApplyNextLevelData(_playerData.PlayerData, PlayerLevel);
        MaxExp = _playerData.PlayerLevelData.GetLevelData(PlayerLevel - 1).GetExp();

        _healthSystem.SetMaxHealth();
        _staminaSystem.SetMaxStamina();
        OnLevelUp?.Invoke(PlayerLevel);
        OnChangeExpUI?.Invoke(_playerExp, MaxExp);

        GameManager.Instance.EffectManager.PlayLevelUpEffect();

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
