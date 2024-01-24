using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopPotion : MonoBehaviour
{
    [HideInInspector] public PotionSO potionSO;
    public ShopPotionInfo shopPotionInfo;

    public Image potionImage;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
    }

    public void Init(PotionSO data)
    {
        potionSO = data;
        potionImage.sprite = data.sprite;

    }

    public void SetItemInfo()
    {
        _button.onClick.AddListener(() => { shopPotionInfo.ShowItemInfo(potionSO);});
    }

    //private void ShowItemInfo()
    //{
    //    Debug.Log("Item Name: " + potionSO.Name);
    //    Debug.Log("Item Description: " + potionSO.Description);
    //}
}
