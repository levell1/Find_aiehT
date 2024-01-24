using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentDatas : MonoBehaviour
{
    public EquipmentData[] EquipData = new EquipmentData[6];
    private HealthSystem _healthSystem;
    private PlayerSO _playerData;

    public float SumHealth;
    public float SumDef;
    public float SumDmg;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            EquipData[i].Level = 0;
            SetEquipCurrent(i);
        }
        SumEquipStat();
        _healthSystem.SetMaxHealth();
    }
    public void EquipLevelUp(int i) 
    {
        EquipData[i].Level++;
        SetEquipCurrent(i);
        SumEquipStat();
        _healthSystem.SetMaxHealth();
        
    }
    public void SetEquipCurrent(int i) 
    { 
        EquipData[i].CurrentUpgradeGold = Mathf.Ceil(EquipData[i].Equipment.UpgradeGold + EquipData[i].Equipment.UpgradeGold* (Mathf.Pow(EquipData[i].Level, 2) / 2));
        EquipData[i].Currenthealth = Mathf.Ceil(EquipData[i].Equipment.EquipmentHealth+ EquipData[i].Equipment.EquipmentHealth * (Mathf.Pow(EquipData[i].Level, 2) / 2));
        EquipData[i].CurrentDef = Mathf.Ceil(EquipData[i].Equipment.EquipmentDef+ EquipData[i].Equipment.EquipmentDef * (Mathf.Pow( EquipData[i].Level, 2) / 2));
        EquipData[i].CurrentAttack = Mathf.Ceil(EquipData[i].Equipment.EquipmentDmg + EquipData[i].Equipment.EquipmentDmg * (Mathf.Pow(EquipData[i].Level, 2) / 2));
    }

    public void SumEquipStat() 
    {
        SumHealth = 0;
        SumDef = 0;
        SumDmg = 0;
        for (int i = 0; i < 6; i++)
        {
            SumHealth += EquipData[i].Currenthealth;
            SumDef += EquipData[i].CurrentDef;
            SumDmg += EquipData[i].CurrentAttack;
        }
        
    }
}
