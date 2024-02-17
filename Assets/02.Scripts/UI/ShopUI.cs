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
    [SerializeField] private Scrollbar _sellScrollbar;
    private void OnEnable()
    {
        _selectButton.SetActive(true);
        _BuyPanel.SetActive(false);
        _SellPanel.SetActive(false);
        _CheckPanel.SetActive(false);
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
        GameManager.Instance.UIManager.ShowCanvas(UIName.InventoryUI);
        GameManager.Instance.Inventory.ShopOpen();
        _selectButton.SetActive(false);
        _BuyPanel.SetActive(true);
    }

    private void ShowSellPanel()
    {
        _selectButton.SetActive(false);
        _SellPanel.SetActive(true);
        _sellScrollbar.value = 1;
    }

    private void ExitShop()
    {
        GameManager.Instance.UIManager.CloseLastCanvas();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void ExitSell()
    {
        _selectButton.SetActive(true);
        _SellPanel.SetActive(false);
    }

    private void ExitBuy()
    {
        GameManager.Instance.UIManager.CloseLastCanvas();
        _selectButton.SetActive(true);
        _BuyPanel.SetActive(false);
    }
}
