using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    // Animation Event
    public void AttackEffect()
    {
        GameManager.Instance.EffectManager.PlayAttackEffect();
    }
}
