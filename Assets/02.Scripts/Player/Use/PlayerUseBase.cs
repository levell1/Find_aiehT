using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseBase : MonoBehaviour
{
    Player _player;
    private PotionSO _potionData;
    private int _healingAmount;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    public virtual void UsePotion()
    {
        if (_potionData != null)
        {
            _healingAmount = _potionData.HealingAmount;
            _player.HealthSystem.Healing(_healingAmount);
        }

    }

}
