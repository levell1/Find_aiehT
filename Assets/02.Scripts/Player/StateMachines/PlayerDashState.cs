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
        _stateMachine.Player.DashReceiver.Dash(_stateMachine.DashForce);
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.DashParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.DashParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_stateMachine.Player.DashReceiver._isDash)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }

    }
}
