using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class PlayerData
{
    //[SerializeField]
    //[JsonProperty("PlayerName")]
    //private string _playerName;

    //[SerializeField]
    //[JsonProperty("PlayerLevel")]
    //private int _playerLevel;

    //[SerializeField]
    //[JsonProperty("PlayerMaxHealth")]
    //private float _playerMaxHealth;

    //[SerializeField]
    //[JsonProperty("PlayerMaxStamina")]
    //private float _playerMaxStamina;

    //[SerializeField]
    //[JsonProperty("PlayerAttack")]
    //private float _playerAttack;

    //[SerializeField]
    //[JsonProperty("PlayerDef")]
    //private float _playerDef;

    //[SerializeField]
    //[JsonProperty("PlayerExp")]
    //private float _playerExp;

    //[SerializeField]
    //[JsonProperty("PlayerGold")]
    //private float _playerGold;

    //[SerializeField]  private string _playerName;
    //[SerializeField]  private int _playerLevel;
    //[SerializeField]  private int _playerMaxHealth;
    //[SerializeField]  private int _playerMaxStamina;
    //[SerializeField]  private int _playerAttack;
    //[SerializeField]  private int _playerDef;
    //[SerializeField]  private int _playerExp;
    //[SerializeField]  private int _playerGold;

    [SerializeField] private string PlayerName;
    [SerializeField] private int PlayerLevel;
    [SerializeField] private int PlayerMaxHealth;
    [SerializeField] private int PlayerMaxStamina;
    [SerializeField] private int PlayerAttack;
    [SerializeField] private int PlayerDef;
    [SerializeField] private int PlayerExp;
    [SerializeField] private int PlayerGold;

    public string GetPlayerName() { return PlayerName; }
    public int GetPlayerLevel() { return PlayerLevel; }
    public int GetPlayerMaxHealth() { return PlayerMaxHealth; }
    public int GetPlayerMaxStamina() { return PlayerMaxStamina; }
    public int GetPlayerAtk() { return PlayerAttack; }
    public int GetPlayerDef() { return PlayerDef; }
    public int GetPlayerExp() { return PlayerExp; }
    public int GetPlayerGold() { return PlayerGold; }

    public void SetPlayerLevel(int value) { PlayerLevel = value; }
    public void SetPlayerMaxHealth(int value) { PlayerMaxHealth = value; }
    public void SetPlayerMaxStamina(int value) { PlayerMaxStamina = value; }
    public void SetPlayerAttack(int value) { PlayerAttack = value; }
    public void SetPlayerDef(int value) { PlayerDef = value; }
    public void SetPlayerExp(int value) { PlayerExp = value; }
    public void SetPlayerGold(int value) { PlayerGold = value; }

}

[Serializable]
public class PlayerJsonData
{
    [SerializeField] public PlayerData PlayerData;
}
