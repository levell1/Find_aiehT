using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Panel;
    public GameObject InvenMain;
    [SerializeField] private Button _exitBtn;
    [SerializeField] private Button _recipeBtn;
    [SerializeField] private GameObject _recipeUI;

    [Header("Selected Item")]
    private int _selectedItemIndex;
    public ItemSlot selectedItem;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    private void Start()
    {
        GameStateManager gameStateManager = GameManager.Instance.GameStateManager;
        _exitBtn.onClick.AddListener(InActiveUI);
        _recipeBtn.onClick.AddListener(ToggleRecipeUI);
        InventoryUI.SetActive(false);

        for (int i = 0; i < UISlots.Length; i++)
        {
            UISlots[i].Index = i;
            UISlots[i].Clear();
        }

        if (gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            LoadItem();
        }

        ClearSeletecItem();
    }

    private void InActiveUI()
    {
        GameManager.Instance.UIManager.CloseLastCanvas();
        _recipeUI.SetActive(false);

    }

    private void ToggleRecipeUI()
    {
        _recipeUI.SetActive(!_recipeUI.activeSelf);
    }

    public void ShopOpen() 
    {
        Panel.SetActive(false);
        InvenMain.SetActive(false);
    }

    public void ToggleInventoryUI()
    {
        if (!InventoryUI.activeSelf)
        {
            GameManager.Instance.UIManager.ShowCanvas(UIName.InventoryUI);
            Panel.SetActive(true);
            InvenMain.SetActive(true);
        }
    }

    public void AddItem(ItemSO item)
    {
        ItemSlot slotToStackTo = GetItemStack(item); 
        if (slotToStackTo != null)
        {
            slotToStackTo.Quantity++; 
            UpdateUI();
            return;
        }

        if (Slots.Count < UISlots.Length) 
        {
            ItemSlot newSlot = new ItemSlot();
            newSlot.Item = item;
            newSlot.Quantity = 1;
            Slots.Add(newSlot); 
            UpdateUI();
        }
    }

    private void AddLoadedItem(ItemSO item, int quantity)
    {
        ItemSlot slotToStackTo = GetItemStack(item); 
        if (slotToStackTo != null)
        {
            slotToStackTo.Quantity++;
            UpdateUI();
            return;
        }

        if (Slots.Count < UISlots.Length) 
        {
            ItemSlot newSlot = new ItemSlot();
            newSlot.Item = item;
            newSlot.Quantity = quantity;
            Slots.Add(newSlot); 
            UpdateUI();
        }
    }

    private void LoadItem()
    {
        ItemSO[] itemDataListSO = GameManager.Instance.DataManager.ItemDataList.ItemList;

        Dictionary<int, ItemSO> itemDataListDic = new Dictionary<int, ItemSO>();
        Dictionary<int, int> LoadItemList = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SaveInventoryItems;

        foreach (ItemSO item in itemDataListSO)
        {
            itemDataListDic.Add(item.ItemID, item);
        }

        foreach (int key in LoadItemList.Keys)
        {
            if (itemDataListDic.ContainsKey(key))
            {
                ItemSO item = itemDataListDic[key];
                int itemCount = LoadItemList[key];
                AddLoadedItem(item, itemCount);
            }
        }

    }

    private ItemSlot GetItemStack(ItemSO item) 
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
            if (Slots[i].Item != null) 
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

    public void SelectItem(int index) 
    {
        if (Slots.Count <= index || Slots[index].Item == null) return;

        selectedItem = Slots[index];
        _selectedItemIndex = index;

        selectedItemName.text = selectedItem.Item.ObjName;
        selectedItemDescription.text = selectedItem.Item.Description;
    }

    public void ClearSeletecItem()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }

    public void RemoveItem(ItemSO item, int num) 
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
                Slots.Remove(selectItem); 
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
