using UnityEngine;
[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Default", order = 0)]
public class EquipmentBase : ScriptableObject
{
    public int EquipmentID; 
    public string Name;
    public int UpgradeGold;
    public Sprite EquipSprite;
    public int EquipmentDmg;
    public int EquipmentHealth;
    public int EquipmentDef;
}
