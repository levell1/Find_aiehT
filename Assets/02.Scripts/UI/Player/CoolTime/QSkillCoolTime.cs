using UnityEngine;


public class QSkillCoolTime : CoolTimeBase
{
    protected void Awake()
    {
        base.Awake();
        _coolCount = _playerData.SkillData.SkillInfoDatas[0].GetSkillCoolTime();
        _keyCode = KeyCode.Q;
    }

}
