using UnityEngine;

public class ESkillCoolTime : CoolTimeBase
{
    protected void Awake()
    {
        base.Awake();
        _coolCount = _playerData.SkillData.SkillInfoDatas[1].GetSkillCoolTime();
        _keyCode = KeyCode.E;
    }
}
