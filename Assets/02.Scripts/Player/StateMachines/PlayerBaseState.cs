using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected readonly PlayerGroundData _groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        _stateMachine = playerStateMachine;
        _groundData = _stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
        RengenStamina();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = _stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;

        input.PlayerActions.Jump.started += OnJumpStarted;
        input.PlayerActions.Dash.started += OnDashStarted;

        input.PlayerActions.Attack.performed += OnAttackPerform;
        input.PlayerActions.Attack.canceled+= OnAttackCanceled;

        input.PlayerActions.Skill1.started += OnSkill1Started;

        input.PlayerActions.Skill2.started += OnSkill2Started;

        input.PlayerActions.Interaction.started += OnInteractStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = _stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

        input.PlayerActions.Jump.started -= OnJumpStarted;
        input.PlayerActions.Dash.started -= OnDashStarted;


        input.PlayerActions.Attack.performed -= OnAttackPerform;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;

        input.PlayerActions.Skill1.started -= OnSkill1Started;

        input.PlayerActions.Skill2.started -= OnSkill2Started;

        input.PlayerActions.Interaction.started -= OnInteractStarted;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackPerform(InputAction.CallbackContext context)
    {
        _stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _stateMachine.IsAttacking = false;
    }

    protected virtual void OnSkill1Started(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnSkill2Started(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnInteractStarted(InputAction.CallbackContext context)
    {
        _stateMachine.Player.Interaction.DestroyItem(); 
    }

    //
    private void ReadMovementInput()
    {
        _stateMachine.MovementInput = _stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();

    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private void Move(Vector3 movementDirection)
    {
        Rigidbody rigidbody = _stateMachine.Player.Rigidbody;

        float movementSpeed = GetMovemenetSpeed();

        movementDirection *= movementSpeed;
        movementDirection.y = rigidbody.velocity.y;

        rigidbody.velocity = movementDirection;

    }

    protected void ForceMove()
    {
        int comboIndex = _stateMachine.ComboIndex;
        float force = _stateMachine.Player.Data.AttackData.AttackInfoDatas[comboIndex].Force;

        _stateMachine.Player.Rigidbody.AddForce(_stateMachine.Player.transform.forward * force);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * _stateMachine.MovementInput.y + right * _stateMachine.MovementInput.x;
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = _stateMachine.MovementSpeed * _stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = _stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);

            
        }
    }

    private void RengenStamina()
    {
        _stateMachine.RegenStamina = _stateMachine.Player.Data.GroundData.RegenerateStamina;

        _stateMachine.Player.StaminaSystem.RegenerateStamina(_stateMachine.RegenStamina);
    }

    protected void StartAnimation(int animationHash)
    {
        _stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        _stateMachine.Player.Animator.SetBool(animationHash, false);
    }


    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0); // 현재 애니메이션에 대한 정보
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0); // 다음 애니메이션에 대한 정보

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag)) // 다음 태그가 받아온 태그이고, 현재 애니메이션이 트랜지션을 타고 있는지
        {
            return nextInfo.normalizedTime; // 다음 애니메이션정보를 가져옴 (애니메이션마다 길이가 다르기 때문에 normalized해주는 거임)
        }
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag)) // 애니메이션이 트랜지션을 타고있지 않다면
        {
            return currentInfo.normalizedTime; // 현재 애니메이션으로 돌아옴
        }
        else
        {
            return 0f;
        }

    }

}