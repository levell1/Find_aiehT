using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image Sprite;
    public TextMeshProUGUI QuatityText;
    private ItemSlot _curSlot;

    public int index;

    public void Set(ItemSlot slot)
    {
        _curSlot = slot;
        Sprite.gameObject.SetActive(true);
        Sprite.sprite = slot.Item.Sprite;
        QuatityText.text = slot.Quantity > 0 ? slot.Quantity.ToString() : string.Empty;
    }

    public void Clear() 
    {
        _curSlot = null;
        Sprite.gameObject.SetActive(false);
        QuatityText.text = string.Empty;
    }

    public void OnButtonClick() //버튼 클릭시 하단에 아이템 정보 변화
    {
        GameManager.instance.Inventory.SelectItem(index);
    }
}
