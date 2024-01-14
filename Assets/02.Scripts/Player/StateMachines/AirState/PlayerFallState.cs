using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Player.GroundCheck)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
    }

}
