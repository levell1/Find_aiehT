using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditorInternal;
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
    private Transform _pigTransform;
    private Animator _animation;
    float time = 0;
    public RangeAttackNode(Transform _playerTransform, Transform transform)
    {
        _player = _playerTransform;
        this._pigTransform = transform;
        _animation = transform.GetComponent<Animator>();
    }


    public override NodeState Evaluate()
    {

        if (time <= 1f)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
            _animation.SetBool(AnimationParameterName.BossAttack, false);
            time += Time.deltaTime;
        }
        else
        {
            _animation.SetBool(AnimationParameterName.BossAttack, true);
            GameObject Bullet = GameManager.Instance.PoolingManager.GetObject("Bullet");
            Bullet.transform.position = _pigTransform.position + _pigTransform.forward * 2 + Vector3.up * 2;

            Rigidbody rigid = Bullet.GetComponent<Rigidbody>();
            Vector3 dirVec = _player.transform.position - _pigTransform.position;
            Vector3 ranVec = new Vector3(Random.Range(-5f, 5f), Random.Range(-1f,1f), Random.Range(-5f, 5f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 10, ForceMode.Impulse);
            time = 0;
            
            return state = NodeState.Success;
        }
        return state = NodeState.Failure;
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
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    private NavMeshAgent _agent;
    private float _agentAttackSpeed = 10.0f;
    private float _time=0;

    public GoToPlayerNode(Transform player, Transform transform,NavMeshAgent agent)
    {
        this._player = player;
        this._pigTransform = transform;
        this._agent = agent;
        _animation = transform.GetComponent<Animator>();
        
        
    }

    public override NodeState Evaluate()
    {

        if (_time < 3f) 
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossFear, true);
            _time += Time.deltaTime;
        }
        else
        {
             _agent.SetDestination(_player.transform.position);
            _agent.speed = _agentAttackSpeed;
            _animation.SetBool(AnimationParameterName.BossFear, false);
            _animation.SetBool(AnimationParameterName.BossRun, true);

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _animation.SetBool(AnimationParameterName.BossRun, false);
                _time = 0;
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
    private float _time = 0;
    private int _count = 0;
    private bool _hasDashed = false;
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
        
        if (_time < 3f)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossFear, true);
            _time += Time.deltaTime;
        }
        else if (3f <= _time && _time <= 4f)
        {
            _animation.SetBool(AnimationParameterName.BossFear, false);
            _animation.SetBool(AnimationParameterName.BossRoll, true);
            //사거리 표시 1번만
            _time += Time.deltaTime;
        }
        else
        {
            if (!_hasDashed)
            {
                DashTowardsPlayer();
            }

            _animation.SetBool(AnimationParameterName.BossRoll, false);
            _animation.SetBool(AnimationParameterName.BossRun, true);

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _hasDashed = false;
                _count++;
                if (_count > Random.Range(2, 5))
                {
                    _count = 0;
                    _time = 0;
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
        for (int i = 5; i >= 0; i--)
        {
            
            _dashPosition = _playerTransform.position + ((_playerTransform.position - _pigTransform.position).normalized * i * 10);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_dashPosition, out hit, 1f, NavMesh.AllAreas))
            {
                _agent.SetDestination(_dashPosition);
                break;
            }
        }
        _hasDashed = true;
    }
}
