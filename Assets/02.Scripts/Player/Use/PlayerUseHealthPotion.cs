using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Heal);
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
            Debug.Log("포션이 다 떨어졌습니다. 장착해주세요");
        }

    }
    protected override void HandleCoolTimeFinish()
    {
        _isCoolTime = false;
    }


}
