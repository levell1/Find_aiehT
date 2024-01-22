using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();
    public int CoCount;
    public float RespawnTime;

    private void FixedUpdate()
    {
        if (CoCount == 1)
        {
            StartCoroutine(RespawnItem());
        }
    }

    private IEnumerator RespawnItem()
    {
        --CoCount;
        if (Items[0] != null)
        {
            yield return new WaitForSeconds(RespawnTime);
            Items[0].gameObject.SetActive(true);
            Items.Remove(Items[0]);
        }
    }
}
