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

    protected override void OnThrowSkillStarted(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerThrowSkillState);
    }

    protected override void OnSpreadSkillStarted(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_stateMachine.PlayerSpreadSkillState);
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
