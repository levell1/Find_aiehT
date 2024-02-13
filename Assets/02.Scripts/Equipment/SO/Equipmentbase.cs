using UnityEngine;
[CreateAssetMenu(fileName = "Equipment", menuName = "Equipment/Default", order = 0)]
public class EquipmentBase : ScriptableObject
{
    [field: SerializeField] public int EquipmentID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int UpgradeGold { get; private set; }
    [field: SerializeField] public Sprite EquipSprite { get; private set; }
    [field: SerializeField] public int EquipmentDmg { get; private set; }
    [field: SerializeField] public int EquipmentHealth { get; private set; }
    [field: SerializeField] public int EquipmentDef { get; private set; }
}
