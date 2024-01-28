using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPotionInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemValue;
    [SerializeField] private TMP_Text _itemInfo;

    public void ShowItemInfo(PotionSO data)
    {
        _itemName.text = data.Name;
        _itemValue.text = data.Price.ToString();
        _itemInfo.text = data.Description;

    }
}
