
public class SecondSkillCoolTimeController : SkillCoolTimeController
{
    public override void Start()
    {
        base.Start();
        _coolTimeManager.AddCoolTimeEvent(CoolTimeObjName.SpreadSkill, HandleCoolTimeFinish);
    }

    public override void StartCoolTime(float coolTime)
    {
        base.StartCoolTime(coolTime);
        _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.SpreadSkill, _coolTime, _coolTimeImage);
    }

    protected override void HandleCoolTimeFinish()
    {
        base.HandleCoolTimeFinish();
    }
}
