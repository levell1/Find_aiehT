using UnityEngine;
[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Default", order = 0)]
public class EquipmentBase : ScriptableObject
{
    public string Name;
    public int UpgradeGold;
    public Sprite sprite;
    public int EquipmentDmg;
    public int EquipmentHealth;
    public int EquipmentDef;
}
