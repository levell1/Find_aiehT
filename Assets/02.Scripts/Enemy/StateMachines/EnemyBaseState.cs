using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;

    protected readonly PlayerGroundData _groundData;
    public EnemyBaseState(EnemyStateMachine ememyStateMachine)
    {
        _stateMachine = ememyStateMachine;
        _groundData = _stateMachine.Enemy.Data.GroundedData;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void PhysicsUpdate()
    {

    }

    protected void StartAnimation(int animationHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
        Move(movementDirection);
    }

    protected void ForceMove()
    {
        _stateMachine.Enemy.Rigidbody.AddForce(Vector3.forward);
    }

    private Vector3 GetMovementDirection()
    {
        return (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).normalized;
    }

    private void Move(Vector3 direction)
    {
        Rigidbody rigidbody = _stateMachine.Enemy.Rigidbody;
        float movementSpeed = GetMovementSpeed();

        direction *= movementSpeed;
        direction.y = rigidbody.velocity.y;

        rigidbody.velocity = direction;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _stateMachine.Enemy.transform.rotation = Quaternion.Slerp(_stateMachine.Enemy.transform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = _stateMachine.MovementSpeed * _stateMachine.MovementSpeedModifier;

        return movementSpeed;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool IsInChaseRange()
    {
        if (_stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.Enemy.Data.PlayerChasingRange * _stateMachine.Enemy.Data.PlayerChasingRange;
    }
}