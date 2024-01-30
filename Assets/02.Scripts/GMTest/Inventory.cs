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
    public List<ItemSlot> Slots = new List<ItemSlot>(); // 원래 배열이였던 부분

    public GameObject InventoryUI;

    [Header("Selected Item")]
    private int selectedItemIndex;
    public ItemSlot selectedItem;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    private void Start()
    {
        InventoryUI.SetActive(false);

        for (int i = 0; i < UISlots.Length; i++)
        {
           // Slots.Add(new ItemSlot());
            UISlots[i].index = i;
            UISlots[i].Clear();
        }
        ClearSeletecItem();
    }

    public void ToggleInventoryUI()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
        if (InventoryUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
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

        if (Slots.Count < UISlots.Length) //새로운 아이템이 들어오는 부분
        {
            ItemSlot newSlot = new ItemSlot();
            newSlot.Item = item;
            newSlot.Quantity = 1;
            Slots.Add(newSlot); //슬롯 생성
            UpdateUI();
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

    private void UpdateUI()
    {
        for (int i = 0; i < Slots.Count; ++i)
        {
            if (Slots[i].Item != null) //슬롯에 아이템이 있으면 같은 인덱스의 UI슬롯에 넣는다.
            {
                UISlots[i].Set(Slots[i]);
            }
        }

        if (Slots.Count < UISlots.Length)
        {
            for (int i = Slots.Count; i < UISlots.Length; ++i)
            {
                UISlots[i].Clear();
            }
        }
    }

    public void SelectItem(int index) // 선택한 아이템 하단에 정보 표시
    {
        if (Slots.Count <= index || Slots[index].Item == null) return;

        selectedItem = Slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.Item.ObjName;
        selectedItemDescription.text = selectedItem.Item.Description;
    }

    public void ClearSeletecItem()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }

    public void RemoveItem(ItemSO item, int num)  // 아이템 판매하거나 타이쿤에 들어가는 재료
    {
        ItemSlot selectItem = GetItemStack(item); //아이템이 있는지 체크
        if (selectItem != null)
        {
            if (num > selectItem.Quantity)
            {
                //error
            }
            else if (num == selectItem.Quantity)
            {
                Slots.Remove(selectItem); //slot 삭제
                ClearSeletecItem();
            }
            else
            {
                selectItem.Quantity -= num;
            }

            UpdateUI();
        }
    }

}
