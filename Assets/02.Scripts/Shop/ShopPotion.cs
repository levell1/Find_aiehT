using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPotion : MonoBehaviour
{
    [HideInInspector] public PotionSO potionSO;
    public ShopPotionInfo shopPotionInfo;
    public ShopPotionInfoPopup shopPotionInfoPopup;

    public Image potionImage;
    public Button buyButton;

    private Button _slotButton;

    void Start()
    {
        _slotButton = GetComponent<Button>();
    }

    public void Init(PotionSO data)
    {
        potionSO = data;
        potionImage.sprite = data.sprite;

    }

    public void SetItemInfo()
    {
        _slotButton.onClick.RemoveAllListeners();

        _slotButton.onClick.AddListener(() => 
        { 
            shopPotionInfo.ShowItemInfo(potionSO);
            shopPotionInfoPopup.ShowPopup(potionSO);
        });
    }

    //private void ShowItemInfo()
    //{
    //    Debug.Log("Item Name: " + potionSO.Name);
    //    Debug.Log("Item Description: " + potionSO.Description);
    //}
}
