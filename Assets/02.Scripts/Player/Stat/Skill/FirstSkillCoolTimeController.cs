public class FirstSkillCoolTimeController : SkillCoolTimeController
{
    public override void Start()
    {
        base.Start();
        _coolTimeManager.AddCoolTimeEvent(CoolTimeObjName.ThrowSkill, HandleCoolTimeFinish);
    }

    public override void StartCoolTime(float coolTime)
    {
        base.StartCoolTime(coolTime);
        _coolTimeManager.StartCoolTimeCoroutine(CoolTimeObjName.ThrowSkill, _coolTime, _coolTimeImage);
    }

    protected override void HandleCoolTimeFinish()
    {
        base.HandleCoolTimeFinish();
    }

}
