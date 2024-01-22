using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemSO ItemData;
    private float _respawnTimer;
    public float RespawnTime;

    private void Awake()
    {
        _respawnTimer = RespawnTime;
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf == false)
        {
            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0)
            {
                ItemRespawn();
            }
        }
    }

    public void GetItem() // 상호작용 됬을때
    {
        //인벤토리 Add
        if (ItemData.type == ItemType.DropItem)
        {
            Destroy(gameObject);
        }
        else if (ItemData.type == ItemType.NatureItem)
        {
            gameObject.SetActive(false);
        }
    }

    private void ItemRespawn()
    {
        _respawnTimer = RespawnTime;
        gameObject.SetActive(true);
    }
}
