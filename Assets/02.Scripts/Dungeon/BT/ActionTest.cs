using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPlayerDistanceNode : Node
{
    private int _playerLayerMask = 1 << 9;
    private Transform _pigtransform;
    private Animator _animation;
    private float _distance = 0;

    public CheckPlayerDistanceNode(Transform transform,float distance)
    {
        this._pigtransform = transform;
        this._distance = distance;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        var collider = Physics.OverlapSphere(_pigtransform.position, _distance, _playerLayerMask);//주변 콜라이더 추출
        if (collider.Length <= 0) return state = NodeState.Failure;
        _animation.SetBool(AnimationParameterName.BossWalk, false);
        return state = NodeState.Success;
    }
}

public class AttackNode : Node
{
    private Transform _player;
    private Transform _pigtransform;
    private Animator _animation;
    private int count = 0;

    public AttackNode(Transform _playerTransform, Transform transform, float distance)
    {
        _player = _playerTransform;
        this._pigtransform = transform;
        _animation = transform.GetComponent<Animator>();
    }


    public override NodeState Evaluate()
    {
        //_agent.SetDestination(player.transform.position);
        _pigtransform.LookAt(_player);
        if (count < 3)
        {
            Debug.Log("카운트" + count);
            _animation.SetBool(AnimationParameterName.BossSpin, true);
            _player.gameObject.GetComponent<HealthSystem>().TakeDamage(3);
            count++;
            return state = NodeState.Running;
        }
        else
        {
            count = 0;
            _animation.SetBool(AnimationParameterName.BossSpin, false);
            return state = NodeState.Success;
        }
    }
}



public class RunAwayNode : Node
{
    private Animator _animation;
    private Vector3 _randomPoint = Vector3.zero;
    private NavMeshAgent _agent;
    public RunAwayNode(Transform transform, NavMeshAgent agent)
    {
        _animation = transform.GetComponent<Animator>();
        _agent = agent;
        agent.speed = 3.5f;
        _randomPoint = GetRandomPositionOnNavMesh();
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 6f;
        randomDirection += _agent.gameObject.transform.position; // 랜덤 방향 벡터를 현재 위치에 더합니다.

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 6f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인합니다.
        {
            return hit.position; // NavMesh 위의 랜덤 위치를 반환합니다.
        }
        else
        {
            return _agent.gameObject.transform.position; // NavMesh 위의 랜덤 위치를 찾지 못한 경우 현재 위치를 반환합니다.
        }
    }

    public override NodeState Evaluate()
    {
        //멀리가기
        _agent.SetDestination(_randomPoint);

        _animation.SetBool(AnimationParameterName.BossWalk, true);

        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
        {
            _randomPoint = GetRandomPositionOnNavMesh();
            return state = NodeState.Success; // 행동 완료 상태 반환
        }
        else
        {
            return state = NodeState.Running; // 행동 진행 중 상태 반환
        }
       
    }
}

public class GoToPlayerNode : Node
{
    private Transform player;
    private Transform transform;
    private Animator _animation;
    private NavMeshAgent _agent;
    private float _agentAttackSpeed = 10.0f;

    public GoToPlayerNode(Transform player, Transform transform,NavMeshAgent agent)
    {
        this.player = player;
        this.transform = transform;
        this._agent = agent;
        _animation = transform.GetComponent<Animator>();
        agent.speed = _agentAttackSpeed;
    }

    public override NodeState Evaluate()
    {
        transform.LookAt(player);
        _agent.SetDestination(player.transform.position);
        
        _animation.SetBool(AnimationParameterName.BossWalk, true);
        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
        {
            _animation.SetBool(AnimationParameterName.BossWalk, false);
            return state = NodeState.Success; // 행동 완료 상태 반환
        }
        {
            return state = NodeState.Running; // 행동 진행 중 상태 반환
        }

    }
}
