using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
        base.Enter();

    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (_stateMachine.Player.GroundCheck.IsGrounded())
        {
            GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.Landing);
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
    }

}
