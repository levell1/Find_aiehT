public class SPPotionQuickSlot : PotionQuickSlotBase
{
    private PlayerUseStaminaPotion _playerStaminaPotion;
    protected override void Start()
    {
        _playerStaminaPotion = GameManager.Instance.Player.GetComponent<PlayerUseStaminaPotion>();
        _playerStaminaPotion.OnPotionUsed += QuickSlotUpdateUI;
        //_useHealthPotion.OnPotionUsed += QuickSlotUpdateUI;
        base.Start();
    }

    protected override string GetPotionSpritePath()
    {
        return "Images/Potion/SP_1";
    }
    protected override void QuickSlotUpdateUI(int quantity)
    {
        base.QuickSlotUpdateUI(quantity);
    }
}
