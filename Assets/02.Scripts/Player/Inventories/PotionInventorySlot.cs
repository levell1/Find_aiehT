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
        PotionAmount.text = InitQuantity.ToString();
    }

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
            HPPotionQuickSlot.ShowPotionToQuickslot(PotionSO, InitQuantity);
            Player.PlayerUseHealthPotion.Potion(PotionSO, InitQuantity);
        }
        else if(PotionSO.Kind == Kind.SP)
        {
            SPPotionQuickSlot.ShowPotionToQuickslot(PotionSO, InitQuantity);
            Player.PlayerUseStaminaPotion.Potion(PotionSO, InitQuantity);
        }
    }

    // 포션 사용 후 UI 업데이트
    public void UpdateUsedHPPotionQuantity(int quantity)
    {
        if (PotionSO == HPPotionQuickSlot.PotionSO)
        {
            InitQuantity = quantity;
            UpdateUI();

            SetQuickSlot();
        }
      
    }

    public void UpdateUsedSPPotionQuantity(int quantity)
    {
        if (PotionSO == SPPotionQuickSlot.PotionSO)
        {
            InitQuantity = quantity;
            UpdateUI();

            SetQuickSlot();
        }

    }


}
