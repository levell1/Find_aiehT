using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondSkillState : PlayerSkillState
{

    SkillInfoData _skillData;

    public PlayerSecondSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        int _skillIndex = 1;
       
        _skillData = _stateMachine.Player.Data.SkillData.GetSkillData(_skillIndex);


        int _skillCost = _skillData.GetSkillCost();
        _stateMachine.IsSkillCoolTime = _stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime;

        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.Skill2ParameterHash);

        if (_stateMachine.Player.StaminaSystem.CanUseSkill(_skillCost) && !_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            if(normalizedTime < 1f)
            {
                Debug.Log("ON");
                _stateMachine.MovementSpeedModifier = 0; // 공격할 때 안움직임
                _stateMachine.Player.SandSkill.EnableCollider();
                _stateMachine.Player.SkillParticle.PlayParticle();

                int skillDamage = _skillData.GetSkillDamage();
                int playerDamage = _stateMachine.Player.Data.PlayerData.GetPlayerAtk();

                int totalDamage = skillDamage + playerDamage;

                Debug.Log(totalDamage);
                _stateMachine.Player.SandSkill.SetAttack(totalDamage);

                _stateMachine.Player.StaminaSystem.UseSkill(_skillCost);

                _stateMachine.Player.SecondSkillCoolTimeController.StartCoolTime(_skillData.GetSkillCoolTime());
            }
            else
            {
                _stateMachine.Player.SandSkill.DisableCollider();
            }

        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.Skill2ParameterHash);

        _stateMachine.Player.SandSkill.DisableCollider();
    }

    public override void Update()
    {
        base.Update();

        if (!_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            Debug.Log("OFF");
            _stateMachine.Player.SandSkill.DisableCollider();
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        if (normalizedTime >= 1f)
        {
            _stateMachine.Player.SandSkill.DisableCollider();
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }


}
