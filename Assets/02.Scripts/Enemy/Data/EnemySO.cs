using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float Damage { get;  set; }
    [field: SerializeField] public int AttackDelay { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float MaxHealth { get;  set; }
    [field: SerializeField] public int DropEXP { get;  set; }
    [field: SerializeField] public GameObject[] DropItem { get; private set; }
}