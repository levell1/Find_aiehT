using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public List<GameObject> ItemWaitSpawnList = new List<GameObject>();

    

    private void FixedUpdate()
    {
        if(GameManager.instance.GlobalTimeManager.DayTime < 0.3f)
        {

        }
    }

}
