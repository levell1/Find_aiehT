using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkState : EnemyBaseState
{
    public EnemyWalkState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.WalkParameterHash);

        _stateMachine.Enemy.Agent.SetDestination(GetWanderLocation());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.GroundParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChaseRange())
        {
            _stateMachine.ChangeState(_stateMachine.ChasingState);
            return;
        }
        else if(!IsInChaseRange() && _stateMachine.Enemy.Agent.remainingDistance < 0.1f)
        { 
            _stateMachine.ChangeState(_stateMachine.IdlingState);
            _stateMachine.Enemy.PatrolDelay = 0f;
            return;
        }
    }

    public Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(_stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(_stateMachine.Enemy.MinPatrolDistance, _stateMachine.Enemy.MaxPatrolDistance)), out hit, _stateMachine.Enemy.MaxPatrolDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(_stateMachine.Enemy.transform.position, hit.position) < _stateMachine.Enemy.DetectDistance)
        {
            NavMesh.SamplePosition(_stateMachine.Enemy.transform.position + (Random.onUnitSphere * Random.Range(_stateMachine.Enemy.MinPatrolDistance, _stateMachine.Enemy.MaxPatrolDistance)), out hit, _stateMachine.Enemy.MaxPatrolDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }





}
