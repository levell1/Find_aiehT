using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondSkillState : PlayerGroundState
{

    SkillInfoData _skillData;

    public PlayerSecondSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        int _skillIndex = 1;
        _stateMachine.MovementSpeedModifier = 0; // 공격할 때 안움직임
        _skillData = _stateMachine.Player.Data.SkillData.GetSkillData(_skillIndex);


        int _skillCost = _skillData.GetSkillCost();
        _stateMachine.IsSkillCoolTime = _stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime;

        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.Skill2ParameterHash);

        if (_stateMachine.Player.StaminaSystem.CanUseSkill(_skillCost) && !_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            _stateMachine.Player.SkillInstantiator.InstantiateTomato();

            int skillDamage = _skillData.GetSkillDamage();
            int playerDamage = _stateMachine.Player.Data.PlayerData.GetPlayerAtk();

            int totalDamage = skillDamage + playerDamage;

            _stateMachine.Player.StaminaSystem.UseSkill(_skillCost);

            Debug.Log("totalDamage: " + totalDamage);
            Debug.Log("사용");

            _stateMachine.Player.SecondSkillCoolTimeController.StartCoolTime(_skillData.GetSkillCoolTime());

        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        //StopAnimation(_stateMachine.Player.AnimationData.Skill2ParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.SecondSkillCoolTimeController.IsCoolTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Skill");

        if (normalizedTime >= 1f)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }


}
