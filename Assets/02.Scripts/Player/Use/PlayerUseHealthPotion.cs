using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseHealthPotion : PlayerUseBase
{
    public override void UsePotion()
    {
        base.UsePotion();
        Debug.Log("HP포션 사용!");
    }
}
