using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private string PlayerName;
    [SerializeField] private int PlayerLevel;
    [SerializeField] private float PlayerMaxHealth;
    [SerializeField] private float PlayerMaxStamina;
    [SerializeField] private float PlayerAttack;
    [SerializeField] private float PlayerDef;
    [SerializeField] private int PlayerExp;
    [SerializeField] private int PlayerGold;

    public string GetPlayerName() { return PlayerName; }
    public int GetPlayerLevel() { return PlayerLevel; }
    public float GetPlayerMaxHealth() { return PlayerMaxHealth; }
    public float GetPlayerMaxStamina() { return PlayerMaxStamina; }
    public float GetPlayerAtk() { return PlayerAttack; }
    public float GetPlayerDef() { return PlayerDef; }
    public int GetPlayerExp() { return PlayerExp; }
    public int GetPlayerGold() { return PlayerGold; }

    public void SetPlayerLevel(int value) { PlayerLevel = value; }
    public void SetPlayerMaxHealth(float value) { PlayerMaxHealth = value; }
    public void SetPlayerMaxStamina(float value) { PlayerMaxStamina = value; }
    public void SetPlayerAttack(float value) { PlayerAttack = value; }
    public void SetPlayerDef(float value) { PlayerDef = value; }
    public void SetPlayerExp(int value) { PlayerExp = value; }
    public void SetPlayerGold(int value) { PlayerGold = value; }

}

[Serializable]
public class PlayerJsonData
{
    [SerializeField] public PlayerData PlayerData;
}
