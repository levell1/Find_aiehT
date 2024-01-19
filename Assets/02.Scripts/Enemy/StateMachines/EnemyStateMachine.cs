using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public HealthSystem Target { get; private set; }

    public EnemyIdleState IdlingState { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyWalkState WalkState { get; }
    public EnemyDieState DieState { get; }
    public float MovementSpeed { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();

        IdlingState = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);
        WalkState = new EnemyWalkState(this);
        DieState = new EnemyDieState(this);
    }
}
