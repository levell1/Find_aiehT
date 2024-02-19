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

    private int[] _loadPotionQuantity;
    private int PotionKey = 2001;
    private void Awake()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;
        _loadPotionQuantity = new int[Potions.Length];


        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
           
            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
            Potions[i].Init(PotionDataList.PotionList[i]);

            if (gameStateManager.CurrentGameState == GameState.LOADGAME)
            {
                Potions[i].InitQuantity = _loadPotionQuantity[i];
            }

            Potions[i].TutorialPotion();
            HPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_hpDefaultPotionID]);
            SPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_spDefaultPotionID]);
        }

    }
}
