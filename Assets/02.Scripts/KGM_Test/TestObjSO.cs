using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NatureItemData", menuName = "NatureItemData/Default", order = 0)]

public class TestObjSO : ScriptableObject
{
    [Header("Attack Info")]
    public string ItemName;
    public string ItemDescription;
    public Sprite Sprite;
    public int ItemPrice;

}
