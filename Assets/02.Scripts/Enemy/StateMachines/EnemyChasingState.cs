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
        _stateMachine.Enemy.Agent.speed = 5f;

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

        if (_stateMachine.Enemy.HealthSystem.IsDead)
        {
            Debug.Log("c-d");
            _stateMachine.ChangeState(_stateMachine.DieState);
            return;
        }

        _stateMachine.Enemy.Agent.SetDestination(_stateMachine.Target.transform.position);

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
}