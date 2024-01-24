using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemObject : MonoBehaviour
{
    private ItemRespawner _itemRespawner;
    private Vector3 _itemSpawnPoint;
    private Rigidbody _itemRigidbody;

    public ItemSO ItemData;

    private void Awake()
    {
        _itemRigidbody = GetComponent<Rigidbody>();
        _itemRespawner = GetComponentInParent<ItemRespawner>();
        _itemSpawnPoint = transform.position;
    }

    private void OnEnable()
    {
        _itemRigidbody.velocity = Vector3.zero;
        transform.position = _itemSpawnPoint;
    }

    public void GetItem()
    {
        GameManager.instance.Inventory.AddItem(ItemData);

        if (ItemData.type == ItemType.DROPITEM)
        {
            Destroy(gameObject);
        }
        else if (ItemData.type == ItemType.NATUREITEM)
        {
            gameObject.SetActive(false);
            _itemRespawner.ItemWaitSpawnList.Add(gameObject);
        }
    }
}
