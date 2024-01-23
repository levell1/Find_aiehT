using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUpgrade : MonoBehaviour
{
    public EquipmentData[] EquipData = new EquipmentData[6];
    Armor armor;
    Weapon weapon;

    public void Upgrade(int i) 
    {
        EquipData[i].Level++;
        if (EquipData[i].Equipment.type == EquipmentType.Armor)
        {
            armor = EquipData[i].Equipment as Armor;
            EquipData[i].Sumhealth += Mathf.Ceil(armor.ItemHealth * Mathf.Pow(armor.ItemHealth * EquipData[i].Level, 2) / 2);
            EquipData[i].SumDef += Mathf.Ceil(armor.ItemDef * Mathf.Pow(armor.ItemDef * EquipData[i].Level, 2) / 2);
        }
        else if (EquipData[i].Equipment.type == EquipmentType.Weapon)
        {
            weapon = EquipData[i].Equipment as Weapon;
            EquipData[i].SumAttack += Mathf.Ceil(weapon.EquipmentAttack * Mathf.Pow(weapon.EquipmentAttack * EquipData[i].Level, 2) / 2);
        }
    }
}
