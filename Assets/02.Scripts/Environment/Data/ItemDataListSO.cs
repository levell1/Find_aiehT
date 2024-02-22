using UnityEngine;

[CreateAssetMenu(fileName = "NatureItemList", menuName = "DataList/NatureItemList")]
public class ItemDataListSO : ScriptableObject
{
    public ItemSO[] ItemList;
}
