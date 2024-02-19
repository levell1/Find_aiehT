using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotionInventory : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public PotionInventorySlot[] Potions;
    public HPPotionQuickSlot HPPotionQuick;
    public SPPotionQuickSlot SPPotionQuick;

    private int _hpDefaultPotionID;
    private int _spDefaultPotionID;

    private int[] _loadPotionQuantity;
    private int _potionKey = 2001;

    private void Start()
    {
        InitInventory();
    }

    public void InitInventory()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;
        _loadPotionQuantity = new int[Potions.Length];

        if (gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            InitInventoryForLoadGame();
        }
        else if (gameStateManager.CurrentGameState == GameState.NEWGAME)
        {
            InitInventoryForNewGame();
        }
    }

    private void InitInventoryForLoadGame()
    {
        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;

            _loadPotionQuantity[i] = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePotions[_potionKey + i];
            Potions[i].InitQuantity = _loadPotionQuantity[i];
            Potions[i].Init(PotionDataList.PotionList[i]);

            _hpDefaultPotionID = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions.Keys.ElementAt(0);
            _spDefaultPotionID = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions.Keys.ElementAt(1);

            //HPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_hpDefaultPotionID - _potionKey]);
            //SPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_spDefaultPotionID - _potionKey]);

            if (PotionDataList.PotionList[i].ID == _hpDefaultPotionID)
            {
                int HPQuickSlotQuantity = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions[PotionDataList.PotionList[i].ID];
                Potions[i].SetQuickSlot(PotionDataList.PotionList[i], HPQuickSlotQuantity);
                //HPPotionQuick.ShowPotionToQuickslot(, HPQuickSlotQuantity);
            }

            if (PotionDataList.PotionList[i].ID == _spDefaultPotionID)
            {
                int SPQuickSlotQuantity = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions[PotionDataList.PotionList[i].ID];
                Potions[i].SetQuickSlot(PotionDataList.PotionList[i], SPQuickSlotQuantity);
                //SPPotionQuick.ShowPotionToQuickslot(, );
            }

            
        }
    }

    private void InitInventoryForNewGame()
    {
        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
            Potions[i].Init(PotionDataList.PotionList[i]);
            Potions[i].TutorialPotion();

            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
        }

        _hpDefaultPotionID = 0;
        _spDefaultPotionID = 3;

        HPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_hpDefaultPotionID]);
        SPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_spDefaultPotionID]);
    }

}
