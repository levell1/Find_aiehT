
public class PlayerUseStaminaPotion : PlayerUseBase
{
    protected override void Start()
    {
        CoolTime = 10f;
        base.Start();
        _coolTimeManager.AddCoolTimeEvent(CoolTimeObjName.StaminaPotion, HandleCoolTimeFinish);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void UsePotion()
    {
        if (_player.StaminaSystem.Stamina >= _player.StaminaSystem.MaxStamina)
        {
            return;
        }
        base.UsePotion();
    }
    protected override void StartCoolTime()
    {
        base.StartCoolTime();
        _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.StaminaPotion, CoolTime, _coolTimeImage);
    }

    protected override void Healing()
    {
        base.Healing();

        if (_quantity > 0)
        {
            _player.StaminaSystem.Healing(_healingAmount);
        }
        else
        {
            //TODO 포션 장착
        }

    }
    protected override void HandleCoolTimeFinish()
    {
        _isCoolTime = false;
    }


}
