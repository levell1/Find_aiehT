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

    [SerializeField] private int _basicInitQuantity = 3;

    void Start()
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

    public void TutorialPotion()
    {
        if(PotionSO.ID == 2001)
            {
            InitQuantity = _basicInitQuantity + 1;
            UpdateUI();
        }

        if (PotionSO.ID == 2004)
        {
            InitQuantity = _basicInitQuantity;
            UpdateUI();
        }
    }


    public void UpdatePotionQuantity(int quantity)
    {
       
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
