using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFirstSkillState : PlayerGroundState
{

    SkillInfoData _skillData;

    public PlayerFirstSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        
    }

    /// <summary>
    /// 스태미너 깎이기
    /// 데미지 넣기
    /// 쿨타임 적용
    /// </summary>

    public override void Enter()
    {

        

        int _skillIndex = 0;
        _stateMachine.MovementSpeedModifier = 0; // 공격할 때 안움직임
        _skillData = _stateMachine.Player.Data.SkillData.GetSkillData(_skillIndex);

        int _skillCost = _skillData.GetSkillCost();
        _stateMachine.IsSkillCoolTime = _stateMachine.Player.SkillCoolTimeController.IsCoolTime;

        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.Skill1ParameterHash);

        if (_stateMachine.Player.StaminaSystem.CanUseSkill(_skillCost) && !_stateMachine.IsSkillCoolTime)
        {
            _stateMachine.Player.StaminaSystem.UseSkill(_skillCost);
            Debug.Log("사용");

            _stateMachine.Player.SkillCoolTimeController.StartCoolTime(_skillData.GetSkillCoolTime());
        }
       else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.Skill1ParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.SkillCoolTimeController.IsCoolTime)
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
