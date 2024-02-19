using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerThrowSkillState : PlayerSkillState
{

    SkillInfoData _skillData;

    public PlayerThrowSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
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
       
        _skillData = _stateMachine.Player.Data.SkillData.GetSkillData(_skillIndex);

        
        int _skillCost = _skillData.SKillCost;
        _stateMachine.IsSkillCoolTime = _stateMachine.Player.FirstSkillCoolTimeController.IsCoolTime;

        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.ThrowSkillParameterHash);

        if (_stateMachine.Player.StaminaSystem.CanUseSkill(_skillCost) && !_stateMachine.Player.FirstSkillCoolTimeController.IsCoolTime)
        {
            _stateMachine.MovementSpeedModifier = 0; // 공격할 때 안움직임
            _stateMachine.Player.SkillInstantiator.InstantiateTomato();

            float skillDamage = _skillData.SkillDamage;
            float playerDamage = _stateMachine.Player.Data.PlayerData.PlayerAttack;

            float totalDamage = skillDamage + playerDamage;

            _stateMachine.Player.StaminaSystem.UseSkill(_skillCost);

            Debug.Log("totalDamage: " + totalDamage);
            Debug.Log("사용");

            _stateMachine.Player.FirstSkillCoolTimeController.StartCoolTime(_skillData.SkillCoolTime);
           
        }
       else
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.ThrowSkillParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.FirstSkillCoolTimeController.IsCoolTime)
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
