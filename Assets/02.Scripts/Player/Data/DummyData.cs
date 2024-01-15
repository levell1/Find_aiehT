using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DummyData
{
    [SerializeField]public string PlayerName { get; set; }
    [SerializeField] public int PlayerLevel { get; set; }
    [SerializeField] public int PlayerMaxHealth { get; set; }
    [SerializeField] public int PlayerMaxStamina { get; set; }
    [SerializeField] public int PlayerAttack { get; set; }
    [SerializeField] public int PlayerDef { get; set; }
    [SerializeField] public int PlayerExp { get; set; }
    [SerializeField] public int PlayerGold { get; set; }

}

public class Root
{
    public DummyData PlayerData { get; set; }
}

