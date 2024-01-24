using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPotionInfoPopup : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemInfo;
    [SerializeField] private TMP_Text _itemQuantity;
    [SerializeField] private TMP_Text _itemPrice;
    [SerializeField] private Slider _itemSlider;
    [SerializeField] private Button _decreaseButton;
    [SerializeField] private Button _increaseButton;

    private int _itemCurQuantity = 1;
    private int _itemCurGold;

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
    }

    public void ShowPopup(PotionSO data)
    {
        _itemImage.sprite = data.sprite;
        _itemName.text = data.Name;
        _itemInfo.text = data.Description;
        _itemPrice.text = data.Price.ToString();
        _itemQuantity.text = _itemCurQuantity.ToString();

        _itemCurGold = data.Price;

        _itemSlider.minValue = 1;
        _itemSlider.maxValue = data.Quantity;

        _itemSlider.value = _itemCurQuantity;

        _itemSlider.onValueChanged.AddListener(OnSliderValueChanged);

    }

    // 슬라이더 이벤트
    private void OnSliderValueChanged(float newValue)
    {
        _itemCurQuantity = Mathf.RoundToInt(newValue); // 소수점을 반올림하여 정수로 변환
        int totalItemGold = _itemCurQuantity * _itemCurGold;

        _itemQuantity.text = _itemCurQuantity.ToString();
        _itemPrice.text = totalItemGold.ToString();
    }

    private void DecreaseItemQuantity()
    {
        if( _itemCurQuantity > _itemSlider.minValue) 
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
        _itemQuantity.text = _itemCurQuantity.ToString();

        _itemPrice.text = (_itemCurQuantity * _itemCurGold).ToString();
        _itemSlider.value = _itemCurQuantity;
    }


    //public void ShowItemInfo(PotionSO data)
    //{
    //PotionSO selectedPotion = shopInteraction.GetSelectedPotion();

    //if (selectedPotion != null)
    //{
    //    _itemImage.sprite = selectedPotion.sprite;
    //    _itemName.text = selectedPotion.Price.ToString();
    //    _itemInfo.text = selectedPotion.Description;
    //}
    //else
    //{
    //    // 선택된 아이템이 없을 때 텍스트 초기화
    //    _itemImage.sprite = "";
    //    _itemName.text = "";
    //    _itemInfo.text = "";
    //}

    //    

    //}
}
