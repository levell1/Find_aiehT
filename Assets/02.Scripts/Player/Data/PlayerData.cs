using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class PlayerData
{
    public Action<int> OnGoldUI;
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
        set { _playerGold = value; OnGoldUI?.Invoke(value); }
    }

}


    //public string GetPlayerName() { return PlayerName; }
    //public int GetPlayerLevel() { return PlayerLevel; }
    //public float GetPlayerMaxHealth() { return PlayerMaxHealth; }
    //public float GetPlayerMaxStamina() { return PlayerMaxStamina; }
    //public float GetPlayerAtk() { return PlayerAttack; }
    //public float GetPlayerDef() { return PlayerDef; }
    //public int GetPlayerExp() { return PlayerExp; }
    //public int GetPlayerGold() { return PlayerGold; }

    //public void SetPlayerLevel(int value) { PlayerLevel = value; }
    //public void SetPlayerMaxHealth(float value) { PlayerMaxHealth = value; }
    //public void SetPlayerMaxStamina(float value) { PlayerMaxStamina = value; }
    //public void SetPlayerAttack(float value) { PlayerAttack = value; }
    //public void SetPlayerDef(float value) { PlayerDef = value; }
    //public void SetPlayerExp(int value) { PlayerExp = value; }
    //public void SetPlayerGold(int value) { PlayerGold = value;  }

  
[Serializable]
public class PlayerJsonData
{
    [SerializeField] public PlayerData PlayerData;
}

[Serializable]
public class PlayerSaveData
{

    [SerializeField] private int _playerLevel;
    [SerializeField] private float _playerMaxHealth;
    [SerializeField] private float _playerMaxStamina;
    [SerializeField] private float _playerAttack;
    [SerializeField] private float _playerDef;
    [SerializeField] private int _playerExp;
    [SerializeField] private int _playerGold;
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
        set { _playerGold = value; }
    }
    //[SerializeField] private int PlayerLevel;
    //[SerializeField] private float PlayerMaxHealth;
    //[SerializeField] private float PlayerMaxStamina;
    //[SerializeField] private float PlayerAttack;
    //[SerializeField] private float PlayerDef;
    //[SerializeField] private int PlayerExp;
    //[SerializeField] private int PlayerGold;

    //public void SetPlayerLevel(int value) { PlayerLevel = value; }
    //public void SetPlayerMaxHealth(float value) { PlayerMaxHealth = value; }
    //public void SetPlayerMaxStamina(float value) { PlayerMaxStamina = value; }
    //public void SetPlayerAttack(float value) { PlayerAttack = value; }
    //public void SetPlayerDef(float value) { PlayerDef = value; }
    //public void SetPlayerExp(int value) { PlayerExp = value; }
    //public void SetPlayerGold(int value) { PlayerGold = value; }
}

[Serializable]
public class PlayerLoadJsonData
{
    [SerializeField] public PlayerSaveData PlayerSaveData;
}