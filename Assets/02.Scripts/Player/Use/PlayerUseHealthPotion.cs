
public class PlayerUseHealthPotion : PlayerUseBase
{
    protected override void Start()
    {
        CoolTime = 7f;
        base.Start();
        _coolTimeManager.AddCoolTimeEvent(CoolTimeObjName.HealthPotion, HandleCoolTimeFinish);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void UsePotion()
    {
        if (_player.HealthSystem.Health >= _player.HealthSystem.MaxHealth)
        {
            return;
        }
        base.UsePotion();
    }
    protected override void StartCoolTime()
    {
        base.StartCoolTime();
        _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.HealthPotion, CoolTime, _coolTimeImage);
    }
    protected override void Healing()
    {
        base.Healing();

        if (_quantity > 0)
        {
            _player.HealthSystem.Healing(_healingAmount);
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
