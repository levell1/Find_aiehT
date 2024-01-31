using System;
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
    [SerializeField] private Button _shopExitButton;
    [SerializeField] private Button _showBuyButton;
    [SerializeField] private Button _showSellButton;
    [SerializeField] private Button _buyExitButton;
    [SerializeField] private Button _sellExitButton;
    private void OnEnable()
    {
        _selectButton.SetActive(true);
        _BuyPanel.SetActive(false);
        _SellPanel.SetActive(false);
        _CheckPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        _showBuyButton.onClick.AddListener(ShowBuyPanel);
        _showSellButton.onClick.AddListener(ShowSellPanel);
        _buyExitButton.onClick.AddListener(ExitBuy);
        _sellExitButton.onClick.AddListener(ExitSell);
        _shopExitButton.onClick.AddListener(ExitShop);
    }
    private void ShowBuyPanel()
    {
        GameManager.instance.Inventory.ToggleInventoryUI();
        GameManager.instance.Inventory.ShopOpen();
        _selectButton.SetActive(false);
        _BuyPanel.SetActive(true);
    }

    private void ShowSellPanel()
    {
        _selectButton.SetActive(false);
        _SellPanel.SetActive(true);
    }

    private void ExitShop()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ExitSell()
    {
        _selectButton.SetActive(true);
        _SellPanel.SetActive(false);
    }

    private void ExitBuy()
    {
        GameManager.instance.Inventory.ToggleInventoryUI();
        Cursor.lockState = CursorLockMode.None;
        _selectButton.SetActive(true);
        _BuyPanel.SetActive(false);
    }




}
