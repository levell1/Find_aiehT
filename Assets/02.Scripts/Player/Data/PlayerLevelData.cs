using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfoData
{
    [SerializeField] private int _level;
    [SerializeField] private float _health;
    [SerializeField] private float _stamina;
    [SerializeField] private float _attack;
    [SerializeField] private float _defence;
    [SerializeField] private int _exp;
    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }
    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public float Stamina
    {
        get { return _stamina; }
        set { _stamina = value; }
    }
    public float Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }
    public float Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }
    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    //public int GetLevel() { return Level; }
    //public float GetHealth() { return Health; }
    //public float GetStamina() { return Stamina; }
    //public float GetAttack() { return Attack; }
    //public float GetDefence() { return Defence; }
    //public int GetExp() { return Exp; }

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
            playerData.PlayerMaxHealth = nextLevelData.Health;
            playerData.PlayerMaxStamina = nextLevelData.Stamina;
            playerData.PlayerAttack = nextLevelData.Attack;
            playerData.PlayerDef = nextLevelData.Defence;
        }
    }
}
