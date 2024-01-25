using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionInventorySlot : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    public ShopPotionInfoPopup ShopPotionInfoPopup;

    public Image PotionImage;
    public TMP_Text PotionAmount;

    private int _initQuantity;

    public void Init(PotionSO data)
    {
        PotionSO = data;
        PotionImage.sprite = data.sprite;
        UpdateUI();
    }

    public void UpdateUI()
    {
        PotionAmount.text = _initQuantity.ToString();
    }

    public void UpdatePotionQuantity(int quantity)
    {

        // 팝업에서 전달된 수량을 해당 슬롯에 반영
       
        if (PotionSO == ShopPotionInfoPopup.PotionData)
        {
            _initQuantity += quantity;
            UpdateUI();
        }
    }

}
