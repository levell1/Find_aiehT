using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseStaminaPotion : PlayerUseBase
{
    public override void UsePotion()
    {
        base.UsePotion();
        Debug.Log("SP포션 사용!");
    }
}
