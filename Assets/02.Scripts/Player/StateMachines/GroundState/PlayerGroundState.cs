using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(_stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // 입력키가 떼졌을 때의 동작이다. ground에서 해주는 이유는 ground가 아닌 다른 state에 있을 때의 키입력이 없는 경우는 또 다른 동작을 해야하기 때문
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (_stateMachine.MovementInput == Vector2.zero)
        {
            return;
        }

        _stateMachine.ChangeState(_stateMachine.IdleState);

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.JumpState);
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.DashState);
    }

    protected override void OnSkill1Started(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerFirstSkillState);
    }

    protected override void OnSkill2Started(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerSecondSkillState);
    }


    protected virtual void OnMove()
    {
        _stateMachine.ChangeState(_stateMachine.WalkState);
    }

    protected virtual void OnAttack()
    {
        _stateMachine.ChangeState(_stateMachine.ComboAttackState);
    }

}
