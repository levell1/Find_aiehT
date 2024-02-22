using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public void AttackEffect()
    {
        GameManager.Instance.EffectManager.PlayAttackEffect();
    }
}
