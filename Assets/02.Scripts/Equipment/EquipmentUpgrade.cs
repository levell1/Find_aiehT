using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUpgrade : MonoBehaviour
{
    [SerializeField] private EquipmentData[] EquipData = new EquipmentData[6];

    [SerializeField] private EquipmentBase[] equipSO = new EquipmentBase[6];
    Armor armor;
    Weapon weapon;

    private void Awake()
    {
        //EquipData[0].Equipment=
    }
    public void Upgrade(int i) 
    {
        EquipData[i].Level++;
        if (EquipData[i].Equipment.type == EquipmentType.Armor)
        {
            armor = EquipData[i].Equipment as Armor; 
            EquipData[i].Sumhealth += Mathf.Ceil(armor.ItemHealth * (Mathf.Pow(EquipData[i].Level, 2) / 2));
            EquipData[i].SumDef += Mathf.Ceil(armor.ItemDef * (Mathf.Pow( EquipData[i].Level, 2) / 2));
        }                           //ItemDef 2  ItemDef*2 ItemDef *  
        else if (EquipData[i].Equipment.type == EquipmentType.Weapon)
        {
            weapon = EquipData[i].Equipment as Weapon;
            EquipData[i].SumAttack += Mathf.Ceil(weapon.EquipmentAttack * (Mathf.Pow(EquipData[i].Level, 2) / 2));
        }
    }
}
