using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInChaseRange())
        {
            _stateMachine.ChangeState(_stateMachine.IdlingState);
            return;
        }
        else if (IsInAttackRange())
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
            return;
        }
    }

    private bool IsInAttackRange()
    {
         if (_stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.Enemy.Data.AttackRange * _stateMachine.Enemy.Data.AttackRange;
    }
}