using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInventory : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public PotionInventorySlot[] Potions;

    private void Awake()
    {
        for (int i = 0; i < PotionDataList.ShopPotionList.Length; i++)
        {
            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
            Potions[i].Init(PotionDataList.ShopPotionList[i]);
        }
        
    }
}
