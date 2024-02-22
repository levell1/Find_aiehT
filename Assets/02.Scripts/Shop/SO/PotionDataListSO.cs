using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion/PotionList", order = 1)]

public class PotionDataListSO : ScriptableObject
{
    public PotionSO[] PotionList;
}
