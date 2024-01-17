using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{

    private bool alreadyAppliedForce;

    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);

    }

    public override void Update()
    {
        base.Update();
        

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            Debug.Log(normalizedTime);
            if (normalizedTime >= _stateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();

        }
        else
        {
            Debug.Log("2222222");
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

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
            alreadyAppliedForce = true;

        //_stateMachine.Enemy.Rigidbody.Reset();

        _stateMachine.Enemy.Rigidbody.AddForce(_stateMachine.Enemy.transform.forward * _stateMachine.Enemy.Data.Force);

    }
}