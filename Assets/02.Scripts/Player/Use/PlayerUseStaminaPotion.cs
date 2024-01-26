using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseStaminaPotion : PlayerUseBase
{
    public override void UsePotion()
    {
        if (_player.StaminaSystem.Stamina >= _player.StaminaSystem.MaxStamina)
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
            _player.StaminaSystem.Healing(_healingAmount);
        }
        else
        {
            Debug.Log("포션이 다 떨어졌습니다. 장착해주세요");
        }

    }

}
