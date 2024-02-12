using UnityEngine;
[CreateAssetMenu(fileName = "BossDataSO", menuName = "Characters/Boss", order = 0)]
public class BossScriptableObject : ScriptableObject
{
    [field: SerializeField] public int BossID { get; private set; }
    [field: SerializeField] public string BossName { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float SkillDamage { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public int DropEXP { get; private set; }
    [field: SerializeField] public int DropGold { get; private set; }
    [field: SerializeField] public GameObject[] DropItem { get; private set; }
}



