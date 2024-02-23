

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _alreadyAppliedForce; 
    private bool _alreadyApplyCombo; 

    AttackInfoData _attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        _alreadyApplyCombo= false;
        _alreadyAppliedForce = false;

        int comboIndex = _stateMachine.ComboIndex;
        _attackInfoData = _stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        _stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
        GameManager.Instance.SoundManager.SFXPlay(SFXSoundPathName.AttackSound + (comboIndex+1).ToString());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!_alreadyApplyCombo)
            _stateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (_alreadyApplyCombo) return; 

        if (_attackInfoData.ComboStateIndex == -1) return; 

        if (!_stateMachine.IsAttacking) return; 

        _alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;
        _alreadyAppliedForce = true;


        float comboForceMultiplier = 1.0f + (_stateMachine.ComboIndex * 0.1f); 
        float scaledForce = _attackInfoData.Force * comboForceMultiplier;
        
        //_stateMachine.Player.Rigidbody.AddForce(_stateMachine.Player.transform.forward * scaledForce);
    }


    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Attack");

        if (normalizedTime < 1f) 
        {
            if (normalizedTime >= _attackInfoData.ForceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= _attackInfoData.ComboTransitionTime) 
            {
                TryComboAttack();
            
            }

                int comboIndex = _stateMachine.ComboIndex;
                float playerAtk = _stateMachine.Player.Data.PlayerData.PlayerAttack;
                float damage = _stateMachine.Player.Data.AttackData.AttackInfoDatas[comboIndex].Damage;

                float totalDamage = playerAtk + damage;

                _stateMachine.Player.Weapon.SetAttack(totalDamage);
                _stateMachine.Player.Weapon.EnableCollider();
        }
        else
        {

            _stateMachine.Player.Weapon.DisableCollider();

            if (_alreadyApplyCombo)
            {
                _stateMachine.ComboIndex = _attackInfoData.ComboStateIndex;
                _stateMachine.ChangeState(_stateMachine.ComboAttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }

        }
    }

}
