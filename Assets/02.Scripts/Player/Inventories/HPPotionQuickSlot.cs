using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPPotionQuickSlot : PotionQuickSlotBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override string GetPotionSpritePath()
    {
        return "Images/Potion/HP_1";
    }
}
