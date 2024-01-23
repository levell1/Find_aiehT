using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Potion/Potion", order = 0)]
public class PotionSO : ScriptableObject
{
    public string Name;
    public string info;
    public int Heal;
    public Sprite sprite;
}
