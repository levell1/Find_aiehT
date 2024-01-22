using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    private ItemRespawner _itemRespawner;
    public ItemSO ItemData;

    private void Awake()
    {
        _itemRespawner = GetComponentInParent<ItemRespawner>();
    }

    public void GetItem() // 상호작용 됬을때
    {
        //인벤토리 Add
        if (ItemData.type == ItemType.DROPITEM)
        {
            Destroy(gameObject);
        }
        else if (ItemData.type == ItemType.NATUREITEM)
        {
            gameObject.SetActive(false);
            ++_itemRespawner.CoCount;
            _itemRespawner.Items.Add(gameObject);
        }
    }
}
