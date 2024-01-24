using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion/Potion", order = 0)]
public class PotionSO : ScriptableObject
{
    public string Name;
    public string Description;
    public int HealingAmount;
    public Sprite sprite;
    public int Price;
    [HideInInspector] public int Quantity = 99;
}
