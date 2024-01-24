using System;
using UnityEngine;


[Serializable]
public class EquipmentData
{
    private int _level = 0;
    [field: SerializeField] private EquipmentBase _equipment;
    private float _currenthealth;
    private float _currentDef;
    private float _currentAttack;
    private float _currentUpgradeGold;

    public int Level { get { return _level; } set { _level = value ; } }
    public EquipmentBase Equipment { get { return _equipment; } set { _equipment = value; } }
    public float Currenthealth { get { return _currenthealth; } set { _currenthealth = value; } }
    public float CurrentAttack { get { return _currentDef; } set { _currentDef = value; } }
    public float CurrentDef { get { return _currentAttack; } set { _currentAttack = value; } }

    public float CurrentUpgradeGold { get { return _currentUpgradeGold; } set { _currentUpgradeGold = value; } }
}
