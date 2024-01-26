using UnityEngine;
using UnityEngine.UI;

public enum Kind
{
    HP,
    SP
}

[CreateAssetMenu(fileName = "Potion", menuName = "Potion/Potion", order = 0)]
public class PotionSO : ScriptableObject
{
    public int ID;
    public Kind Kind;
    public string Name;
    public string Description;
    public int HealingAmount;
    public Sprite sprite;
    public int Price;
    public int Quantity = 99;
}
