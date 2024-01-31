using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _BuyPanel;
    [SerializeField] private GameObject _SellPanel;
    [SerializeField] private GameObject _CheckPanel;
    [SerializeField] private Button _showBuyButton;
    [SerializeField] private Button _showSellButton;

    private void OnEnable()
    {
        _selectButton.SetActive(true);
        _BuyPanel.SetActive(false);
        _SellPanel.SetActive(false);
        _CheckPanel.SetActive(false);
    }
}
