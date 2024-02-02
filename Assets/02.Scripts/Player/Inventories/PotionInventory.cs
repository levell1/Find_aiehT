using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInventory : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public PotionInventorySlot[] Potions;
    public HPPotionQuickSlot HPPotionQuick;
    public SPPotionQuickSlot SPPotionQuick;

    private int _hpDefaultPotionID = 0;
    private int _spDefaultPotionID = 3;

    private void Awake()
    {
        for (int i = 0; i < PotionDataList.ShopPotionList.Length; i++)
        {
            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
            Potions[i].Init(PotionDataList.ShopPotionList[i]);
            HPPotionQuick.DefaultPotionInit(PotionDataList.ShopPotionList[_hpDefaultPotionID]);
            SPPotionQuick.DefaultPotionInit(PotionDataList.ShopPotionList[_spDefaultPotionID]);
        }
        
    }
}
