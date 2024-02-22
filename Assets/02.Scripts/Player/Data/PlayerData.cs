using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Action<int> OnGoldUI;
    public Action<int> OnAccumulateGold;

    [SerializeField] private string _playerName;
    [SerializeField] private int _playerLevel;
    [SerializeField] private float _playerMaxHealth;
    [SerializeField] private float _playerMaxStamina;
    [SerializeField] private float _playerAttack;
    [SerializeField] private float _playerDef;
    [SerializeField] private int _playerExp;
    [SerializeField] private int _playerGold;

    public string PlayerName
    {
        get { return _playerName; }
        set { _playerName = value; }
    }

    public int PlayerLevel
    {
        get { return _playerLevel; }
        set { _playerLevel = value; }
    }

    public float PlayerMaxHealth
    {
        get { return _playerMaxHealth; }
        set { _playerMaxHealth = value; }
    }

    public float PlayerMaxStamina
    {
        get { return _playerMaxStamina; }
        set { _playerMaxStamina = value; }
    }

    public float PlayerAttack
    {
        get { return _playerAttack; }
        set { _playerAttack = value; }
    }
    public float PlayerDef
    {
        get { return _playerDef; }
        set { _playerDef = value; }
    }

    public int PlayerExp
    {
        get { return _playerExp; }
        set { _playerExp = value; }
    }

    public int PlayerGold
    {
        get { return _playerGold; }
        set 
        {
            _playerGold = value;  OnGoldUI?.Invoke(value);
            OnAccumulateGold?.Invoke(30004);
        }
    }

}

[Serializable]
public class PlayerJsonData
{
    [SerializeField] public PlayerData PlayerData;
}