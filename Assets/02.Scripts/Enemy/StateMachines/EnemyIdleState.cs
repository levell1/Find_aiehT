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
        _stateMachine.Enemy.Agent.speed = 0f;
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
        if (_stateMachine.Enemy.HealthSystem.IsDead)
        {
            _stateMachine.ChangeState(_stateMachine.DieState);
            return;
        }

        if (IsInAttackRange())
        {
            _stateMachine.Enemy.transform.LookAt(_stateMachine.Target.transform);
            if (_stateMachine.Enemy.AttackDelay > _stateMachine.Enemy.Data.AttackDelay)
            {
                _stateMachine.Enemy.AttackDelay = 0;
                _stateMachine.ChangeState(_stateMachine.AttackState);
                return;
            }
            return;
        }

        if (IsInChaseRange())
        {
            _stateMachine.Enemy.transform.LookAt(_stateMachine.Target.transform);
            _stateMachine.ChangeState(_stateMachine.ChasingState);
            return;
        }
        
        if (!IsInChaseRange() &&_stateMachine.Enemy.PatrolDelay > 3f)
        {
            _stateMachine.ChangeState(_stateMachine.WalkState);
            _stateMachine.Enemy.PatrolDelay = 0f;
            return;
        }
    }
}