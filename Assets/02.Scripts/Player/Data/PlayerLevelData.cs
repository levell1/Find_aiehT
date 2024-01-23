using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfoData
{

    [SerializeField] private int Level;
    [SerializeField] private float Health;
    [SerializeField] private float Stamina;
    [SerializeField] private float Attack;
    [SerializeField] private float Defence;
    [SerializeField] private int Exp;

    public int GetLevel() { return Level; }
    public float GetHealth() { return Health; }
    public float GetStamina() { return Stamina; }
    public float GetAttack() { return Attack; }
    public float GetDefence() { return Defence; }
    public int GetExp() { return Exp; }

}

[Serializable]
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
