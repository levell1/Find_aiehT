using UnityEngine;

public enum EquipmentType
{
    None,
    Weapon,
    Armor
}

[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Default", order = 0)]
public class EquipmentBase : ScriptableObject
{
    public EquipmentType type = 0;
    public int Level;
    public string Name;
    public int UpgradeGold;
    public Sprite sprite;
}
