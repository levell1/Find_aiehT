using TMPro;
using UnityEngine;

public class ShopPotionInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemValue;
    [SerializeField] private TMP_Text _itemInfo;

    private void OnEnable()
    {
        _itemName.text = string.Format("포션 상점");
        _itemValue.text = string.Format(" ");
        _itemInfo.text = string.Format(" 포션을 선택해주세요 "); 
    }
    public void ShowItemInfo(PotionSO data)
    {
        _itemName.text = data.Name;
        _itemValue.text = data.Price.ToString();
        _itemInfo.text = data.Description;

    }
}
