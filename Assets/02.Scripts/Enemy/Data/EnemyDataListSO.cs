using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "DataList/EnemyList")]
public class EnemyDataListSO : ScriptableObject
{
    public EnemySO[] EnemyList;
}
