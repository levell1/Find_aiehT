using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Enemy.Agent.speed = 0f;
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

        //_stateMachine.Enemy.Spot.Collider.enabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Enemy.HealthSystem.IsDead)
        {
            _stateMachine.ChangeState(_stateMachine.DieState);
            return;
        }

        if (IsInAttackRange())
        {
            AnimatorStateInfo animTime = _stateMachine.Enemy.Animator.GetCurrentAnimatorStateInfo(0);
            if (animTime.normalizedTime >= 1f)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
        }
        else
        {
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
}