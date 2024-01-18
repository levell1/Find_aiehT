using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

        _stateMachine.Enemy.Spot.Collider.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

        _stateMachine.Enemy.Spot.Collider.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (IsInChaseRange())
        {
            _stateMachine.ChangeState(_stateMachine.ChasingState);
            return;
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
            return;
        }
        
    }
}