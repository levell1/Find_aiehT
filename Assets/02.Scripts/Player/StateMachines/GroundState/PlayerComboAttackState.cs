using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _alreadyAppliedForce; // 힘을 적용 햇는지 
    private bool _alreadyApplyCombo; // 콤보를 적용했는지

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
        if (_alreadyApplyCombo) return; // 콤보를 이미 한 경우에 alreadyApplyCombo가 true이기 때문에 return

        if (_attackInfoData.ComboStateIndex == -1) return; // 마지막 공격이기 때문에 return

        if (!_stateMachine.IsAttacking) return; // 공격중이 아니기 때문에 return

        _alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (_alreadyAppliedForce) return;
        _alreadyAppliedForce = true;

        //_stateMachine.Player.ForceReceiver.Reset();

        float comboForceMultiplier = 1.0f + (_stateMachine.ComboIndex * 0.1f); 
        float scaledForce = _attackInfoData.Force * comboForceMultiplier;

        _stateMachine.Player.Rigidbody.AddForce(_stateMachine.Player.transform.forward * scaledForce);
    }


    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Attack");

        if (normalizedTime < 1f) // 애니메이션이 진행중
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

            // TODO
            //if (WeaponCollider != null)
            //    WeaponCollider.enabled = true;
            //Debug.Log(_stateMachine.Player.Weapon.gameObject.name);

            //Debug.Log("comboIndex" + comboIndex);
            //Debug.Log("playerAtk" + playerAtk);
            //Debug.Log("damage" + damage);
            //Debug.Log("totalDamage" + totalDamage);

        }
        else
        {
            //if (WeaponCollider != null)
            //    WeaponCollider.enabled = false;
            //_stateMachine.Player.Weapon.gameObject.SetActive(false);
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
