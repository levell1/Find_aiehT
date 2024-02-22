using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPotionInfoPopup : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionData;

    [SerializeField] private PlayerSO _playerData; 
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
    public static event Action<int> OnPurchaseSuccessAction;

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
        _successButton.onClick.AddListener(TryPurchasePotion);
    }

    public void ShowPopup(PotionSO data)
    {
        PotionData = data;

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


    private void OnSliderValueChanged(float newValue)
    {
        _itemCurQuantity = Mathf.RoundToInt(newValue); 
        int totalItemGold = _itemCurQuantity * _itemCurGold;

        _itemQuantity.text = _itemCurQuantity.ToString();
        _itemPrice.text = totalItemGold.ToString();

        if (totalItemGold > _playerData.PlayerData.PlayerGold)
        {
            _itemPrice.color = Color.red;
        }
        else
        {
            _itemPrice.color = Color.black;
        }
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
        _itemTotalPrice = _itemCurQuantity * _itemCurGold;

        _itemQuantity.text = _itemCurQuantity.ToString();

        _itemPrice.text = _itemTotalPrice.ToString();
        _itemSlider.value = _itemCurQuantity;

        if (_itemTotalPrice > _playerData.PlayerData.PlayerGold)
        {
            _itemPrice.color = Color.red;
        }
        else
        {
            _itemPrice.color = Color.black;
        }

    }

    private void TryPurchasePotion()
    {
        _itemTotalPrice = _itemCurQuantity * _itemCurGold;

        if (_playerData.PlayerData.PlayerGold >= _itemTotalPrice)
        {
            OnPurchaseSuccessAction?.Invoke(_itemCurQuantity);
            _playerData.PlayerData.PlayerGold = _playerData.PlayerData.PlayerGold - _itemTotalPrice;

            
            StartCoroutine(PurchasePopupOff());
            
        }
        else
        {
            StartCoroutine(PurchaseFaillPopupOff());
        }
    }

    IEnumerator PurchasePopupOff() 
    {
        _successPopup.SetActive(true);
        _successPopupObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        _successPopup.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator PurchaseFaillPopupOff()
    {
        _successPopup.SetActive(true);
        _successPopupObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        _successPopup.SetActive(false);
    }

}
