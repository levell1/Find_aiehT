using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopInteraction : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public ShopPotion[] shopPotions;

    // TODO 상점이랑 상호작용시 상점에서 아이템 리스트 나열
    public void SetShop()
    {
        for (int i = 0; i < PotionDataList.ShopPotionList.Length; i++)
        {
            shopPotions[i].Init(PotionDataList.ShopPotionList[i]);
            shopPotions[i].SetItemInfo();

        }
    }

}
