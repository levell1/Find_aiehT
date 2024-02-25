public class HPPotionQuickSlot : PotionQuickSlotBase
{
    private PlayerUseHealthPotion _playerHealthPotion;

    protected override void Start()
    {
        _playerHealthPotion = GameManager.Instance.Player.GetComponent<PlayerUseHealthPotion>();
        _playerHealthPotion.OnPotionUsed += QuickSlotUpdateUI;
        //_useHealthPotion.OnPotionUsed += QuickSlotUpdateUI;
        base.Start();
    }

    protected override string GetPotionSpritePath()
    {
        return "Images/Potion/HP_1";
    }

    protected override void QuickSlotUpdateUI(int quantity)
    {
        base.QuickSlotUpdateUI(quantity);
    }

}
