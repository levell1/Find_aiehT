using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]

public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string ObjName { get; private set; }
    [field: SerializeField] public string Discription { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int MaxStackAmount { get; private set; }
}
