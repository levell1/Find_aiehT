
public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    public EnemyBaseState(EnemyStateMachine ememyStateMachine)
    {
        _stateMachine = ememyStateMachine;
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

    protected bool IsInChaseRange()
    {
        if (_stateMachine.Target.HealthSystem.IsDead) { return false; }
        
        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.Enemy.Data.PlayerChasingRange * _stateMachine.Enemy.Data.PlayerChasingRange;
    }

    protected bool IsInAttackRange()
    {
        if (_stateMachine.Target.HealthSystem.IsDead) { return false; }

        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.Enemy.Data.AttackRange * _stateMachine.Enemy.Data.AttackRange;
    }
}