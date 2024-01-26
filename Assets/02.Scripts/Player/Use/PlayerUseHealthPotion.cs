using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseHealthPotion : PlayerUseBase
{
    public override void UsePotion()
    {
        if (_player.HealthSystem.Health >= _player.HealthSystem.MaxHealth)
        {
            return;
        }

        base.UsePotion();
    }
    protected override void Healing()
    {
        base.Healing();

        if (_quantity > 0)
        {
            Debug.Log(_player.HealthSystem.Health);
            _player.HealthSystem.Healing(_healingAmount);

        }
        else
        {
            Debug.Log("포션이 다 떨어졌습니다. 장착해주세요");
        }

    }
}
