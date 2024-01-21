using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemObject : MonoBehaviour
{
    public ItemSO ItemData;

    public void GetItem() // 상호작용 됬을때
    {
        //인벤토리 Add
        Destroy(gameObject);
    }

}
