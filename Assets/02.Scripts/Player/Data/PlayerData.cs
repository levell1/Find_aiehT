using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class PlayerData
{
    [SerializeField]
    [JsonProperty("PlayerName")]
    private string _playerName;

    [SerializeField]
    [JsonProperty("PlayerLevel")]
    private int _playerLevel;

    [SerializeField]
    [JsonProperty("PlayerMaxHealth")]
    private float _playerMaxHealth;

    [SerializeField]
    [JsonProperty("PlayerMaxStamina")]
    private float _playerMaxStamina;

    [SerializeField]
    [JsonProperty("PlayerAttack")]
    private float _playerAttack;

    [SerializeField]
    [JsonProperty("PlayerDef")]
    private float _playerDef;

    [SerializeField]
    [JsonProperty("PlayerExp")]
    private float _playerExp;

    [SerializeField]
    [JsonProperty("PlayerGold")]
    private float _playerGold;

    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int PlayerLevel { get => _playerLevel; set => _playerLevel = value; }
    public float PlayerMaxHealth { get => _playerMaxHealth; set => _playerMaxHealth = value; }
    public float PlayerMaxStamina { get => _playerMaxStamina; set => _playerMaxStamina = value; }
    public float PlayerAttack { get => _playerAttack; set => _playerAttack = value; }
    public float PlayerDef { get => _playerDef; set => _playerDef = value; }
    public float PlayerExp { get => _playerExp; set => _playerExp = value; }
    public float PlayerGold { get => _playerGold; set => _playerGold = value; }
}
