using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CheckPlayerDistanceNode : Node
{
    private int _playerLayerMask = 1 << 9;
    private Transform _pigtransform;
    private Animator _animation;
    private float _distance;
    

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

public class LevitateNode : Node
{
    private Animator _animation;
    private LevitateObject _levitateObject;
    private float _time;

    public LevitateNode(Transform pigtransform, LevitateObject levitateObject)
    {
        _levitateObject = levitateObject;
        _animation = pigtransform.GetComponent<Animator>();
    }


    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time > 2)
        {
            _levitateObject.gameObject.SetActive(true);
            if (_levitateObject.EndSkill == true)
            {
                _levitateObject.gameObject.SetActive(false);
                _time = 0;
                _animation.SetBool(AnimationParameterName.BossSpin, false);
                return state = NodeState.Success;
            }
            else
            {
                _animation.SetBool(AnimationParameterName.BossSpin, true);
                
                return state = NodeState.Running;
            }
        }
        return state = NodeState.Failure;

    }

    
}

public class RangeAttackNode : Node
{
    private Transform _player;
    private Transform _pigtransform;
    private Animator _animation;
    private int _count = 0;

    public RangeAttackNode(Transform _playerTransform, Transform transform)
    {
        _player = _playerTransform;
        this._pigtransform = transform;
        _animation = transform.GetComponent<Animator>();
    }


    public override NodeState Evaluate()
    {
        //_agent.SetDestination(player.transform.position);
        _pigtransform.LookAt(_player);
        if (_count < 3)
        {
            Debug.Log("카운트" + _count);
            _animation.SetBool(AnimationParameterName.BossSpin, true);
            _player.gameObject.GetComponent<HealthSystem>().TakeDamage(3);
            _count++;
            return state = NodeState.Running;
        }
        else
        {
            _count = 0;
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
        
        _randomPoint = GetRandomPositionOnNavMesh();
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 6f;
        randomDirection += _agent.gameObject.transform.position; 
       
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 6f, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인
        {
            return hit.position; 
        }
        else
        {
            return _agent.gameObject.transform.position; 
        }
    }

    public override NodeState Evaluate()
    {
        //멀리가기
        _agent.SetDestination(_randomPoint);
        _animation.SetBool(AnimationParameterName.BossWalk, true);
        _agent.speed = 3.5f;
        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
        {
            _randomPoint = GetRandomPositionOnNavMesh();
            _animation.SetBool(AnimationParameterName.BossWalk, false);
            return state = NodeState.Success; 
        }
        else
        {
            return state = NodeState.Running; 
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
    float time=0;

    public GoToPlayerNode(Transform player, Transform transform,NavMeshAgent agent)
    {
        this.player = player;
        this.transform = transform;
        this._agent = agent;
        _animation = transform.GetComponent<Animator>();
        
        
    }

    public override NodeState Evaluate()
    {

        if (time < 3f) 
        {
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossFear, true);
            time += Time.deltaTime;
        }
        else
        {
             _agent.SetDestination(player.transform.position);
            _agent.speed = _agentAttackSpeed;
            _animation.SetBool(AnimationParameterName.BossFear, false);
            _animation.SetBool(AnimationParameterName.BossRun, true);

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _animation.SetBool(AnimationParameterName.BossRun, false);
                time = 0;
                return state = NodeState.Success; 
            }else
            {
                return state = NodeState.Running; 
            }
        }
        return state = NodeState.Running; 
    }

}

public class DashToPlayer : Node
{
    private Transform _playerTransform;
    private Transform _pigTransform;
    private Animator _animation;
    private NavMeshAgent _agent;
    private float _agentAttackSpeed = 10.0f;
    float time = 0;
    private int count = 0;
    private bool hasDashed = false;
    private Vector3 _dashPosition = Vector3.zero;

    public DashToPlayer(Transform player, Transform transform, NavMeshAgent agent)
    {
        this._playerTransform = player;
        this._pigTransform = transform;
        this._agent = agent;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        
        if (time < 3f)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossIdleC, true);
            time += Time.deltaTime;
        }
        else
        {
            if (!hasDashed)
            {
                DashTowardsPlayer();
            }

            _animation.SetBool(AnimationParameterName.BossIdleC, false);
            _animation.SetBool(AnimationParameterName.BossRun, true);

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                hasDashed = false;
                count++;
                Debug.Log(count);
                if (count > Random.Range(2, 5))
                {
                    count = 0;
                    time = 0;
                    _animation.SetBool(AnimationParameterName.BossRun, false);
                    return state = NodeState.Success;
                }
            }
            else
            {
                return state = NodeState.Running;
            }
        }
        return state = NodeState.Running;
    }

    private void DashTowardsPlayer()
    {
        for (int i = 5; i > 0; i--)
        {
            _dashPosition = _playerTransform.position + ((_playerTransform.position - _pigTransform.position).normalized * i * 10);
            Debug.Log((_playerTransform.position - _pigTransform.position).normalized);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_dashPosition, out hit, 1f, NavMesh.AllAreas))
            {
                _agent.SetDestination(_dashPosition);
            }
        }
        hasDashed = true;
    }
}
