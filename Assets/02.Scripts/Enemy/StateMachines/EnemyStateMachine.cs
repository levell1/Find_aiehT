public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Player Target { get; private set; }

    public EnemyIdleState IdlingState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWalkState WalkState { get; }
    public EnemyDieState DieState { get; }


    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Target = GameManager.Instance.Player.GetComponent<Player>();
        IdlingState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WalkState = new EnemyWalkState(this);
        DieState = new EnemyDieState(this);
    }
}
