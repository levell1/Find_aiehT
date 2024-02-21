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

    public int InitQuantity;
    private Button _potionInvenButton;

    void OnEnable()
    {
        _potionInvenButton = GetComponent<Button>();
        
        _potionInvenButton.onClick.RemoveAllListeners();
        _potionInvenButton.onClick.AddListener(() => SetQuickSlot(PotionSO, InitQuantity));

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
        PotionAmount.text = InitQuantity.ToString();
    }

    //TODO 1일차에 포션 3개
    public void TutorialPotion()
    {
        if(PotionSO.ID == 2001)
        {
            InitQuantity = 1;

            UpdateUI();
        }
    }


    public void UpdatePotionQuantity(int quantity)
    {
        // 팝업에서 전달된 수량을 해당 슬롯에 반영
       
        if (PotionSO == ShopPotionInfoPopup.PotionData)
        {
            InitQuantity += quantity;

            if(ShopPotionInfoPopup.PotionData.ID == HPPotionQuickSlot.PotionSO.ID)
            {
                SetQuickSlot(PotionSO, InitQuantity);
            }

            if (ShopPotionInfoPopup.PotionData.ID == SPPotionQuickSlot.PotionSO.ID)
            {
                SetQuickSlot(PotionSO, InitQuantity);
            }

            UpdateUI();
        }
    }

    public void SetQuickSlot(PotionSO data, int quantity)
    {
        if(data.Kind == Kind.HP)
        {
            HPPotionQuickSlot.ShowPotionToQuickslot(data, quantity);
            Player.PlayerUseHealthPotion.Potion(data, quantity);
        }
        else if(data.Kind == Kind.SP)
        {
            SPPotionQuickSlot.ShowPotionToQuickslot(data, quantity);
            Player.PlayerUseStaminaPotion.Potion(data, quantity);
        }
    }

    // 포션 사용 후 UI 업데이트
    public void UpdateUsedHPPotionQuantity(int quantity)
    {
        if (PotionSO == HPPotionQuickSlot.PotionSO)
        {
            InitQuantity = quantity;
            UpdateUI();

            SetQuickSlot(PotionSO, InitQuantity);
        }
      
    }

    public void UpdateUsedSPPotionQuantity(int quantity)
    {
        if (PotionSO == SPPotionQuickSlot.PotionSO)
        {
            InitQuantity = quantity;
            UpdateUI();

            SetQuickSlot(PotionSO, InitQuantity);
        }

    }


}
