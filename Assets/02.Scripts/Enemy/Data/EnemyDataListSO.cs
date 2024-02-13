using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "DataList/EnemyList")]
public class EnemyDataListSO : ScriptableObject
{
    public EnemySO[] EnemyList;
}
