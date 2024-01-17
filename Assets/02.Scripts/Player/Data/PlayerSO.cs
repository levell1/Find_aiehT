using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player", menuName ="Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
    [field: SerializeField] public PlayerSkillData SkillData { get; set; }
    [field: SerializeField] public PlayerData PlayerData { get; private set; }

    public void SetPlayerData(PlayerData newData)
    {
        PlayerData = newData;
    }

    public PlayerData GetPlayerData()
    {
        return PlayerData;
    }

}
