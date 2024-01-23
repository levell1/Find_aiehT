using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData
{
    private int _level;
    private EquipmentBase _equipment;
    private float _sumhealth;
    private float _sumDef;
    private float _sumAttack;

    public int Level { get { return _level; } set { _level = value ; } }
    public EquipmentBase Equipment { get { return _equipment; } set { _equipment = value; } }
    public float Sumhealth { get { return _sumhealth; } set { _sumhealth = value; } }
    public float SumAttack { get { return _sumDef; } set { _sumDef = value; } }
    public float SumDef { get { return _sumAttack; } set { _sumAttack = value; } }


}
