using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        if (IsInChaseRange())
        {
            _stateMachine.ChangeState(_stateMachine.ChasingState);
            return;
        }
    }
}