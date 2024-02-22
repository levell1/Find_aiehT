using UnityEngine;


public class ShopInteraction : MonoBehaviour
{
    public PotionDataListSO PotionDataList;
    public ShopPotion[] ShopPotions;
    [SerializeField] private GameObject _shopSellPopUp;
    private void OnEnable()
    {
        SetShop();
        _shopSellPopUp.SetActive(false);
    }
    public void SetShop()
    {
        for (int i = 0; i < PotionDataList.PotionList.Length; i++)
        {
            ShopPotions[i].Init(PotionDataList.PotionList[i]);
            ShopPotions[i].SetItemInfo();
        }
    }

}
