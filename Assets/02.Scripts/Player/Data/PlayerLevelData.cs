using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfoData
{

    [SerializeField] private int Level;
    [SerializeField] private int Health;
    [SerializeField] private int Stamina;
    [SerializeField] private int Attack;
    [SerializeField] private int Defence;
    [SerializeField] private int Exp;

    public int GetLevel() { return Level; }
    public int GetHealth() { return Health; }
    public int GetStamina() { return Stamina; }
    public int GetAttack() { return Attack; }
    public int GetDefence() { return Defence; }
    public int GetExp() { return Exp; }

}

public class PlayerLevelData
{
    [field: SerializeField] public List<LevelInfoData> LevelInfoData;

    public LevelInfoData GetLevelData(int playerLevel) { return LevelInfoData[playerLevel]; }

    public void ApplyNextLevelData(PlayerData playerData, int nextLevel)
    {
        LevelInfoData nextLevelData = GetLevelData(nextLevel);

        if (nextLevelData != null)
        {
            playerData.SetPlayerMaxHealth(nextLevelData.GetHealth());
            playerData.SetPlayerMaxStamina(nextLevelData.GetStamina());
            playerData.SetPlayerAttack(nextLevelData.GetAttack());
            playerData.SetPlayerDef(nextLevelData.GetDefence());
        }
    }
}
