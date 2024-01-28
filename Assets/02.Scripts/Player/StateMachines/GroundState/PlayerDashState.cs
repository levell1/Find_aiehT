using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundState
{

    public PlayerDashState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {

        _stateMachine.DashForce = _stateMachine.Player.Data.GroundData.DashForce;
        _stateMachine.DashStamina = _stateMachine.Player.Data.GroundData.DashStamina;

        _stateMachine.IsDashCoolTime = _stateMachine.Player.DashForceReceiver.IsCoolTime;

        //_stateMachine.MaxStamina = _stateMachine.Player.Data.PlayerData.GetPlayerMaxStamina();
        if(_stateMachine.Player.GroundCheck.IsGrounded())
        {
            if (_stateMachine.Player.StaminaSystem.CanUseDash(_stateMachine.DashStamina) && !_stateMachine.IsDashCoolTime)
            {
                _stateMachine.Player.DashForceReceiver.Dash(_stateMachine.DashForce);
                _stateMachine.Player.StaminaSystem.UseDash(_stateMachine.DashStamina);
                base.Enter();
                StartAnimation(_stateMachine.Player.AnimationData.DashParameterHash);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
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

        StopAnimation(_stateMachine.Player.AnimationData.DashParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.DashForceReceiver.IsCoolTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

    }
}
