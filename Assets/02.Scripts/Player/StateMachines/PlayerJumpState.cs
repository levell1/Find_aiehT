using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // 점프할때는 값이 크게 들어갔다가 떨어질 때에는 0보다 작은 값으로 떨어지고있기 때문에
        // ChageState를 해준다.
        //if(_stateMachine.Player.Controller.velocity.y <= 0)
        //{
        //    _stateMachine.ChangeState(_stateMachine.FallState);
        //    return;
        //}

    }
}
