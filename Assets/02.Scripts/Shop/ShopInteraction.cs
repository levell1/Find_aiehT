using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopInteraction : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public ShopPotion[] ShopPotions;
    [SerializeField] private GameObject _shopSellPopUp;
    private void OnEnable()
    {
        SetShop();
        _shopSellPopUp.SetActive(false);
    }
    // TODO 상점이랑 상호작용시 상점에서 아이템 리스트 나열
    public void SetShop()
    {
        for (int i = 0; i < PotionDataList.ShopPotionList.Length; i++)
        {
            ShopPotions[i].Init(PotionDataList.ShopPotionList[i]);
            ShopPotions[i].SetItemInfo();

        }
    }

}
