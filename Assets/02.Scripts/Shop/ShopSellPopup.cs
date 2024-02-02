using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSellPopup : MonoBehaviour
{
    public PlayerSO PlayerSO;
    public ShopSell ShopSell;

    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemInfo;
    [SerializeField] private TMP_Text _itemQuantity;
    [SerializeField] private TMP_Text _itemPrice;
    [SerializeField] private Slider _itemSlider;
    [SerializeField] private Button _decreaseButton;
    [SerializeField] private Button _increaseButton;
    [SerializeField] private Button _successButton;
    [SerializeField] private GameObject _successPopup;
    [SerializeField] private GameObject _successPopupObject;

    private int _itemCurQuantity = 1;
    private int _itemCurGold;
    private int _itemTotalPrice;

    private void OnEnable()
    {
        InitializePopup();
    }

    void InitializePopup()
    {
        _itemCurQuantity = 1;

        UpdateUI();

        _decreaseButton.onClick.RemoveAllListeners();
        _increaseButton.onClick.RemoveAllListeners();

        _decreaseButton.onClick.AddListener(DecreaseItemQuantity);
        _increaseButton.onClick.AddListener(IncreaseItemQuantity);

        _successButton.onClick.RemoveAllListeners();
        _successButton.onClick.AddListener(SellItem);
    }

    public void SetPopup()
    {
        _itemName.text = ShopSell.selectedItem.Item.ObjName;
        _itemInfo.text = ShopSell.selectedItem.Item.Description;
        _itemImage.sprite = ShopSell.selectedItem.Item.Sprite;
        _itemPrice.text = ShopSell.selectedItem.Item.Price.ToString();
        _itemQuantity.text = ShopSell.selectedItem.ToString();

        _itemCurGold = ShopSell.selectedItem.Item.Price;

        _itemSlider.minValue = 1;
        _itemSlider.maxValue = ShopSell.selectedItem.Quantity;

        _itemSlider.value = _itemCurQuantity;

        _itemSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float newValue)
    {
        _itemCurQuantity = Mathf.RoundToInt(newValue); // 소수점을 반올림하여 정수로 변환
        int totalItemGold = _itemCurQuantity * _itemCurGold;

        _itemQuantity.text = _itemCurQuantity.ToString();
        _itemPrice.text = totalItemGold.ToString();
    }

    private void DecreaseItemQuantity()
    {
        if (_itemCurQuantity > _itemSlider.minValue)
        {
            _itemCurQuantity--;

            UpdateUI();
        }
    }

    private void IncreaseItemQuantity()
    {
        if (_itemCurQuantity < _itemSlider.maxValue)
        {
            _itemCurQuantity++;

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _itemTotalPrice = _itemCurQuantity * _itemCurGold;

        _itemQuantity.text = _itemCurQuantity.ToString();

        _itemPrice.text = _itemTotalPrice.ToString();
        _itemSlider.value = _itemCurQuantity;
    }

    public void SellItem()
    {
        _itemTotalPrice = _itemCurQuantity * _itemCurGold;
        ShopSell.Inventory.RemoveItem(ShopSell.selectedItem.Item, _itemCurQuantity);

        int _gold = PlayerSO.PlayerData.GetPlayerGold();
        _gold += _itemTotalPrice;
        PlayerSO.PlayerData.SetPlayerGold(_gold);

        ShopSell.UpdateUI();
        ShopSell.ClearSeletecItem();
        StartCoroutine(SellPopupOff());
    }

    IEnumerator SellPopupOff()
    {
        _successPopup.SetActive(true);
        _successPopupObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        _successPopup.SetActive(false);
        gameObject.SetActive(false);
    }
}
