using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
        ClearSeletecItem();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Inventory.Slots.Count; ++i)
        {
            if (Inventory.Slots[i].Item != null) //슬롯에 아이템이 있으면 같은 인덱스의 UI슬롯에 넣는다.
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

    public void SelectItem(int index) // 선택한 아이템 하단에 정보 표시
    {
        if (Inventory.Slots.Count <= index || Inventory.Slots[index].Item == null) return;

        selectedItem = Inventory.Slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.Item.ObjName;
        selectedItemDescription.text = selectedItem.Item.Description;
        selectedItemPrice.text = selectedItem.Item.Price.ToString();
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
