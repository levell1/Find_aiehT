using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;
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

public class CheckPlayerDistanceNotNode : Node
{
    private int _playerLayerMask = 1 << 9;
    private Transform _pigtransform;
    private Animator _animation;
    private float _distance;


    public CheckPlayerDistanceNotNode(Transform transform, float distance)
    {
        this._pigtransform = transform;
        this._distance = distance;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        var collider = Physics.OverlapSphere(_pigtransform.position, _distance, _playerLayerMask);//주변 콜라이더 추출
        if (collider.Length > 0) return state = NodeState.Failure;
        _animation.SetBool(AnimationParameterName.BossWalk, false);
        return state = NodeState.Success;
    }
}

public class LevitateNode : Node
{
    private Animator _animation;
    private LevitateObject _levitateObject;
    private Transform _pigTransform;
    private Transform _player;
    private float _time;
    private float _cooltime;

    public LevitateNode(Transform pigtransform, Transform playerTransform, LevitateObject levitateObject, float cooltime)
    {
        _levitateObject = levitateObject;
        _pigTransform = pigtransform;
        _player = playerTransform;
        _animation = pigtransform.GetComponent<Animator>();
        _cooltime= cooltime;
    }


    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
        rotation.y = 0f;
        _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
        if (_time > _cooltime)
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
        return state = NodeState.Running;

    }

    
}

public class RangeAttackNode : Node
{
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    private float _Bulletangle = 10f;
    float time = 0;
    public RangeAttackNode(Transform _playerTransform, Transform transform)
    {
        _player = _playerTransform;
        this._pigTransform = transform;
        _animation = transform.GetComponent<Animator>();

    }


    public override NodeState Evaluate()
    {
        time += Time.deltaTime;

        if (time <= 1f)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
            _animation.SetBool(AnimationParameterName.BossAttack, false);
        }
        else
        {
            Vector3 direction = _player.position - _pigTransform.position;
            direction.y = 0f;

            _animation.SetBool(AnimationParameterName.BossAttack, true);

            GameObject bullet1 = GameManager.Instance.PoolingManager.GetObject("Bullet");
            bullet1.transform.rotation = Quaternion.LookRotation(direction);
            bullet1.transform.position = _pigTransform.position + _pigTransform.forward * 2 ;
            bullet1.GetComponent<Rigidbody>().velocity = bullet1.transform.forward * 5f;


            GameObject bullet2 = GameManager.Instance.PoolingManager.GetObject("Bullet");
            bullet2.transform.rotation = Quaternion.LookRotation(direction);
            bullet2.transform.Rotate(Vector3.up * _Bulletangle);
            bullet2.transform.position = _pigTransform.position + _pigTransform.forward * 2 ;
            bullet2.GetComponent<Rigidbody>().velocity = bullet2.transform.forward * 5f;


            GameObject bullet3 = GameManager.Instance.PoolingManager.GetObject("Bullet");
            bullet3.transform.rotation = Quaternion.LookRotation(direction);
            bullet3.transform.Rotate(Vector3.down * _Bulletangle);
            bullet3.transform.position = _pigTransform.position + _pigTransform.forward * 2 ;
            bullet3.GetComponent<Rigidbody>().velocity = bullet3.transform.forward * 5f;
            
            time = 0;
            return state = NodeState.Success;
        }
        return state = NodeState.Failure;
    }
}


public class LightAttack : Node
{
    private Transform _player;
    private Transform _pigTransform;
    private Animator _animation;
    float _time = 0;
    float _waitTime;
    public LightAttack(Transform _playerTransform, Transform transform, float WaitTime)
    {
        _player = _playerTransform;
        this._pigTransform = transform;
        _animation = transform.GetComponent<Animator>();
        _waitTime = WaitTime;
    }


    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time <= _waitTime)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 4);
            _animation.SetBool(AnimationParameterName.BossFly, true);
            return state = NodeState.Running;
        }
        else
        {            
            GameObject lightObject = GameManager.Instance.PoolingManager.GetObject("Light");
            lightObject.transform.rotation = Quaternion.identity;
            lightObject.transform.position = _pigTransform.position + _pigTransform.forward * 2;
            _animation.SetBool(AnimationParameterName.BossFly, false);
            _time = 0;
            return state = NodeState.Success;
        }
    }
}

public class RunAwayNode : Node
{
    private Animator _animation;
    private Vector3 _randomPoint = Vector3.zero;
    private NavMeshAgent _agent;
    private float _beforeSpeed;
    private float _randomRange =10f;
    public RunAwayNode(Transform transform, NavMeshAgent agent, float beforeSpeed)
    {
        _animation = transform.GetComponent<Animator>();
        _agent = agent;
        _beforeSpeed = beforeSpeed;
        _randomPoint = GetRandomPositionOnNavMesh();
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _randomRange;
        randomDirection += _agent.gameObject.transform.position;
        randomDirection.y= _agent.gameObject.transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, _randomRange, NavMesh.AllAreas)) // 랜덤 위치가 NavMesh 위에 있는지 확인
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
        _agent.speed =5f;
        if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
        {
            _randomPoint = GetRandomPositionOnNavMesh();
            Debug.Log(_randomPoint);
            _agent.speed = _beforeSpeed;
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
    private float _waitTime = 0;

    public GoToPlayerNode(Transform player, Transform transform,NavMeshAgent agent, float waittime)
    {
        this._player = player;
        this._pigTransform = transform;
        this._agent = agent;
        _waitTime = waittime;
        _animation = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime) 
        {
            Quaternion rotation = Quaternion.LookRotation(_player.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 2);
            _animation.SetBool(AnimationParameterName.BossFear, true);
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
    private float _waitTime = 0;
    private int _count = 0;
    private bool _hasDashed = false;
    private Vector3 _dashPosition = Vector3.zero;
    private BoxCollider _collider;

    public DashToPlayer(Transform player, Transform transform, NavMeshAgent agent, float waitTime)
    {
        this._playerTransform = player;
        this._pigTransform = transform;
        this._agent = agent;
        _waitTime = waitTime;
        _animation = transform.GetComponent<Animator>();
        _collider = _pigTransform.gameObject.GetComponent<BoxCollider>();
    }

    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 3);
            _agent.SetDestination(_pigTransform.position);
            _animation.SetBool(AnimationParameterName.BossSit, true);
        }
        else if (_waitTime <= _time && _time <= _waitTime+0.5f)
        {
            Quaternion rotation = Quaternion.LookRotation(_playerTransform.position - _pigTransform.position);
            _pigTransform.rotation = Quaternion.Lerp(_pigTransform.rotation, rotation, Time.deltaTime * 3);
            _animation.SetBool(AnimationParameterName.BossSit, false);
            _animation.SetBool(AnimationParameterName.BossRoll, true);
        }
        else
        {
            if (!_hasDashed)
            {
                DashTowardsPlayer();
            }

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
            {
                _hasDashed = false;
                _count++;
                if (_count > Random.Range(2, 5)|| _pigTransform.gameObject.name == "ChickBullet(Clone)")
                {
                    _collider.enabled = false;
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
        for (int i = 4; i >= 0; i--)
        {
            _dashPosition = _playerTransform.position + ((_playerTransform.position - _pigTransform.position).normalized * i * 10);
            _dashPosition.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(_dashPosition, out hit, 1f, NavMesh.AllAreas))
            {
                _agent.SetDestination(_dashPosition);
                _collider.enabled = true;
                _animation.SetBool(AnimationParameterName.BossRoll, false);
                _animation.SetBool(AnimationParameterName.BossRun, true);
                break;
            }
        }
        _hasDashed = true;
    }
}
