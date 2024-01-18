using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    public override void Enter()
    {
        Debug.Log("Enter");
        _stateMachine.Enemy.Agent.speed = 0f;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.DieParameterHash);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.DieParameterHash);
    }

    public override void Update()
    {
        if (!_stateMachine.Enemy.HealthSystem.IsDead)
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
            return;
        }
    }
}
