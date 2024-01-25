using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemSlot
{
    public ItemSO Item;
    public int Quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] UISlots;
    public List<ItemSlot> Slots = new List<ItemSlot>();

    public GameObject InventoryUI;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    private void Start()
    {
        InventoryUI.SetActive(false);

        for (int i = 0; i < Slots.Count; i++)
        {
            Slots[i] = new ItemSlot();
            UISlots[i].index = i;
            UISlots[i].Clear();
        }
        ClearSeletecItem();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
    }

    public void AddItem(ItemSO item)
    {
        ItemSlot slotToStackTo = GetItemStack(item); // 이미 아이템이 있는지 체크
        if (slotToStackTo != null)
        {
            slotToStackTo.Quantity++; // 있다면 수량 증가
            UpdateUI();
            return;
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.Item = item;
            emptySlot.Quantity = 1; // 처음 획득하면 들어오는 부분
            UpdateUI();
            return;
        }
    }

    ItemSlot GetItemStack(ItemSO item) //최대수량보다 적은 아이템 중복 체크
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Item == item && Slots[i].Quantity < item.MaxStackAmount)
            {
                return Slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot() // 빈슬롯 찾기
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if (Slots[i].Item == null)
            {
                return Slots[i];
            }
        }
        return null;
    }

    void UpdateUI()
    {
        PushSlots();

        for (int i = 0; i < Slots.Count; ++i)
        {
            if (Slots[i].Item != null) //슬롯에 아이템이 있으면 같은 인덱스의 UI슬롯에 넣는다.
            {
                UISlots[i].Set(Slots[i]);
            }
            else
            {
                UISlots[i].Clear();
            }
        }
    }

    private void PushSlots()
    {
        int lastSlot = Slots.Count - 1;

        for (int i = Slots.Count - 1; i >= 0; --i)
        {
            if (Slots[i].Item == null)
            {
                PullSlots(i, lastSlot);
                --lastSlot;
            }
        }
    }

    private void PullSlots(int emptySlot, int lastSlot)
    {
        for (int i = emptySlot; i < lastSlot; ++i)
        {
            UISlots[i].Set(Slots[i + 1]);
            Slots[i + 1].Item = null;
        }
        UISlots[lastSlot].Clear();
        Slots[lastSlot].Item = null;
    }

    public void SelectItem(int index) // 선택한 아이템 하단에 정보 표시
    {
        if (Slots[index].Item == null) return;

        selectedItem = Slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.Item.ObjName;
        selectedItemDescription.text = selectedItem.Item.Description;
    }

    private void ClearSeletecItem()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }

    public void RemoveItem(ItemSO item, int num)  // 아이템 판매하거나 타이쿤에 들어가는 재료
    {
        ItemSlot selectItem = GetItemStack(item);
        if (selectItem != null)
        {
            if (num > selectItem.Quantity)
            {
                //error
            }
            else if (num == selectItem.Quantity)
            {
                selectItem.Item = null;
                selectItem.Quantity = 0;
            }
            else
            {
                selectItem.Quantity -= num;
            }
        }

        UpdateUI();
    }

}
