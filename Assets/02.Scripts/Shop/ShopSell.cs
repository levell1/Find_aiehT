using TMPro;
using UnityEngine;

public class ShopSell : MonoBehaviour
{
    public Inventory Inventory { get; set;}
    
    public GameObject shop;
    public ItemSlotUI[] UISlots;
    public ShopSellPopup popup;

    [Header("Selected Item")]
    public int selectedItemIndex;
    public ItemSlot selectedItem;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemPrice;

    private void Awake()
    {
        Inventory = GameManager.Instance.Player.GetComponent<Inventory>();
        for (int i = 0; i < UISlots.Length; i++)
        {
            UISlots[i].Index = i;
            UISlots[i].Clear();
        }
    }

    private void OnEnable()
    {
        popup.gameObject.SetActive(false);
        ClearSeletecItem();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Inventory.Slots.Count; ++i)
        {
            if (Inventory.Slots[i].Item != null) 
            {
                UISlots[i].Set(Inventory.Slots[i]);
            }
        }

        if (Inventory.Slots.Count < UISlots.Length)
        {
            for (int i = Inventory.Slots.Count; i < UISlots.Length; ++i)
            {
                UISlots[i].Clear();
            }
        }
    }

    public void SelectItem(int index) 
    {
        if (Inventory.Slots.Count <= index || Inventory.Slots[index].Item == null) return;

        selectedItem = Inventory.Slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.Item.ObjName;
        selectedItemDescription.text = selectedItem.Item.Description;
        selectedItemPrice.text = "가격 : " + selectedItem.Item.Price.ToString();
    }

    public void ClearSeletecItem()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemPrice.text = string.Empty;
    }

    public void SellButtonClick()
    {
        if (selectedItem != null)
        {
            popup.SetPopup();
            popup.gameObject.SetActive(true);
        }
    }
}
