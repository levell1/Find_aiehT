using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInventory : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public PotionInventorySlot[] Potions;
    public ShopPotionInfoPopup ShopPotionInfoPopup;

    private void OnEnable()
    {
        for (int i = 0; i < PotionDataList.ShopPotionList.Length; i++)
        {
            Potions[i].Init(PotionDataList.ShopPotionList[i]);

            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
        }

        
    }


}
