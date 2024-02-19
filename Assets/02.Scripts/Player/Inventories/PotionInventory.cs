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
        _hpDefaultPotionID = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions.Keys.ElementAt(0);
        _spDefaultPotionID = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions.Keys.ElementAt(1);

        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
            _loadPotionQuantity[i] = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePotions[_potionKey + i];
            Potions[i].InitQuantity = _loadPotionQuantity[i];

            HPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_hpDefaultPotionID - _potionKey]);
            SPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_spDefaultPotionID - _potionKey]);

            if (PotionDataList.PotionList[i].ID == _hpDefaultPotionID)
            {
                int HPQuickSlotQuantity = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions[PotionDataList.PotionList[i].ID];
                HPPotionQuick.ShowPotionToQuickslot(PotionDataList.PotionList[i], HPQuickSlotQuantity);
            }

            if (PotionDataList.PotionList[i].ID == _spDefaultPotionID)
            {
                int SPQuickSlotQuantity = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveQuickSlotPotions[PotionDataList.PotionList[i].ID];
                SPPotionQuick.ShowPotionToQuickslot(PotionDataList.PotionList[i], SPQuickSlotQuantity);
            }

            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
            Potions[i].Init(PotionDataList.PotionList[i]);
        }
    }

    private void InitInventoryForNewGame()
    {
        _hpDefaultPotionID = Potions[0].PotionSO.ID;
        _spDefaultPotionID = Potions[3].PotionSO.ID;

        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
            Potions[i].TutorialPotion();
            HPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_hpDefaultPotionID]);
            SPPotionQuick.DefaultPotionInit(PotionDataList.PotionList[_spDefaultPotionID]);

            ShopPotionInfoPopup.OnPurchaseSuccessAction += Potions[i].UpdatePotionQuantity;
            Potions[i].Init(PotionDataList.PotionList[i]);
        }

    }

}
