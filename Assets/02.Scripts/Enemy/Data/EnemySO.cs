using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int[] AppropriateLevel { get; private set; }
    [field: SerializeField] public float ActiveNameRange { get; private set; }
    [field: SerializeField] public string EnemyName { get; private set; }
    [field: SerializeField] public string EnemyDescription { get; private set; }
    [field: SerializeField] public int EnemyID { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public int AttackDelay { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public int DropEXP { get; private set; }
    [field: SerializeField] public GameObject[] DropItem { get; private set; }
    [field: SerializeField, Range(0.0f, 1.0f)] public float DropPercent { get; private set; }
}