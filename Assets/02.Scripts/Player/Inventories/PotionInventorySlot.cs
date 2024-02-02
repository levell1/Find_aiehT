using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionInventorySlot : MonoBehaviour
{
    [HideInInspector] public PotionSO PotionSO;

    public Player Player;
    public ShopPotionInfoPopup ShopPotionInfoPopup;
    public HPPotionQuickSlot HPPotionQuickSlot;
    public SPPotionQuickSlot SPPotionQuickSlot;

    public Image PotionImage;
    public TMP_Text PotionAmount;

    private int _initQuantity;
    private Button _potionInvenButton;

    void OnEnable()
    {

        _potionInvenButton = GetComponent<Button>();
        
        _potionInvenButton.onClick.RemoveAllListeners();
        _potionInvenButton.onClick.AddListener(SetQuickSlot);

        Player.PlayerUseHealthPotion.OnPotionUsed += UpdateUsedHPPotionQuantity;
        Player.PlayerUseStaminaPotion.OnPotionUsed += UpdateUsedSPPotionQuantity;
    }

    public void Init(PotionSO data)
    {
        PotionSO = data;
        PotionImage.sprite = data.sprite;
        UpdateUI();
    }

    public void UpdateUI()
    {
        PotionAmount.text = _initQuantity.ToString();
    }

    public void UpdatePotionQuantity(int quantity)
    {

        // 팝업에서 전달된 수량을 해당 슬롯에 반영
       
        if (PotionSO == ShopPotionInfoPopup.PotionData)
        {
            _initQuantity += quantity;

            if(ShopPotionInfoPopup.PotionData.ID == HPPotionQuickSlot.PotionSO.ID)
            {
                SetQuickSlot();
            }

            if (ShopPotionInfoPopup.PotionData.ID == SPPotionQuickSlot.PotionSO.ID)
            {
                SetQuickSlot();
            }

            UpdateUI();
        }
    }

    public void SetQuickSlot()
    {
        if(PotionSO.Kind == Kind.HP)
        {
            HPPotionQuickSlot.ShowPotionToQuickslot(PotionSO, _initQuantity);
            Player.PlayerUseHealthPotion.Potion(PotionSO, _initQuantity);
        }
        else if(PotionSO.Kind == Kind.SP)
        {
            SPPotionQuickSlot.ShowPotionToQuickslot(PotionSO, _initQuantity);
            Player.PlayerUseStaminaPotion.Potion(PotionSO, _initQuantity);
        }
    }

    // 포션 사용 후 UI 업데이트
    public void UpdateUsedHPPotionQuantity(int quantity)
    {
        if (PotionSO == HPPotionQuickSlot.PotionSO)
        {
            _initQuantity = quantity;
            UpdateUI();

            SetQuickSlot();
        }
      
    }

    public void UpdateUsedSPPotionQuantity(int quantity)
    {
        if (PotionSO == SPPotionQuickSlot.PotionSO)
        {
            _initQuantity = quantity;
            UpdateUI();

            SetQuickSlot();
        }

    }


}
