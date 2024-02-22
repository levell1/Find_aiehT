public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);

        _stateMachine.JumpForce = _stateMachine.Player.Data.AirData.JumpForce;
        _stateMachine.Player.ForceReceiver.Jump(_stateMachine.JumpForce);

        base.Enter();
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (_stateMachine.Player.Rigidbody.velocity.y <= 0.2f)
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
            return;
        }

    }
}
