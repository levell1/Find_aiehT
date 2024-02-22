using UnityEngine;

public enum ItemType
{
    NATUREITEM,
    DROPITEM
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]

public class ItemSO : ScriptableObject
{
    [field: SerializeField] public int ItemID { get; private set; }
    [field: SerializeField] public string ObjName { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int MaxStackAmount { get; private set; }

    [field: SerializeField] public ItemType type;


}
