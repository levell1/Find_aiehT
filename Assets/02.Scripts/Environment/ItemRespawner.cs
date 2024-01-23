using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> ItemWaitSpawnList = new List<GameObject>();

    private void FixedUpdate()
    {
        if (ItemWaitSpawnList.Count > 0)
        {
            ReSpwanItem();
        }
    }

    void ReSpwanItem()
    {
        if (0.3f <= GameManager.instance.GlobalTimeManager.DayTime && GameManager.instance.GlobalTimeManager.DayTime < 0.31f)
        {
            for (int i = 0; i < ItemWaitSpawnList.Count; i++)
            {
                ItemWaitSpawnList[i].SetActive(true);
            }
            ItemWaitSpawnList.Clear();
        }
    }
}
