using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.JumpForce = _stateMachine.Player.Data.AirData.JumpForce;
        _stateMachine.Player.ForceReceiver.Jump(_stateMachine.JumpForce);

        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Player.Rigidbody.velocity.y < 0)
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
            //Debug.Log("떨어진다~");
            return;
        }

    }
}
